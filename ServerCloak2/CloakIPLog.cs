using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServerCloakData;
using MaterialSkin.Controls;

namespace ServerCloak2
{
    public partial class CloakIPLog : MaterialForm
    {
        public CloakEnum Cloak { get; set; }

        public CloakIPLog()
        {
            InitializeComponent();
        }

        public void BindData()
        {
            Text = Convert.ToString(Cloak);

            var dtIPLog = ServerCloak.GetIPLogs();

            CloakList.Items.Clear();
            foreach (DataRow row in dtIPLog.Select(string.Format("AttackType = '{0}'", Convert.ToString(Cloak))))
                CloakList.Items.Add(new ListViewItem(new[] {
                    Convert.ToString(row["IP"]),
                    Convert.ToString(row["Attacks"]),
                    string.Format("{0:dd-MMM-yyyy @ hh:mm tt}", row["Created"].ToType<DateTime>()),
                    string.Format("{0:dd-MMM-yyyy @ hh:mm tt}", row["Locked"].ToTypeOrNull<DateTime>())
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
            CloakList.Columns[0].Width = 200;
            CloakList.Columns[1].Width = 80;
            CloakList.Columns[2].Width = 120;
            CloakList.Columns[3].Width = 120;
            CloakList.Columns[4].Width = 80;
            CloakList.Font = MainWindow.PageFont;
        }
    }
}
