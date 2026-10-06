namespace ServerCloak2
{
    partial class CloakIPLog
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
            this.CloakList = new MaterialSkin.Controls.MaterialListView();
            this.LockedColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AttacksColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IPColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CreatedColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CountryColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // CloakList
            // 
            this.CloakList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CloakList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IPColumn,
            this.AttacksColumn,
            this.CreatedColumn,
            this.LockedColumn,
            this.CountryColumn});
            this.CloakList.Depth = 0;
            this.CloakList.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.CloakList.FullRowSelect = true;
            this.CloakList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.CloakList.Location = new System.Drawing.Point(1, 63);
            this.CloakList.MouseLocation = new System.Drawing.Point(-1, -1);
            this.CloakList.MouseState = MaterialSkin.MouseState.OUT;
            this.CloakList.Name = "CloakList";
            this.CloakList.OwnerDraw = true;
            this.CloakList.Size = new System.Drawing.Size(612, 432);
            this.CloakList.TabIndex = 1;
            this.CloakList.UseCompatibleStateImageBehavior = false;
            this.CloakList.View = System.Windows.Forms.View.Details;
            // 
            // LockedColumn
            // 
            this.LockedColumn.DisplayIndex = 2;
            this.LockedColumn.Text = "Locked";
            // 
            // AttacksColumn
            // 
            this.AttacksColumn.Text = "Attacks";
            // 
            // IPColumn
            // 
            this.IPColumn.Text = "IP";
            // 
            // CreatedColumn
            // 
            this.CreatedColumn.DisplayIndex = 3;
            this.CreatedColumn.Text = "Created";
            // 
            // CountryColumn
            // 
            this.CountryColumn.Text = "Country";
            // 
            // CloakIPLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 507);
            this.Controls.Add(this.CloakList);
            this.Name = "CloakIPLog";
            this.Text = "CloakIPLog";
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialListView CloakList;
        private System.Windows.Forms.ColumnHeader IPColumn;
        private System.Windows.Forms.ColumnHeader AttacksColumn;
        private System.Windows.Forms.ColumnHeader LockedColumn;
        private System.Windows.Forms.ColumnHeader CreatedColumn;
        private System.Windows.Forms.ColumnHeader CountryColumn;
    }
}