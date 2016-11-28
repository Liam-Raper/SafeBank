namespace LocalDeploymentTool
{
    partial class SafeBankLocalDeploymentToolWIndow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SafeBankLocalDeploymentToolWIndow));
            this.DescriptionText = new System.Windows.Forms.Label();
            this.WhatShouldHappenText = new System.Windows.Forms.Label();
            this.DeployButton = new System.Windows.Forms.Button();
            this.SetUpProgress = new System.Windows.Forms.ProgressBar();
            this.DeploymentProcessLog = new System.Windows.Forms.RichTextBox();
            this.UndeployButton = new System.Windows.Forms.Button();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // DescriptionText
            // 
            this.DescriptionText.AutoSize = true;
            this.DescriptionText.Location = new System.Drawing.Point(12, 9);
            this.DescriptionText.Name = "DescriptionText";
            this.DescriptionText.Size = new System.Drawing.Size(432, 52);
            this.DescriptionText.TabIndex = 1;
            this.DescriptionText.Text = resources.GetString("DescriptionText.Text");
            // 
            // WhatShouldHappenText
            // 
            this.WhatShouldHappenText.AutoSize = true;
            this.WhatShouldHappenText.Location = new System.Drawing.Point(12, 80);
            this.WhatShouldHappenText.Name = "WhatShouldHappenText";
            this.WhatShouldHappenText.Size = new System.Drawing.Size(359, 91);
            this.WhatShouldHappenText.TabIndex = 2;
            this.WhatShouldHappenText.Text = resources.GetString("WhatShouldHappenText.Text");
            // 
            // DeployButton
            // 
            this.DeployButton.Location = new System.Drawing.Point(15, 203);
            this.DeployButton.Name = "DeployButton";
            this.DeployButton.Size = new System.Drawing.Size(457, 23);
            this.DeployButton.TabIndex = 3;
            this.DeployButton.Text = "Deploy SafeBank";
            this.DeployButton.UseVisualStyleBackColor = true;
            this.DeployButton.Click += new System.EventHandler(this.DeployButton_Click);
            // 
            // SetUpProgress
            // 
            this.SetUpProgress.Location = new System.Drawing.Point(15, 174);
            this.SetUpProgress.Maximum = 6;
            this.SetUpProgress.Name = "SetUpProgress";
            this.SetUpProgress.Size = new System.Drawing.Size(457, 23);
            this.SetUpProgress.Step = 1;
            this.SetUpProgress.TabIndex = 4;
            // 
            // DeploymentProcessLog
            // 
            this.DeploymentProcessLog.BackColor = System.Drawing.SystemColors.ControlText;
            this.DeploymentProcessLog.ForeColor = System.Drawing.Color.LightGreen;
            this.DeploymentProcessLog.Location = new System.Drawing.Point(15, 261);
            this.DeploymentProcessLog.Name = "DeploymentProcessLog";
            this.DeploymentProcessLog.ReadOnly = true;
            this.DeploymentProcessLog.Size = new System.Drawing.Size(457, 188);
            this.DeploymentProcessLog.TabIndex = 5;
            this.DeploymentProcessLog.Text = "";
            // 
            // UndeployButton
            // 
            this.UndeployButton.Location = new System.Drawing.Point(15, 232);
            this.UndeployButton.Name = "UndeployButton";
            this.UndeployButton.Size = new System.Drawing.Size(457, 23);
            this.UndeployButton.TabIndex = 6;
            this.UndeployButton.Text = "Undeploy SafeBank";
            this.UndeployButton.UseVisualStyleBackColor = true;
            this.UndeployButton.Click += new System.EventHandler(this.UndeployButton_Click);
            // 
            // folderBrowser
            // 
            this.folderBrowser.Description = "Select IIS\'s inetput folder and click ok. Closing or canceling the window will ro" +
    "llback the deployment.";
            this.folderBrowser.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.folderBrowser.ShowNewFolderButton = false;
            // 
            // SafeBankLocalDeploymentToolWIndow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.UndeployButton);
            this.Controls.Add(this.DeploymentProcessLog);
            this.Controls.Add(this.SetUpProgress);
            this.Controls.Add(this.DeployButton);
            this.Controls.Add(this.WhatShouldHappenText);
            this.Controls.Add(this.DescriptionText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SafeBankLocalDeploymentToolWIndow";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SafeBank Local deployment tool";
            this.Load += new System.EventHandler(this.SafeBankLocalDeploymentToolWIndow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label DescriptionText;
        private System.Windows.Forms.Label WhatShouldHappenText;
        private System.Windows.Forms.Button DeployButton;
        private System.Windows.Forms.ProgressBar SetUpProgress;
        private System.Windows.Forms.RichTextBox DeploymentProcessLog;
        private System.Windows.Forms.Button UndeployButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
    }
}

