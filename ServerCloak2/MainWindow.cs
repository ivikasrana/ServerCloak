using ServerCloakData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin;

namespace ServerCloak2
{
    public partial class MainWindow : MaterialForm
    {
        public static Font PageFont = new System.Drawing.Font("Roboto", 10F);
        public MainWindow()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);

            ResizeListView();

            ServerCloak.Initialize();

            foreach (DataRow row in ServerCloak.GetCloaks().Rows)
            {
                var label = new MaterialLabel()
                {
                    Text = Convert.ToString(row["Name"]),
                    Top = AttackPanel.Controls.Count * 12,
                    Width = 180,
                    Font = PageFont
                };

                var txtLimit = new MaterialSingleLineTextField()
                {
                    Name = Convert.ToString(row["ID"]),
                    Text = Convert.ToString(row["Limit"]),
                    Top = AttackPanel.Controls.Count * 12,
                    Left = LimitLabel.Left - 5,
                    Width = 80,
                    MaxLength = 3,
                    Font = PageFont
                };

                ComboBox cbx = new ComboBox()
                {
                    DropDownStyle = ComboBoxStyle.DropDown,
                    Name = "cbx" + Convert.ToString(row["ID"]),
                    Top = AttackPanel.Controls.Count * 12,
                    Left = BlockForLabel.Left - 5,
                    Width = 90,
                    Font = PageFont
                };

                cbx.Items.Add(new ComboboxItem(Convert.ToString(BlockDurationEnum.Permanent), (int)BlockDurationEnum.Permanent));
                cbx.Items.Add(new ComboboxItem(Convert.ToString(BlockDurationEnum.Day), (int)BlockDurationEnum.Day));
                cbx.Items.Add(new ComboboxItem(Convert.ToString(BlockDurationEnum.Week), (int)BlockDurationEnum.Week));
                cbx.Items.Add(new ComboboxItem(Convert.ToString(BlockDurationEnum.Month), (int)BlockDurationEnum.Month));
                cbx.Items.Add(new ComboboxItem(Convert.ToString(BlockDurationEnum.Month3), (int)BlockDurationEnum.Month3));
                cbx.Items.Add(new ComboboxItem(Convert.ToString(BlockDurationEnum.Year), (int)BlockDurationEnum.Year));
                cbx.SelectedItem = cbx.Items.OfType<ComboboxItem>().Where(x => x.Value.ToType<int>() == row["BlockDuration"].ToType<int>()).FirstOrDefault();

                AttackPanel.Controls.Add(label);
                AttackPanel.Controls.Add(txtLimit);
                AttackPanel.Controls.Add(cbx);

                switch ((CloakEnum)Enum.Parse(typeof(CloakEnum), Convert.ToString(row["Name"])))
                {
                    case CloakEnum.Ftp:
                    case CloakEnum.Rdp:
                    case CloakEnum.Smtp:
                        var txtPort = new MaterialSingleLineTextField()
                        {
                            Name = Convert.ToString(row["ID"]),
                            Text = Convert.ToString(row["Port"]),
                            Top = PortPanel.Controls.OfType<MaterialSingleLineTextField>().Count() * 38,
                            Left = PortLabel.Left - 5,
                            Width = 80,
                            MaxLength = 5,
                            Font = PageFont
                        };
                        PortPanel.Controls.Add(txtPort);
                        break;
                }
            }

            ServerCloak.Initialize();

            //var data = new AttackDetectedEventArgs()
            //{
            //    CloakName = CloakEnum.Rdp,
            //    CreateDate = DateTime.Now,
            //    EventId = 12345,
            //    EventMessage = string.Empty,
            //    IpAddress = "192.168.10.10"
            //};
            //ServerCloak.BlockIP(data);
            //FirewallPolicyManager.Instance.Block(data.IpAddress);
        }

        private void ResizeListView()
        {
            Whitelist.Width = CloakList.Width = this.Width - 2;
            CloakList.Columns[0].Width = (int)(CloakList.Width * .4m);
            CloakList.Columns[1].Width = (int)(CloakList.Width * .2m);
            CloakList.Columns[2].Width = (int)(CloakList.Width * .2m);
            CloakList.Columns[3].Width = (int)(CloakList.Width * .2m);
            CloakList.Font = PageFont;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(ServerCloak.WhiteListFile))
                Whitelist.Text = File.ReadAllText(ServerCloak.WhiteListFile);

            var serviceStatus = ServiceInstaller.GetServiceStatus(ServiceName);
            if (serviceStatus == ServiceState.NotFound || serviceStatus == ServiceState.Stop)
            {
                ToggleCloak();
                InstallServiceButton.Text = "Uninstall Service";
            }
            else if (serviceStatus == ServiceState.Run || serviceStatus == ServiceState.Starting)
            {
                StartServiceButton.Text = "Stop Service";
                InstallServiceButton.Text = "Uninstall Service";
            }

            StartTimer();
        }

        private void BindData()
        {
            var dtIPLog = ServerCloak.GetIPLogs().Rows.OfType<DataRow>().Select(xs => new
            {
                Type = Convert.ToString(xs["AttackType"]),
                Attacks = xs["Attacks"].ToType<int>(),
                Locked = xs["Locked"].ToTypeOrNull<DateTime>()
            });

            CloakList.Items.Clear();
            foreach (DataRow row in ServerCloak.GetCloaks().Rows)
                CloakList.Items.Add(new ListViewItem(new[] {
                    Convert.ToString(row["Name"]),
                    Convert.ToString(row["Port"]),
                    Convert.ToString(dtIPLog.Where(x=>x.Type == Convert.ToString(row["Name"])).Sum(xs => xs.Attacks)),
                    Convert.ToString(dtIPLog.Count(x=>x.Type == Convert.ToString(row["Name"]) && x.Locked.HasValue))
                }));

            SavePortButton.Visible = PortPanel.Enabled = SaveLimitButton.Visible = AttackPanel.Enabled = Whitelist.Enabled = SaveWhitelistButton.Visible = false;
            EditCancelPortButton.Text = EditCancelLimitButton.Text = EditCancelWhitelist.Text = "Edit";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            BindData();
        }

        void StartTimer()
        {
            timer1_Tick(timer1, EventArgs.Empty);
            timer1.Start();
        }

        void StopTimer()
        {
            timer1.Stop();
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            if (EditCancelWhitelist.Text == "Edit")
            {
                EditCancelWhitelist.Text = "Cancel";
                Whitelist.Enabled = SaveWhitelistButton.Visible = true;
            }
            else
            {
                Whitelist.Enabled = SaveWhitelistButton.Visible = false;
                EditCancelWhitelist.Text = "Edit";
            }
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            File.WriteAllText(ServerCloak.WhiteListFile, Whitelist.Text.Trim());
            MessageBox.Show("White list updated.");
            StopTimer();
            StartTimer();
        }

        private void EditCancelLimitButton_Click(object sender, EventArgs e)
        {
            if (EditCancelLimitButton.Text == "Edit")
            {
                EditCancelLimitButton.Text = "Cancel";
                AttackPanel.Enabled = SaveLimitButton.Visible = true;
            }
            else
            {
                AttackPanel.Enabled = SaveLimitButton.Visible = false;
                EditCancelLimitButton.Text = "Edit";
            }
        }

        private void SaveLimitButton_Click(object sender, EventArgs e)
        {
            var dtCloak = ServerCloak.GetCloaks();
            for (int i = 0; i < dtCloak.Rows.Count; i++)
            {
                DataRow row = dtCloak.Rows[i];
                var textBox = AttackPanel.Controls.OfType<MaterialSingleLineTextField>().Where(x => x.Name == Convert.ToString(row["ID"])).FirstOrDefault();
                if (textBox != null)
                    row["Limit"] = textBox.Text;
                var cbx = AttackPanel.Controls.OfType<ComboBox>().Where(x => x.Name == "cbx" + Convert.ToString(row["ID"])).FirstOrDefault();
                if (cbx != null)
                    row["BlockDuration"] = (cbx.SelectedItem as ComboboxItem).Value;
            }
            dtCloak.WriteXml(ServerCloak.CloakFilePath, XmlWriteMode.WriteSchema);
            StopTimer();
            StartTimer();
            MessageBox.Show("Attack limit updated.");
        }

        private void EditCancelPortButton_Click(object sender, EventArgs e)
        {
            if (EditCancelPortButton.Text == "Edit")
            {
                EditCancelPortButton.Text = "Cancel";
                PortPanel.Enabled = SavePortButton.Visible = true;
            }
            else
            {
                PortPanel.Enabled = SavePortButton.Visible = false;
                EditCancelPortButton.Text = "Edit";
            }
        }

        private void SavePortButton_Click(object sender, EventArgs e)
        {
            var dtCloak = ServerCloak.GetCloaks();
            for (int i = 0; i < dtCloak.Rows.Count; i++)
            {
                DataRow row = dtCloak.Rows[i];
                var textBox = PortPanel.Controls.OfType<MaterialSingleLineTextField>().Where(x => x.Name == Convert.ToString(row["ID"])).FirstOrDefault();
                if (textBox != null)
                    row["Port"] = textBox.Text;
            }
            dtCloak.WriteXml(ServerCloak.CloakFilePath, XmlWriteMode.WriteSchema);
            StopTimer();
            StartTimer();
            MessageBox.Show("All ports updated.");
        }

        const string ServiceName = "ServerCloakService";
        void ToggleCloak()
        {
            if (Convert.ToString(StartServiceButton.Text).Contains("Start"))
            {
                if (!ServiceInstaller.ServiceIsInstalled(ServiceName))
                {
                    ServiceInstaller.InstallAndStart(ServiceName, ServiceName, string.Format("{0}{1}{2}", Application.StartupPath, "\\", ServiceName));
                    MessageBox.Show("Thank you for installing ServerCloak.\nIf you find any issue or need any help, don't hesitate to let me know as I'll be happy to help you.\n\n Vikas Rana\n i@VikasRana.com", "ServerCloak Installed");
                }
                else
                    ServiceInstaller.StartService(ServiceName);
                StartServiceButton.Text = "Stop Service";
                InstallServiceButton.Text = "Uninstall Service";
                timer1.Start();
            }
            else
            {
                ServiceInstaller.StopService(ServiceName);
                StartServiceButton.Text = "Start Service";
                timer1.Stop();
            }
        }

        private void StartServiceButton_Click(object sender, EventArgs e)
        {
            ToggleCloak();
        }

        private void InstallServiceButton_Click(object sender, EventArgs e)
        {
            if (InstallServiceButton.Text == "Uninstall Service")
            {
                ServiceInstaller.StopService(ServiceName);
                ServiceInstaller.Uninstall(ServiceName);
                InstallServiceButton.Text = "Install Service";
                MessageBox.Show("ServerCloak Service Uninstalled");
                Application.Exit();
            }
            else
            {
                if (!ServiceInstaller.ServiceIsInstalled(ServiceName))
                    ServiceInstaller.InstallAndStart(ServiceName, ServiceName, string.Format("{0}{1}{2}", Application.StartupPath, "\\", ServiceName));
                else
                    ServiceInstaller.StartService(ServiceName);
                InstallServiceButton.Text = "Uninstall Service";
                MessageBox.Show("ServerCloak Service Installed");
            }
        }

        private void UnblockIP_Click(object sender, EventArgs e)
        {
            try
            {
                ServerCloak.UnblockIP(UnblockTextBox.Text.Trim());
                MessageBox.Show("IP Address unblocked");
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private static CloakIPLog _CloakIPLogForm;
        private static CloakIPLog CloakIPLogForm
        {
            get
            {
                if (_CloakIPLogForm == null)
                {
                    _CloakIPLogForm = new CloakIPLog();
                    _CloakIPLogForm.FormClosed += _CloakIPLogForm_FormClosed;
                }
                return _CloakIPLogForm;
            }
        }

        static void _CloakIPLogForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _CloakIPLogForm = null;
        }

        private void CloakList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (_CloakIPLogForm != null)
            {
                _CloakIPLogForm.Close();
                _CloakIPLogForm = null;
            }
            CloakEnum selectedCloak;
            Enum.TryParse(Convert.ToString(e.Item.SubItems[0].Text), out selectedCloak);
            CloakIPLogForm.Cloak = selectedCloak;
            CloakIPLogForm.BindData();
        }
    }
}