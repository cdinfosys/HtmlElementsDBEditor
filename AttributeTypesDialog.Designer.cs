namespace HtmlElementsDBEditor
{
    partial class AttributeTypesDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AttributeTypesDialog));
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            this.attributeTypesListView = new System.Windows.Forms.ListView();
            this.typeColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.addButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Name = "okButton";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // applyButton
            // 
            resources.ApplyResources(this.applyButton, "applyButton");
            this.applyButton.Name = "applyButton";
            this.applyButton.UseVisualStyleBackColor = true;
            // 
            // attributeTypesListView
            // 
            this.attributeTypesListView.AllowColumnReorder = true;
            resources.ApplyResources(this.attributeTypesListView, "attributeTypesListView");
            this.attributeTypesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.typeColumn});
            this.attributeTypesListView.FullRowSelect = true;
            this.attributeTypesListView.LabelEdit = true;
            this.attributeTypesListView.Name = "attributeTypesListView";
            this.attributeTypesListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.attributeTypesListView.UseCompatibleStateImageBehavior = false;
            this.attributeTypesListView.View = System.Windows.Forms.View.Details;
            // 
            // typeColumn
            // 
            resources.ApplyResources(this.typeColumn, "typeColumn");
            // 
            // addButton
            // 
            resources.ApplyResources(this.addButton, "addButton");
            this.addButton.Name = "addButton";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.OnAddButtonClicked);
            // 
            // editButton
            // 
            resources.ApplyResources(this.editButton, "editButton");
            this.editButton.Name = "editButton";
            this.editButton.UseVisualStyleBackColor = true;
            // 
            // deleteButton
            // 
            resources.ApplyResources(this.deleteButton, "deleteButton");
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.UseVisualStyleBackColor = true;
            // 
            // AttributeTypesDialog
            // 
            this.AcceptButton = this.okButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.attributeTypesListView);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AttributeTypesDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.ListView attributeTypesListView;
        private System.Windows.Forms.ColumnHeader typeColumn;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button deleteButton;
    }
}