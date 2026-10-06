using ServerCloakData;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Windows.Forms;
using System.Linq;
using System.IO;

namespace ServerCloak2
{
    static class Program
    {
        public static bool IsAdmin
        {
            get
            {
                try
                {
                    WindowsIdentity user = WindowsIdentity.GetCurrent();
                    WindowsPrincipal principal = new WindowsPrincipal(user);
                    return principal.IsInRole(WindowsBuiltInRole.Administrator);
                }
                catch { }
                return false;
            }
        }

        [STAThread]
        static void Main()
        {
            var fpm = FirewallPolicyManager.Instance;

            //var test = fpm.BlockIPs1;
            //File.WriteAllLines(AppDomain.CurrentDomain.BaseDirectory + "Firewal11.txt", fpm.BlockIPs1.SplitTrim(","));
            //File.WriteAllLines(AppDomain.CurrentDomain.BaseDirectory + "Firewal22.txt", fpm.BlockIPs2.SplitTrim(","));
            //return;

            //File.WriteAllLines(AppDomain.CurrentDomain.BaseDirectory + "Firewal11.txt", File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "Firewal1.txt").SplitTrim(","));
            //File.WriteAllLines(AppDomain.CurrentDomain.BaseDirectory + "Firewal22.txt", File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "Firewal2.txt").SplitTrim(","));
            //foreach (string ip in File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "Firewal11.txt").SplitTrim(Environment.NewLine).Select(xs => xs.Contains('/') ? xs.Remove(xs.IndexOf('/')) : xs))
            //    try
            //    {
            //        if (!fpm.IsLocked(ip))
            //            fpm.Block(ip);
            //    }
            //    catch { }


            //foreach (string ip in File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "Firewal22.txt").SplitTrim(Environment.NewLine).Select(xs => xs.Contains('/') ? xs.Remove(xs.IndexOf('/')) : xs))
            //    try
            //    {
            //        if (!fpm.IsLocked(ip))
            //            fpm.Block(ip);
            //    }
            //    catch { }
            //return;

            //var IpAddress = "127.0.0.1";
            //if (ServerCloak.WhiteListTemp.Count(x => x.Key == IpAddress) == 0)
            //    ServerCloak.WhiteListTemp.Add(new KeyValuePair<string, DateTime>(IpAddress, DateTime.Now));

            //if (ServerCloak.WhiteListTemp.Count(x => x.Key == IpAddress) == 0)
            //    ServerCloak.WhiteListTemp.Add(new KeyValuePair<string, DateTime>(IpAddress, DateTime.Now));

            //return;
            //var ipAddress = "192.168.100.101";
            //IPAddress ip;
            //var test1 = IPAddress.TryParse(ipAddress, out ip);
            //var test2 = IPAddressRange.Parse("192.168.100.0/24").Contains(ip);

            //if (ServerCloak.BlockIP(new AttackDetectedEventArgs()
            //{
            //    CloakName = CloakEnum.ActiveDirectory,
            //    CreateDate = DateTime.Now,
            //    EventId = 0,
            //    EventMessage = string.Empty,
            //    IpAddress = ipAddress
            //}))
            //{

            //}

            //return;

            //var cloaks = ServerCloak.GetCloaks();
            //var ipLogs = ServerCloak.GetIPLogs().Select("Locked IS NOT NULL AND Locked <> ''");
            //foreach (DataRow row in ipLogs)
            //{
            //    var cloakType = (int)Enum.Parse(typeof(CloakEnum), Convert.ToString(row["AttackType"]));
            //    var blockDuration = cloaks.Rows.OfType<DataRow>()
            //        .Where(x => x["ID"].ToType<int>() == cloakType)
            //        .Select(xs => xs["BlockDuration"].ToType<int>()).FirstOrDefault();
            //    if (blockDuration > 0 && row["Locked"].ToType<DateTime>().AddDays(blockDuration).Date < DateTime.Now.Date)
            //        ServerCloak.UnblockIP(Convert.ToString(row["IP"]));
            //}
            //return;

            if (!IsAdmin)
            {
                MessageBox.Show("Access Denied as you have to run this utility as Administrator.");
                Application.Exit();
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
