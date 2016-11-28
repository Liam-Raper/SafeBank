using System.Collections.Generic;
using System.Linq;

namespace LocalDeploymentTool
{
    public class DeploymentLog
    {

        public bool HaveDeployed = false;
        public bool DeployedIis = false;
        public bool DeployedAppPool = false;
        public bool DeployedSite = false;
        public List<string> WindowFeaturesDeployed = new List<string>();
        public List<string> FoldersCreated = new List<string>();
        public Dictionary<string,string> FoldersMoved = new Dictionary<string, string>();
        public List<string> TempFolders = new List<string>();

        public bool FailedDeployment()
        {
            if (HaveDeployed) return false;
            return DeployedIis || WindowFeaturesDeployed.Any() || FoldersCreated.Any() || FoldersMoved.Any() || TempFolders.Any();
        }

    }
}
