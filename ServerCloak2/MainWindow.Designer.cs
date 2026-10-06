namespace ServerCloak2
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            this.materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.CloakList = new MaterialSkin.Controls.MaterialListView();
            this.Cloak = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Port = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Attacks = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Locked = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.EditCancelWhitelist = new MaterialSkin.Controls.MaterialFlatButton();
            this.SaveWhitelistButton = new MaterialSkin.Controls.MaterialRaisedButton();
            this.Whitelist = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.BlockForLabel = new MaterialSkin.Controls.MaterialLabel();
            this.EditCancelLimitButton = new MaterialSkin.Controls.MaterialFlatButton();
            this.SaveLimitButton = new MaterialSkin.Controls.MaterialRaisedButton();
            this.AttackPanel = new System.Windows.Forms.Panel();
            this.LimitLabel = new MaterialSkin.Controls.MaterialLabel();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.EditCancelPortButton = new MaterialSkin.Controls.MaterialFlatButton();
            this.SavePortButton = new MaterialSkin.Controls.MaterialRaisedButton();
            this.PortLabel = new MaterialSkin.Controls.MaterialLabel();
            this.PortPanel = new System.Windows.Forms.Panel();
            this.SmtpPortLabel = new MaterialSkin.Controls.MaterialLabel();
            this.RdpPortLabel = new MaterialSkin.Controls.MaterialLabel();
            this.FtpPortLabel = new MaterialSkin.Controls.MaterialLabel();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.InstallServiceButton = new MaterialSkin.Controls.MaterialRaisedButton();
            this.StartServiceButton = new MaterialSkin.Controls.MaterialRaisedButton();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.UnblockIP = new MaterialSkin.Controls.MaterialRaisedButton();
            this.UnblockLabel = new MaterialSkin.Controls.MaterialLabel();
            this.UnblockTextBox = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.materialTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.PortPanel.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialTabSelector1
            // 
            this.materialTabSelector1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialTabSelector1.BaseTabControl = this.materialTabControl1;
            this.materialTabSelector1.Depth = 0;
            this.materialTabSelector1.Location = new System.Drawing.Point(0, 63);
            this.materialTabSelector1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector1.Name = "materialTabSelector1";
            this.materialTabSelector1.Size = new System.Drawing.Size(961, 48);
            this.materialTabSelector1.TabIndex = 18;
            this.materialTabSelector1.Text = "materialTabSelector1";
            // 
            // materialTabControl1
            // 
            this.materialTabControl1.Controls.Add(this.tabPage1);
            this.materialTabControl1.Controls.Add(this.tabPage2);
            this.materialTabControl1.Controls.Add(this.tabPage3);
            this.materialTabControl1.Controls.Add(this.tabPage4);
            this.materialTabControl1.Controls.Add(this.tabPage5);
            this.materialTabControl1.Controls.Add(this.tabPage6);
            this.materialTabControl1.Depth = 0;
            this.materialTabControl1.Location = new System.Drawing.Point(2, 112);
            this.materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(635, 452);
            this.materialTabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.CloakList);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(627, 426);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Cloaking";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // CloakList
            // 
            this.CloakList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CloakList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Cloak,
            this.Port,
            this.Attacks,
            this.Locked});
            this.CloakList.Depth = 0;
            this.CloakList.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.CloakList.FullRowSelect = true;
            this.CloakList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.CloakList.Location = new System.Drawing.Point(0, 0);
            this.CloakList.MouseLocation = new System.Drawing.Point(-1, -1);
            this.CloakList.MouseState = MaterialSkin.MouseState.OUT;
            this.CloakList.Name = "CloakList";
            this.CloakList.OwnerDraw = true;
            this.CloakList.Size = new System.Drawing.Size(627, 419);
            this.CloakList.TabIndex = 0;
            this.CloakList.UseCompatibleStateImageBehavior = false;
            this.CloakList.View = System.Windows.Forms.View.Details;
            this.CloakList.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.CloakList_ItemSelectionChanged);
            // 
            // Cloak
            // 
            this.Cloak.Text = "Cloak";
            this.Cloak.Width = 79;
            // 
            // Port
            // 
            this.Port.Text = "Port";
            // 
            // Attacks
            // 
            this.Attacks.Text = "Attacks";
            // 
            // Locked
            // 
            this.Locked.Text = "Locked";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.EditCancelWhitelist);
            this.tabPage2.Controls.Add(this.SaveWhitelistButton);
            this.tabPage2.Controls.Add(this.Whitelist);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(627, 426);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "Whitelist";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(209, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "White List IP Adresses (Comma Separated)";
            // 
            // EditCancelWhitelist
            // 
            this.EditCancelWhitelist.AutoSize = true;
            this.EditCancelWhitelist.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.EditCancelWhitelist.BackColor = System.Drawing.Color.Black;
            this.EditCancelWhitelist.Depth = 0;
            this.EditCancelWhitelist.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.EditCancelWhitelist.Icon = null;
            this.EditCancelWhitelist.Location = new System.Drawing.Point(483, 384);
            this.EditCancelWhitelist.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.EditCancelWhitelist.MouseState = MaterialSkin.MouseState.HOVER;
            this.EditCancelWhitelist.Name = "EditCancelWhitelist";
            this.EditCancelWhitelist.Primary = false;
            this.EditCancelWhitelist.Size = new System.Drawing.Size(73, 36);
            this.EditCancelWhitelist.TabIndex = 2;
            this.EditCancelWhitelist.Text = "Cancel";
            this.EditCancelWhitelist.UseVisualStyleBackColor = false;
            this.EditCancelWhitelist.Click += new System.EventHandler(this.materialFlatButton1_Click);
            // 
            // SaveWhitelistButton
            // 
            this.SaveWhitelistButton.AutoSize = true;
            this.SaveWhitelistButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SaveWhitelistButton.Depth = 0;
            this.SaveWhitelistButton.Icon = null;
            this.SaveWhitelistButton.Location = new System.Drawing.Point(346, 384);
            this.SaveWhitelistButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.SaveWhitelistButton.Name = "SaveWhitelistButton";
            this.SaveWhitelistButton.Primary = true;
            this.SaveWhitelistButton.Size = new System.Drawing.Size(113, 36);
            this.SaveWhitelistButton.TabIndex = 1;
            this.SaveWhitelistButton.Text = "Save & Apply";
            this.SaveWhitelistButton.UseVisualStyleBackColor = true;
            this.SaveWhitelistButton.Click += new System.EventHandler(this.materialRaisedButton1_Click);
            // 
            // Whitelist
            // 
            this.Whitelist.Location = new System.Drawing.Point(0, 33);
            this.Whitelist.Multiline = true;
            this.Whitelist.Name = "Whitelist";
            this.Whitelist.Size = new System.Drawing.Size(624, 345);
            this.Whitelist.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.BlockForLabel);
            this.tabPage3.Controls.Add(this.EditCancelLimitButton);
            this.tabPage3.Controls.Add(this.SaveLimitButton);
            this.tabPage3.Controls.Add(this.AttackPanel);
            this.tabPage3.Controls.Add(this.LimitLabel);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(627, 426);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "Attack Limit";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // BlockForLabel
            // 
            this.BlockForLabel.AutoSize = true;
            this.BlockForLabel.Depth = 0;
            this.BlockForLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.BlockForLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BlockForLabel.Location = new System.Drawing.Point(496, 14);
            this.BlockForLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.BlockForLabel.Name = "BlockForLabel";
            this.BlockForLabel.Size = new System.Drawing.Size(73, 19);
            this.BlockForLabel.TabIndex = 5;
            this.BlockForLabel.Text = "Block For";
            // 
            // EditCancelLimitButton
            // 
            this.EditCancelLimitButton.AutoSize = true;
            this.EditCancelLimitButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.EditCancelLimitButton.BackColor = System.Drawing.Color.Black;
            this.EditCancelLimitButton.Depth = 0;
            this.EditCancelLimitButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.EditCancelLimitButton.Icon = null;
            this.EditCancelLimitButton.Location = new System.Drawing.Point(483, 384);
            this.EditCancelLimitButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.EditCancelLimitButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.EditCancelLimitButton.Name = "EditCancelLimitButton";
            this.EditCancelLimitButton.Primary = false;
            this.EditCancelLimitButton.Size = new System.Drawing.Size(73, 36);
            this.EditCancelLimitButton.TabIndex = 4;
            this.EditCancelLimitButton.Text = "Cancel";
            this.EditCancelLimitButton.UseVisualStyleBackColor = false;
            this.EditCancelLimitButton.Click += new System.EventHandler(this.EditCancelLimitButton_Click);
            // 
            // SaveLimitButton
            // 
            this.SaveLimitButton.AutoSize = true;
            this.SaveLimitButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SaveLimitButton.Depth = 0;
            this.SaveLimitButton.Icon = null;
            this.SaveLimitButton.Location = new System.Drawing.Point(346, 384);
            this.SaveLimitButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.SaveLimitButton.Name = "SaveLimitButton";
            this.SaveLimitButton.Primary = true;
            this.SaveLimitButton.Size = new System.Drawing.Size(113, 36);
            this.SaveLimitButton.TabIndex = 3;
            this.SaveLimitButton.Text = "Save & Apply";
            this.SaveLimitButton.UseVisualStyleBackColor = true;
            this.SaveLimitButton.Click += new System.EventHandler(this.SaveLimitButton_Click);
            // 
            // AttackPanel
            // 
            this.AttackPanel.Location = new System.Drawing.Point(8, 47);
            this.AttackPanel.Name = "AttackPanel";
            this.AttackPanel.Size = new System.Drawing.Size(610, 325);
            this.AttackPanel.TabIndex = 2;
            // 
            // LimitLabel
            // 
            this.LimitLabel.AutoSize = true;
            this.LimitLabel.Depth = 0;
            this.LimitLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.LimitLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LimitLabel.Location = new System.Drawing.Point(382, 14);
            this.LimitLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.LimitLabel.Name = "LimitLabel";
            this.LimitLabel.Size = new System.Drawing.Size(43, 19);
            this.LimitLabel.TabIndex = 1;
            this.LimitLabel.Text = "Limit";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.EditCancelPortButton);
            this.tabPage4.Controls.Add(this.SavePortButton);
            this.tabPage4.Controls.Add(this.PortLabel);
            this.tabPage4.Controls.Add(this.PortPanel);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(627, 426);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Port";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // EditCancelPortButton
            // 
            this.EditCancelPortButton.AutoSize = true;
            this.EditCancelPortButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.EditCancelPortButton.BackColor = System.Drawing.Color.Black;
            this.EditCancelPortButton.Depth = 0;
            this.EditCancelPortButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.EditCancelPortButton.Icon = null;
            this.EditCancelPortButton.Location = new System.Drawing.Point(478, 195);
            this.EditCancelPortButton.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.EditCancelPortButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.EditCancelPortButton.Name = "EditCancelPortButton";
            this.EditCancelPortButton.Primary = false;
            this.EditCancelPortButton.Size = new System.Drawing.Size(73, 36);
            this.EditCancelPortButton.TabIndex = 6;
            this.EditCancelPortButton.Text = "Cancel";
            this.EditCancelPortButton.UseVisualStyleBackColor = false;
            this.EditCancelPortButton.Click += new System.EventHandler(this.EditCancelPortButton_Click);
            // 
            // SavePortButton
            // 
            this.SavePortButton.AutoSize = true;
            this.SavePortButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SavePortButton.Depth = 0;
            this.SavePortButton.Icon = null;
            this.SavePortButton.Location = new System.Drawing.Point(341, 195);
            this.SavePortButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.SavePortButton.Name = "SavePortButton";
            this.SavePortButton.Primary = true;
            this.SavePortButton.Size = new System.Drawing.Size(113, 36);
            this.SavePortButton.TabIndex = 5;
            this.SavePortButton.Text = "Save & Apply";
            this.SavePortButton.UseVisualStyleBackColor = true;
            this.SavePortButton.Click += new System.EventHandler(this.SavePortButton_Click);
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Depth = 0;
            this.PortLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.PortLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.PortLabel.Location = new System.Drawing.Point(380, 14);
            this.PortLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(37, 19);
            this.PortLabel.TabIndex = 4;
            this.PortLabel.Text = "Port";
            // 
            // PortPanel
            // 
            this.PortPanel.Controls.Add(this.SmtpPortLabel);
            this.PortPanel.Controls.Add(this.RdpPortLabel);
            this.PortPanel.Controls.Add(this.FtpPortLabel);
            this.PortPanel.Location = new System.Drawing.Point(8, 47);
            this.PortPanel.Name = "PortPanel";
            this.PortPanel.Size = new System.Drawing.Size(610, 128);
            this.PortPanel.TabIndex = 3;
            // 
            // SmtpPortLabel
            // 
            this.SmtpPortLabel.AutoSize = true;
            this.SmtpPortLabel.Depth = 0;
            this.SmtpPortLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.SmtpPortLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.SmtpPortLabel.Location = new System.Drawing.Point(17, 76);
            this.SmtpPortLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.SmtpPortLabel.Name = "SmtpPortLabel";
            this.SmtpPortLabel.Size = new System.Drawing.Size(44, 19);
            this.SmtpPortLabel.TabIndex = 5;
            this.SmtpPortLabel.Text = "Smtp";
            // 
            // RdpPortLabel
            // 
            this.RdpPortLabel.AutoSize = true;
            this.RdpPortLabel.Depth = 0;
            this.RdpPortLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.RdpPortLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.RdpPortLabel.Location = new System.Drawing.Point(17, 38);
            this.RdpPortLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.RdpPortLabel.Name = "RdpPortLabel";
            this.RdpPortLabel.Size = new System.Drawing.Size(121, 19);
            this.RdpPortLabel.TabIndex = 4;
            this.RdpPortLabel.Text = "Remote Desktop";
            // 
            // FtpPortLabel
            // 
            this.FtpPortLabel.AutoSize = true;
            this.FtpPortLabel.Depth = 0;
            this.FtpPortLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.FtpPortLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FtpPortLabel.Location = new System.Drawing.Point(17, 0);
            this.FtpPortLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.FtpPortLabel.Name = "FtpPortLabel";
            this.FtpPortLabel.Size = new System.Drawing.Size(93, 19);
            this.FtpPortLabel.TabIndex = 3;
            this.FtpPortLabel.Text = "File Transfer";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.InstallServiceButton);
            this.tabPage5.Controls.Add(this.StartServiceButton);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(627, 426);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Service";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // InstallServiceButton
            // 
            this.InstallServiceButton.AutoSize = true;
            this.InstallServiceButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.InstallServiceButton.Depth = 0;
            this.InstallServiceButton.Icon = null;
            this.InstallServiceButton.Location = new System.Drawing.Point(379, 50);
            this.InstallServiceButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.InstallServiceButton.Name = "InstallServiceButton";
            this.InstallServiceButton.Primary = true;
            this.InstallServiceButton.Size = new System.Drawing.Size(133, 36);
            this.InstallServiceButton.TabIndex = 1;
            this.InstallServiceButton.Text = "Install Service";
            this.InstallServiceButton.UseVisualStyleBackColor = true;
            this.InstallServiceButton.Click += new System.EventHandler(this.InstallServiceButton_Click);
            // 
            // StartServiceButton
            // 
            this.StartServiceButton.AutoSize = true;
            this.StartServiceButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.StartServiceButton.Depth = 0;
            this.StartServiceButton.Icon = null;
            this.StartServiceButton.Location = new System.Drawing.Point(85, 50);
            this.StartServiceButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.StartServiceButton.Name = "StartServiceButton";
            this.StartServiceButton.Primary = true;
            this.StartServiceButton.Size = new System.Drawing.Size(121, 36);
            this.StartServiceButton.TabIndex = 0;
            this.StartServiceButton.Text = "Start Service";
            this.StartServiceButton.UseVisualStyleBackColor = true;
            this.StartServiceButton.Click += new System.EventHandler(this.StartServiceButton_Click);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.UnblockIP);
            this.tabPage6.Controls.Add(this.UnblockLabel);
            this.tabPage6.Controls.Add(this.UnblockTextBox);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(627, 426);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Unblock IP";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // UnblockIP
            // 
            this.UnblockIP.AutoSize = true;
            this.UnblockIP.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.UnblockIP.Depth = 0;
            this.UnblockIP.Icon = null;
            this.UnblockIP.Location = new System.Drawing.Point(236, 72);
            this.UnblockIP.MouseState = MaterialSkin.MouseState.HOVER;
            this.UnblockIP.Name = "UnblockIP";
            this.UnblockIP.Primary = true;
            this.UnblockIP.Size = new System.Drawing.Size(118, 36);
            this.UnblockIP.TabIndex = 2;
            this.UnblockIP.Text = "Unblock Now";
            this.UnblockIP.UseVisualStyleBackColor = true;
            this.UnblockIP.Click += new System.EventHandler(this.UnblockIP_Click);
            // 
            // UnblockLabel
            // 
            this.UnblockLabel.AutoSize = true;
            this.UnblockLabel.Depth = 0;
            this.UnblockLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.UnblockLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.UnblockLabel.Location = new System.Drawing.Point(46, 37);
            this.UnblockLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.UnblockLabel.Name = "UnblockLabel";
            this.UnblockLabel.Size = new System.Drawing.Size(81, 19);
            this.UnblockLabel.TabIndex = 1;
            this.UnblockLabel.Text = "IP Address";
            // 
            // UnblockTextBox
            // 
            this.UnblockTextBox.Location = new System.Drawing.Point(156, 36);
            this.UnblockTextBox.Name = "UnblockTextBox";
            this.UnblockTextBox.Size = new System.Drawing.Size(263, 20);
            this.UnblockTextBox.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 600000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 566);
            this.Controls.Add(this.materialTabControl1);
            this.Controls.Add(this.materialTabSelector1);
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.ShowIcon = false;
            this.Text = "ServerCloak";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.materialTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.PortPanel.ResumeLayout(false);
            this.PortPanel.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private MaterialSkin.Controls.MaterialListView CloakList;
        private System.Windows.Forms.ColumnHeader Cloak;
        private System.Windows.Forms.ColumnHeader Port;
        private System.Windows.Forms.ColumnHeader Attacks;
        private System.Windows.Forms.ColumnHeader Locked;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox Whitelist;
        private MaterialSkin.Controls.MaterialRaisedButton SaveWhitelistButton;
        private MaterialSkin.Controls.MaterialFlatButton EditCancelWhitelist;
        private MaterialSkin.Controls.MaterialLabel LimitLabel;
        private System.Windows.Forms.Panel AttackPanel;
        private System.Windows.Forms.TabPage tabPage4;
        private MaterialSkin.Controls.MaterialFlatButton EditCancelLimitButton;
        private MaterialSkin.Controls.MaterialRaisedButton SaveLimitButton;
        private MaterialSkin.Controls.MaterialLabel PortLabel;
        private System.Windows.Forms.Panel PortPanel;
        private MaterialSkin.Controls.MaterialLabel SmtpPortLabel;
        private MaterialSkin.Controls.MaterialLabel RdpPortLabel;
        private MaterialSkin.Controls.MaterialLabel FtpPortLabel;
        private MaterialSkin.Controls.MaterialFlatButton EditCancelPortButton;
        private MaterialSkin.Controls.MaterialRaisedButton SavePortButton;
        private System.Windows.Forms.TabPage tabPage5;
        private MaterialSkin.Controls.MaterialRaisedButton InstallServiceButton;
        private MaterialSkin.Controls.MaterialRaisedButton StartServiceButton;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Label label2;
        private MaterialSkin.Controls.MaterialLabel UnblockLabel;
        private System.Windows.Forms.TextBox UnblockTextBox;
        private MaterialSkin.Controls.MaterialRaisedButton UnblockIP;
        private MaterialSkin.Controls.MaterialLabel BlockForLabel;

    }
}

