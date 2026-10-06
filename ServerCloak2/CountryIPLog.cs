using MaterialSkin.Controls;
using ServerCloakData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerCloak2
{
    public partial class CountryIPLog : MaterialForm
    {
        public CloakEnum Cloak { get; set; }

        public CountryIPLog()
        {
            InitializeComponent();
        }

        public void BindData()
        {
            Text = Convert.ToString(Cloak);

            var dtIPLog = ServerCloak.GetIPLogs();

            CloakList.Items.Clear();
            foreach (DataRow row in dtIPLog.Rows)
                CloakList.Items.Add(new ListViewItem(new[] {
                    Convert.ToString(row["IP"]),
                    Convert.ToString(row["Attacks"]),
                    Convert.ToString(row["Attacks"])
                }));

            TopMost = true;
            ResizeListView();
            Show();
        }

        private void ResizeListView()
        {
            int width = this.Width - 2;
            CloakList.Width = width;
            width = width - 20;
            CloakList.Columns[0].Width = 300;
            CloakList.Columns[1].Width = 140;
            CloakList.Columns[2].Width = 140;
            CloakList.Font = MainWindow.PageFont;
        }
    }
}
