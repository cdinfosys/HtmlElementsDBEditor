namespace HtmlElementsDBEditor
{
    partial class FrameWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrameWindow));
            this.menuBar = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.attributeTypesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cssPropertyTypesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.statusBarLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.clientAreaPanel = new System.Windows.Forms.Panel();
            this.menuBar.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuBar
            // 
            this.menuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.toolsToolStripMenuItem,
            this.helpMenu});
            resources.ApplyResources(this.menuBar, "menuBar");
            this.menuBar.Name = "menuBar";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMenuItem,
            this.openMenuItem,
            this.saveMenuItem,
            this.saveAsMenuItem,
            this.closeMenuItem,
            this.toolStripMenuItem1,
            this.exitMenuItem});
            this.fileMenu.Name = "fileMenu";
            resources.ApplyResources(this.fileMenu, "fileMenu");
            // 
            // newMenuItem
            // 
            this.newMenuItem.Name = "newMenuItem";
            resources.ApplyResources(this.newMenuItem, "newMenuItem");
            this.newMenuItem.Click += new System.EventHandler(this.OnNewClicked);
            // 
            // openMenuItem
            // 
            this.openMenuItem.Name = "openMenuItem";
            resources.ApplyResources(this.openMenuItem, "openMenuItem");
            this.openMenuItem.Click += new System.EventHandler(this.OnOpenClicked);
            // 
            // saveMenuItem
            // 
            resources.ApplyResources(this.saveMenuItem, "saveMenuItem");
            this.saveMenuItem.Name = "saveMenuItem";
            this.saveMenuItem.Click += new System.EventHandler(this.OnSaveClicked);
            // 
            // saveAsMenuItem
            // 
            resources.ApplyResources(this.saveAsMenuItem, "saveAsMenuItem");
            this.saveAsMenuItem.Name = "saveAsMenuItem";
            this.saveAsMenuItem.Click += new System.EventHandler(this.OnSaveAsClicked);
            // 
            // closeMenuItem
            // 
            resources.ApplyResources(this.closeMenuItem, "closeMenuItem");
            this.closeMenuItem.Name = "closeMenuItem";
            this.closeMenuItem.Click += new System.EventHandler(this.OnCloseClicked);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            resources.ApplyResources(this.exitMenuItem, "exitMenuItem");
            this.exitMenuItem.Click += new System.EventHandler(this.OnExitClicked);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.attributeTypesMenuItem,
            this.cssPropertyTypesMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            resources.ApplyResources(this.toolsToolStripMenuItem, "toolsToolStripMenuItem");
            // 
            // attributeTypesMenuItem
            // 
            resources.ApplyResources(this.attributeTypesMenuItem, "attributeTypesMenuItem");
            this.attributeTypesMenuItem.Name = "attributeTypesMenuItem";
            this.attributeTypesMenuItem.Click += new System.EventHandler(this.OnAttributeTypesClicked);
            // 
            // cssPropertyTypesMenuItem
            // 
            resources.ApplyResources(this.cssPropertyTypesMenuItem, "cssPropertyTypesMenuItem");
            this.cssPropertyTypesMenuItem.Name = "cssPropertyTypesMenuItem";
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutMenuItem});
            this.helpMenu.Name = "helpMenu";
            resources.ApplyResources(this.helpMenu, "helpMenu");
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Name = "aboutMenuItem";
            resources.ApplyResources(this.aboutMenuItem, "aboutMenuItem");
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBarLabel});
            resources.ApplyResources(this.statusBar, "statusBar");
            this.statusBar.Name = "statusBar";
            // 
            // statusBarLabel
            // 
            resources.ApplyResources(this.statusBarLabel, "statusBarLabel");
            this.statusBarLabel.Name = "statusBarLabel";
            this.statusBarLabel.Spring = true;
            // 
            // clientAreaPanel
            // 
            this.clientAreaPanel.BackColor = System.Drawing.SystemColors.Window;
            this.clientAreaPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.clientAreaPanel, "clientAreaPanel");
            this.clientAreaPanel.Name = "clientAreaPanel";
            // 
            // FrameWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.clientAreaPanel);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.menuBar);
            this.MainMenuStrip = this.menuBar;
            this.Name = "FrameWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.menuBar.ResumeLayout(false);
            this.menuBar.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuBar;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel statusBarLabel;
        private System.Windows.Forms.ToolStripMenuItem openMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newMenuItem;
        private System.Windows.Forms.Panel clientAreaPanel;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem attributeTypesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cssPropertyTypesMenuItem;
    }
}

