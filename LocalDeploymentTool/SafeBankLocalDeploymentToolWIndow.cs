using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using LocalDeploymentTool.Properties;
using Microsoft.Win32;
using Newtonsoft.Json;
using Microsoft.Web.Administration;
using System.Data.Sql;

namespace LocalDeploymentTool
{
    public partial class SafeBankLocalDeploymentToolWIndow : Form
    {

        private const string DeploymentLogFilename = "DeploymentLog.json";
        private readonly Dictionary<string,bool> _featuresList = new Dictionary<string, bool>();
        private readonly string[] _requiredFeatures;
        private readonly DeploymentLog _deploymentLog;

        public static void AddDirectorySecurity(string directory, string account, FileSystemRights rights, AccessControlType controlType)
        {
            var dInfo = new DirectoryInfo(directory);
            var dSecurity = dInfo.GetAccessControl();
            dSecurity.AddAccessRule(new FileSystemAccessRule(account,rights,controlType));
            dInfo.SetAccessControl(dSecurity);
        }

        public SafeBankLocalDeploymentToolWIndow()
        {
            InitializeComponent();
            _requiredFeatures = new[]
            {
                "IIS-ManagementConsole",
                "IIS-NetFxExtensibility45",
                "IIS-ISAPIExtensions",
                "IIS-ISAPIFilter",
                "IIS-StaticContent",
                "IIS-DefaultDocument",
                "IIS-RequestFiltering",
                "IIS-WebServerManagementTools",
                "IIS-WebServerRole",
                "IIS-ASPNET45"
            };
            _deploymentLog = new DeploymentLog();
            if (File.Exists(DeploymentLogFilename))
            {
                var logFile = File.OpenRead(DeploymentLogFilename);
                var bytesToRead = (int) logFile.Length;
                var jsonBytes = new byte[bytesToRead];
                logFile.Read(jsonBytes, 0, bytesToRead);
                var jsonString = Encoding.ASCII.GetString(jsonBytes);
                _deploymentLog = JsonConvert.DeserializeObject<DeploymentLog>(jsonString);
                logFile.Close();
            }
            else
            {
                var logFile = File.Create(DeploymentLogFilename);
                var jsonString = JsonConvert.SerializeObject(_deploymentLog);
                var jsonBytes = Encoding.ASCII.GetBytes(jsonString);
                logFile.Write(jsonBytes,0,jsonBytes.Length);
                logFile.Close();
            }
        }
        
        private void SaveDeploymentLog()
        {
            var logFile = File.OpenWrite(DeploymentLogFilename);
            logFile.SetLength(0);
            logFile.Flush();
            var jsonString = JsonConvert.SerializeObject(_deploymentLog);
            var jsonBytes = Encoding.ASCII.GetBytes(jsonString);
            logFile.Write(jsonBytes, 0, jsonBytes.Length);
            logFile.Close();
        }

        private void AddMessageToProcessLog(string message)
        {
            DeploymentProcessLog.Text += message + "\n";
        }

        private static bool IsAdministrator()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent()))
                    .IsInRole(WindowsBuiltInRole.Administrator);
        }

        private string RunCommand(string command)
        {
            var output = new StringBuilder();
            var pStartInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            var p = new Process {StartInfo = pStartInfo};
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.OutputDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    output.Append(e.Data + "\n");
                }
            };
            p.ErrorDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    output.Append("ERROR: " + e.Data + "\n");
                }
            };
            p.Start();
            p.BeginOutputReadLine();
            p.WaitForExit();
            return output.ToString();
        }

        private void GetFeaturesList()
        {
            var output = RunCommand("dism /online /get-features");
            var features = Regex.Matches(output, "Feature Name : (.)+\nState : (Disabled|Enabled)\n");
            _featuresList.Clear();
            foreach (Match feature in features)
            {
                var featureString = feature.Value;
                var featureName = Regex.Replace(featureString, "State : (Disabled|Enabled)\n", string.Empty).Replace("\n",string.Empty).Replace("Feature Name : ",string.Empty);
                var featureStateString = Regex.Replace(featureString, "Feature Name : (.)+\n", string.Empty).Replace("\n", string.Empty).Replace("State : ",string.Empty);
                _featuresList.Add(featureName, featureStateString == "Enabled");
            }
        }

        private bool EnableFeatures(string[] features, string missingFeatureMessage, bool checkToEnable)
        {
            var featureNameList = new StringBuilder();
            foreach (var requiredFeature in features)
            {
                if (!_featuresList.ContainsKey(requiredFeature))
                {
                    MessageBox.Show(
                        string.Format(missingFeatureMessage, requiredFeature),
                        "Feature dose not exist!", MessageBoxButtons.OK, MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                    return false;
                }
                DialogResult canEnableFeature = DialogResult.Yes;
                if (checkToEnable)
                {
                    canEnableFeature =
                        MessageBox.Show(
                            "We need the enable " + requiredFeature + "\nAre you happy for this to be done?",
                            "Can we enable " + requiredFeature + " for you?", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.DefaultDesktopOnly,
                            false);
                }
                if (canEnableFeature != DialogResult.Yes)
                {
                    MessageBox.Show(
                        string.Format("The deployment will fail if {0} is not enabled, reverting deployment.", requiredFeature),
                        "Feature not enabled!", MessageBoxButtons.OK, MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                    return false;
                }
                featureNameList.AppendFormat("/FeatureName:{0} ", requiredFeature);
                AddMessageToProcessLog("Turning on feature " + requiredFeature);
            }
            RunCommand("DISM /Online /Enable-Feature " + featureNameList);
            return true;
        }

        private bool EnableIis()
        {
            return EnableFeatures(_requiredFeatures, "Unable to enable IIS as a required feature for the SafeBank application {0} is not available, this might be down to your version of Windows.",false);
        }

        private bool DisableFeature(string[] features, string missingFeatureMessage, bool checkToDiable)
        {
            var featureNameList = new StringBuilder();
            foreach (var requiredFeature in features)
            {
                if (!_featuresList.ContainsKey(requiredFeature))
                {
                    MessageBox.Show(
                        string.Format(missingFeatureMessage, requiredFeature),
                        "Feature dose not exist!", MessageBoxButtons.OK, MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                    return false;
                }
                DialogResult canDiableFeature = DialogResult.Yes;
                if (checkToDiable)
                {
                    canDiableFeature =
                        MessageBox.Show(
                            "We think that " + requiredFeature + " should be turned off as we turned it on.\nAre you happy for this to be done?",
                            "Can we turn this " + requiredFeature + " off for you?", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.DefaultDesktopOnly,
                            false);
                }
                if (canDiableFeature != DialogResult.Yes) continue;
                featureNameList.AppendFormat("/FeatureName:{0} ", requiredFeature);
                AddMessageToProcessLog("Turning off feature " + requiredFeature);
            }
            RunCommand("DISM /NoRestart /Online /Disable-Feature " + featureNameList);
            return true;
        }

        private bool DiableIis()
        {
            return DisableFeature(_requiredFeatures,
                "Unable to disable {0} as the feature dose not exist, this is probably due to the deployment log been tampered with.",
                false);
        }

        private void DeployIIS()
        {
            var iisVersion = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\InetStp", "VersionString", null);
            if (iisVersion == null)
            {
                AddMessageToProcessLog(Resources.IIS_detection_failed_asking_user_for_instructions);
                var result = MessageBox.Show(Resources.IIS_is_not_installed_Text, Resources.IIS_is_not_installed,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly, false);
                if (result == DialogResult.No)
                {
                    MessageBox.Show(Resources.Unable_to_deploy_SafeBank_as_IIS_is_required_for_it_to_run,
                        Resources.Unable_to_deploy, MessageBoxButtons.OK, MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, false);
                    return;
                }
                AddMessageToProcessLog(Resources.Enabling_IIS);
                GetFeaturesList();
                if (!EnableIis())
                {
                    return;
                }
                _deploymentLog.DeployedIis = true;
                SaveDeploymentLog();
                AddMessageToProcessLog(Resources.IIS_Enabled);
                GetFeaturesList();
            }
            else
            {
                GetFeaturesList();
                var missingFeatures = _requiredFeatures.Where(x => _featuresList.All(y => y.Key != x)).ToArray();
                if (missingFeatures.Any())
                {
                    AddMessageToProcessLog("There are missing requried windows features");
                    if (
                        !EnableFeatures(missingFeatures,
                            "The required feature for IIS {0} is missing so can't be enabled, this might be down to your version of Windows.",
                            true))
                    {
                        return;
                    }
                    _deploymentLog.WindowFeaturesDeployed.AddRange(missingFeatures);
                    SaveDeploymentLog();
                    AddMessageToProcessLog(Resources.IIS_Enabled);
                }
                else
                {
                    AddMessageToProcessLog("IIS already installed");
                }
            }
        }

        private string GetInetputFolder()
        {
            var systemLetter = Environment.GetEnvironmentVariable("SystemDrive");
            var inetpubFolder = systemLetter + "\\inetpub";
            if (!Directory.Exists(inetpubFolder))
            {
                AddMessageToProcessLog("Missing inetput folder");
                var result = MessageBox.Show(
                    "IIS's inetput folder is not on the system drive this is not normal but you may have set your system up this way.\nDo you know where the IIS inetpub folder is located?",
                    "IIS's folder miss placed?", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly, false);
                if (result != DialogResult.Yes)
                {
                    result = MessageBox.Show("Do you wan't use to create " + inetpubFolder + " folder?",
                        "IIS's folder missing", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, false);
                    if (result != DialogResult.Yes)
                    {
                        MessageBox.Show("Unable to continue with deployment will now roll back.",
                            "IIS's folder", MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, false);
                        AddMessageToProcessLog("Rolling back deployment");
                        Undeploy();
                        AddMessageToProcessLog("Rolled back deployment");
                        return null;
                    }
                    Directory.CreateDirectory(inetpubFolder);
                    _deploymentLog.FoldersCreated.Add(inetpubFolder);
                    AddMessageToProcessLog("Created " + inetpubFolder + " folder");
                }
                else
                {
                    result = folderBrowser.ShowDialog();
                    if (result != DialogResult.OK)
                    {
                        MessageBox.Show("Unable to continue with deployment will now roll back.",
                            "IIS's folder", MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, false);
                        AddMessageToProcessLog("Rolling back deployment");
                        Undeploy();
                        AddMessageToProcessLog("Rolled back deployment");
                        return null;
                    }
                    inetpubFolder = folderBrowser.SelectedPath;
                    AddMessageToProcessLog("Located " + inetpubFolder + " folder");
                }
            }
            else
            {
                AddMessageToProcessLog("inetput folder exists");
            }
            return inetpubFolder;
        }

        private string GetWwwrootFolder(string inetpubFolder)
        {
            var wwwrootFolder = inetpubFolder + "\\wwwroot";
            if (!Directory.Exists(wwwrootFolder))
            {
                AddMessageToProcessLog("Missing wwwroot folder");
                var result = MessageBox.Show(
                    "IIS's inetput folder is not on the system drive this is not normal but you may have set your system up this way.\nDo you know where the IIS inetpub folder is located?",
                    "IIS's folder miss placed?", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly, false);
                if (result != DialogResult.Yes)
                {
                    result = MessageBox.Show("Do you wan't use to create " + wwwrootFolder + " folder?",
                        "IIS's folder missing", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, false);
                    if (result != DialogResult.Yes)
                    {
                        MessageBox.Show("Unable to continue with deployment will now roll back.",
                            "IIS's folder", MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, false);
                        AddMessageToProcessLog("Rolling back deployment");
                        Undeploy();
                        AddMessageToProcessLog("Rolled back deployment");
                        return null;
                    }
                    Directory.CreateDirectory(wwwrootFolder);
                    _deploymentLog.FoldersCreated.Add(wwwrootFolder);
                    AddMessageToProcessLog("Created " + wwwrootFolder + " folder");
                }
                else
                {
                    result = folderBrowser.ShowDialog();
                    if (result != DialogResult.OK)
                    {
                        MessageBox.Show("Unable to continue with deployment will now roll back.",
                            "IIS's folder", MessageBoxButtons.OK, MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, false);
                        AddMessageToProcessLog("Rolling back deployment");
                        Undeploy();
                        AddMessageToProcessLog("Rolled back deployment");
                        return null;
                    }
                    wwwrootFolder = folderBrowser.SelectedPath;
                    AddMessageToProcessLog("Located " + wwwrootFolder + " folder");
                }
            }
            else
            {
                AddMessageToProcessLog("wwwroot folder exists");
            }
            return wwwrootFolder;
        }

        private string CreateSafeBankFolder(string wwwrootFolder)
        {
            var safeBankFolder = wwwrootFolder + "\\SafeBank";
            if (Directory.Exists(safeBankFolder))
            {
                var redeploy = MessageBox.Show("The SafeBank folder already has thing in do you wan't to replace it?",
                    "Already deployed?", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, false);
                if (redeploy != DialogResult.Yes)
                {
                    AddMessageToProcessLog(safeBankFolder + " already exists and was not changed.");
                    return safeBankFolder;
                }
                var username = Environment.UserName;
                AddDirectorySecurity(safeBankFolder, username, FileSystemRights.FullControl, AccessControlType.Allow);
                if (Directory.GetFileSystemEntries(safeBankFolder).Any())
                {
                    var backup = MessageBox.Show("The SafeBank folder already has thing in do you wan't to rename the folder to keep a copy?",
                        "Already deployed?", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, false);
                    if (backup == DialogResult.Yes)
                    {
                        var backupFolder = safeBankFolder + DateTime.Now.ToString("yy_MM_dd_hh_mm_ss");
                        Directory.Move(safeBankFolder, backupFolder);
                        AddMessageToProcessLog(safeBankFolder + " moved to " + backupFolder);
                        _deploymentLog.FoldersMoved.Add(backupFolder,safeBankFolder);
                        SaveDeploymentLog();
                    }
                    else
                    {
                        Directory.Move(safeBankFolder, safeBankFolder + "_temp");//This temp folder should be deleted at the end of the deployment
                        AddMessageToProcessLog("Removed the " + safeBankFolder + " folder");
                        _deploymentLog.TempFolders.Add(safeBankFolder + "_temp");
                        SaveDeploymentLog();
                    }
                }
                else
                {
                    AddMessageToProcessLog(safeBankFolder + " exists but was empty");
                    return safeBankFolder;
                }
            }
            Directory.CreateDirectory(safeBankFolder);
            _deploymentLog.FoldersCreated.Add(safeBankFolder);
            SaveDeploymentLog();
            AddMessageToProcessLog(safeBankFolder + " created");
            return safeBankFolder;
        }

        private string DeployFolders()
        {
            var inetpubFolder = GetInetputFolder();
            if (inetpubFolder == null) return null;
            var wwwrootFolder = GetWwwrootFolder(inetpubFolder);
            if (wwwrootFolder == null) return null;
            return CreateSafeBankFolder(wwwrootFolder);
        }

        private ApplicationPool DeployAppPool(ServerManager serverManager)
        {
            if (serverManager.ApplicationPools.All(x => x.Name != "SafeBankAppPool"))
            {
                AddMessageToProcessLog("Creating SafeBankAppPool");
                var appPool = serverManager.ApplicationPools.Add("SafeBankAppPool");
                serverManager.CommitChanges();
                _deploymentLog.DeployedAppPool = true;
                SaveDeploymentLog();
                AddMessageToProcessLog("Created SafeBankAppPool");
                return appPool;
            }
            AddMessageToProcessLog("SafeBankAppPool already exists.");
            return serverManager.ApplicationPools["SafeBankAppPool"];
        }

        private void DeploySite(ServerManager serverManager, ApplicationPool safeBankAppPool, string safebankFolder)
        {
            if (serverManager.Sites.All(x => x.Name != "SafeBank"))
            {
                AddMessageToProcessLog("Creating SafeBank site");
                var safeBankSite = serverManager.Sites.Add("SafeBank", "http", "*:7233:", safebankFolder);
                safeBankSite.ApplicationDefaults.ApplicationPoolName = safeBankAppPool.Name;
                foreach (var application in safeBankSite.Applications)
                {
                    application.ApplicationPoolName = safeBankAppPool.Name;
                }
                serverManager.CommitChanges();
                _deploymentLog.DeployedSite = true;
                SaveDeploymentLog();
                AddMessageToProcessLog("Created SafeBank site");
            }
            else
            {
                AddMessageToProcessLog("SafeBank site already exists");
            }
        }

        private void DeployCodeToSite(string safeBankFolder)
        {
            if (Directory.GetFileSystemEntries(safeBankFolder).Any())
            {
                var result =
                    MessageBox.Show(
                        "Some things are already in the SafeBank folder do you wan't to download and deploy the latest.",
                        "Code already deployed?", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, false);
                if(result != DialogResult.Yes)
                {
                    AddMessageToProcessLog("No code deployed");
                    return;
                }
                var deleteConent = MessageBox.Show("Can we delete the content in the SafeBank folder?",
                    "Code already deployed?", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, false);
                if(deleteConent == DialogResult.Yes)
                {
                    AddMessageToProcessLog("Deleting all files and folders in SafeBank folder");
                    var folders = Directory.EnumerateDirectories(safeBankFolder);
                    foreach(var folder in folders)
                    {
                        Directory.Delete(folder,true);
                    }
                    var files = Directory.EnumerateFiles(safeBankFolder);
                    foreach (var file in files)
                    {
                        File.Delete(file);
                    }
                    AddMessageToProcessLog("Deleted all files and folders in SafeBank folder");
                }else
                {
                    AddMessageToProcessLog("Although files exist in SafeBanks folder the user did not wan't to delete them");
                }
            }
            AddMessageToProcessLog("Downloading site code");
            var webClient = new WebClient();
            webClient.DownloadFile("https://github.com/TechLiam/SafeBank/raw/SafeBank/SafeBank/Deployment/safebank.zip", "safebank.zip");
            ZipFile.ExtractToDirectory("safebank.zip", safeBankFolder);
            AddMessageToProcessLog("Deployed code to the SafeBank folder");
        }

        private void DeployDatabase()
        {
            AddMessageToProcessLog("Searching for SQL server instances");
            var instances = SqlDataSourceEnumerator.Instance.GetDataSources();
            var sqlServers = (from DataRow instance in instances.Rows
                select new SqlServerData
                {
                    ServerName = instance["ServerName"].ToString(), InstanceName = instance["InstanceName"].ToString(), IsClustered = instance["IsClustered"].ToString(), Version = instance["Version"].ToString()
                }).ToArray();
            if (sqlServers.Any(x => x.SqlVersion() != null))
            {
                SqlServerData sqlServerToUse;
                if (sqlServers.Length > 0)
                {
                    AddMessageToProcessLog("Found " + sqlServers.Length + " sql server instances");
                    var sqlDictKey = 0;
                    var sqlDict = sqlServers.ToDictionary(sqlServerData => sqlDictKey++,
                        sqlServerData => sqlServerData.ConnectionName());
                    var sqlPicked = new SqlServerPicker("Pick a SQL server to use", "SQL servers", sqlDict);
                    var sqlPickedResult = sqlPicked.ShowDialog(this);
                    if (sqlPickedResult == DialogResult.OK)
                    {
                        sqlServerToUse = sqlServers[sqlPicked.Pick.Key];
                    }
                    else
                    {
                        AddMessageToProcessLog("Did not detect a sql server the user wished to use");
                    }
                }
                else
                {
                    sqlServerToUse = sqlServers.Single();
                    AddMessageToProcessLog("SQL instance (" + sqlServerToUse.ConnectionName() + ") detected");
                }
            }
            else
            {
                AddMessageToProcessLog("No SQL servers detected");
            }
        }

        private void CleanUpTermFolders()
        {
            AddMessageToProcessLog("Finishing deployment");
            foreach (var tempFolder in _deploymentLog.TempFolders)
            {
                Directory.Delete(tempFolder, true);
            }
            _deploymentLog.TempFolders.Clear();
            SaveDeploymentLog();
        }

        private void Deploy()
        {

            DeployDatabase();
            return;

            DeployIIS();
            SetUpProgress.PerformStep();

            var safebankFolder = DeployFolders();
            if(safebankFolder == null) return;
            SetUpProgress.PerformStep();

            var serverManager = new ServerManager();
            var safeBankAppPool = DeployAppPool(serverManager);
            SetUpProgress.PerformStep();

            DeploySite(serverManager, safeBankAppPool, safebankFolder);
            SetUpProgress.PerformStep();

            DeployCodeToSite(safebankFolder);
            SetUpProgress.PerformStep();

            CleanUpTermFolders();
            SetUpProgress.PerformStep();

            _deploymentLog.HaveDeployed = true;
            SaveDeploymentLog();
            SetUpProgress.PerformStep();

            AddMessageToProcessLog("Deployment finished");
        }

        private void SafeBankLocalDeploymentToolWIndow_Load(object sender, EventArgs e)
        {
            if (!IsAdministrator())
            {
                MessageBox.Show(Resources.Not_an_Administrator_Error_Text, Resources.Not_an_Administrator_Error_Title,
                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly, false);
                Close();
            }
            if (_deploymentLog.HaveDeployed) return;
            if (_deploymentLog.FailedDeployment())
            {
                MessageBox.Show(
                    "Oh dear it looks like the last deployment done did not finish fully click the deploy button again to fix the problem.",
                    "Bad deployment detected", MessageBoxButtons.OK, MessageBoxIcon.Hand,
                    MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, false);
            }
            UndeployButton.Enabled = false;
        }

        private void DeployButton_Click(object sender, EventArgs e)
        {
            Deploy();
            UndeployButton.Enabled = true;
        }

        private bool UndeployIIS()
        {
            if (_deploymentLog.DeployedIis)
            {
                AddMessageToProcessLog("Diabling IIS");
                if (!DiableIis())
                {
                    AddMessageToProcessLog(
                        "Critical error: Unable to undeploy SafeBank check that the deploymentlog.json has not been changed.");
                    return false;
                }
                _deploymentLog.DeployedIis = false;
                SaveDeploymentLog();
                AddMessageToProcessLog("Diabled IIS");
            }
            if (_deploymentLog.WindowFeaturesDeployed.Any())
            {
                if (!DisableFeature(_deploymentLog.WindowFeaturesDeployed.ToArray(),
                    "Unable to disable {0} as the feature dose not exist, this is probably due to the deployment log been tampered with.",
                    true))
                {
                    AddMessageToProcessLog(
                        "Critical error: Unable to undeploy SafeBank check that the deploymentlog.json has not been changed.");
                    return false;
                }
                _deploymentLog.WindowFeaturesDeployed.Clear();
                SaveDeploymentLog();
                AddMessageToProcessLog("All features turned off that should have been.");
            }
            return true;
        }

        private void UndeployFolders()
        {
            foreach (var createdFolder in _deploymentLog.FoldersCreated)
            {
                AddMessageToProcessLog("Deleting " + createdFolder);
                Directory.Delete(createdFolder);
                AddMessageToProcessLog("Deleted " + createdFolder);
            }
            _deploymentLog.FoldersCreated.Clear();
            SaveDeploymentLog();
            foreach (var tempFolder in _deploymentLog.TempFolders)
            {
                var oldName = tempFolder.Substring(0, (tempFolder.Length - 5));
                AddMessageToProcessLog("Restoring " + oldName);
                if (Directory.Exists(oldName))
                {
                    Directory.Delete(oldName);
                }
                Directory.Move(tempFolder, oldName);
                AddMessageToProcessLog("Restored " + oldName);
            }
            _deploymentLog.TempFolders.Clear();
            SaveDeploymentLog();
            foreach (var movedFolder in _deploymentLog.FoldersMoved)
            {
                AddMessageToProcessLog("Moving back from " + movedFolder.Key + " to " + movedFolder.Value);
                Directory.Move(movedFolder.Key,movedFolder.Value);
                AddMessageToProcessLog("Moved back from " + movedFolder.Key + " to " + movedFolder.Value);
            }
            _deploymentLog.FoldersMoved.Clear();
            SaveDeploymentLog();
        }

        private void UndeployAppPool()
        {
            if (!_deploymentLog.DeployedAppPool) return;
            var serverManager = new ServerManager();
            if (serverManager.ApplicationPools.All(x => x.Name != "SafeBankAppPool")) return;
            AddMessageToProcessLog("Removing SafeBankAppPool");
            var appPool = serverManager.ApplicationPools["SafeBankAppPool"];
            serverManager.ApplicationPools.Remove(appPool);
            serverManager.CommitChanges();
            _deploymentLog.DeployedAppPool = false;
            SaveDeploymentLog();
            AddMessageToProcessLog("Removed SafeBankAppPool");
        }

        private void UndeploySite()
        {
            if (!_deploymentLog.DeployedSite) return;
            var serverManager = new ServerManager();
            if (serverManager.Sites.All(x => x.Name != "SafeBank")) return;
            AddMessageToProcessLog("Removing SafeBank site");
            var site = serverManager.Sites["SafeBank"];
            serverManager.Sites.Remove(site);
            serverManager.CommitChanges();
            _deploymentLog.DeployedSite = false;
            SaveDeploymentLog();
            AddMessageToProcessLog("Removed SafeBank site");
        }

        private void Undeploy()
        {
            GetFeaturesList();
            UndeploySite();
            UndeployAppPool();
            if (!UndeployIIS()) return;
            UndeployFolders();
            _deploymentLog.HaveDeployed = false;
            SaveDeploymentLog();
            AddMessageToProcessLog("All undeployed");
        }

        private void UndeployButton_Click(object sender, EventArgs e)
        {
            Undeploy();
            UndeployButton.Enabled = false;
        }
    }
}
