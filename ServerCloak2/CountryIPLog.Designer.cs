namespace ServerCloak2
{
    partial class CountryIPLog
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
            this.CountryColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TotalColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BlockedColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // CloakList
            // 
            this.CloakList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CloakList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CountryColumn,
            this.TotalColumn,
            this.BlockedColumn});
            this.CloakList.Depth = 0;
            this.CloakList.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.CloakList.FullRowSelect = true;
            this.CloakList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.CloakList.Location = new System.Drawing.Point(1, 64);
            this.CloakList.MouseLocation = new System.Drawing.Point(-1, -1);
            this.CloakList.MouseState = MaterialSkin.MouseState.OUT;
            this.CloakList.Name = "CloakList";
            this.CloakList.OwnerDraw = true;
            this.CloakList.Size = new System.Drawing.Size(612, 432);
            this.CloakList.TabIndex = 2;
            this.CloakList.UseCompatibleStateImageBehavior = false;
            this.CloakList.View = System.Windows.Forms.View.Details;
            // 
            // CountryColumn
            // 
            this.CountryColumn.Text = "Country";
            // 
            // TotalColumn
            // 
            this.TotalColumn.Text = "Total Attacks";
            // 
            // BlockedColumn
            // 
            this.BlockedColumn.Text = "Blocked IPs";
            // 
            // CountryIPLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 507);
            this.Controls.Add(this.CloakList);
            this.Name = "CountryIPLog";
            this.Text = "CountryIPLog";
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialListView CloakList;
        private System.Windows.Forms.ColumnHeader CountryColumn;
        private System.Windows.Forms.ColumnHeader TotalColumn;
        private System.Windows.Forms.ColumnHeader BlockedColumn;
    }
}