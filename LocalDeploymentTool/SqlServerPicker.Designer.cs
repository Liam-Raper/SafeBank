namespace LocalDeploymentTool
{
    partial class SqlServerPicker
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
            this.List = new System.Windows.Forms.ComboBox();
            this.Message = new System.Windows.Forms.Label();
            this.PickButton = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // List
            // 
            this.List.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.List.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.List.FormattingEnabled = true;
            this.List.Location = new System.Drawing.Point(15, 25);
            this.List.Name = "List";
            this.List.Size = new System.Drawing.Size(457, 21);
            this.List.TabIndex = 0;
            // 
            // Message
            // 
            this.Message.AutoSize = true;
            this.Message.Location = new System.Drawing.Point(12, 9);
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(76, 13);
            this.Message.TabIndex = 1;
            this.Message.Text = "SQL instances";
            // 
            // PickButton
            // 
            this.PickButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PickButton.Location = new System.Drawing.Point(315, 55);
            this.PickButton.Name = "PickButton";
            this.PickButton.Size = new System.Drawing.Size(75, 23);
            this.PickButton.TabIndex = 2;
            this.PickButton.Text = "Pick";
            this.PickButton.UseVisualStyleBackColor = true;
            this.PickButton.Click += new System.EventHandler(this.PickButton_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.Location = new System.Drawing.Point(397, 55);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 3;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // SqlServerPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 86);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.PickButton);
            this.Controls.Add(this.Message);
            this.Controls.Add(this.List);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(500, 125);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(200, 125);
            this.Name = "SqlServerPicker";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SqlServerPicker";
            this.Load += new System.EventHandler(this.SqlServerPicker_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox List;
        private System.Windows.Forms.Label Message;
        private System.Windows.Forms.Button PickButton;
        private System.Windows.Forms.Button CancelBtn;
    }
}