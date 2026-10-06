using ServerCloakData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ServerCloakService
{
    public partial class ServerCloakService : ServiceBase
    {
        static List<BaseCloak> Cloaks = new List<BaseCloak>(){
            new ActiveDirectoryCredentialValidationSecurityCloak(),
            new FileMakerCloak(),
            new FtpCloak(),
            new Kerberos(),
            new RdpCloak(),
            new RrasSecurityCloak(),
            new SmtpCloak(),
            new SqlCloak(),
            new WindowsSecurityCloak()
            //,new WindowsSecurityCloakWhiteList()
        };

        public ServerCloakService()
        {
            InitializeComponent();
        }

        System.Timers.Timer FirewallCheckTimer, UnblockTimer;
        protected override void OnStart(string[] args)
        {
            ServerCloak.Initialize();

            FirewallCheckTimer = new System.Timers.Timer();
            FirewallCheckTimer.Interval = 30000;
            FirewallCheckTimer.Elapsed += FirewallCheckTimer_Tick;
            FirewallCheckTimer.Start();

            UnblockTimer = new System.Timers.Timer();
            UnblockTimer.Interval = 12 * 60 * 60 * 1000;
            UnblockTimer.Elapsed += UnblockTimer_Elapsed;
            UnblockTimer.Start();

            foreach (var cloak in Cloaks)
            {
                cloak.AttackDetected += AttackDetected;
                cloak.Start();
            }
        }

        void UnblockTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var cloaks = ServerCloak.GetCloaks();
            var ipLogs = ServerCloak.GetIPLogs().Select("Locked IS NOT NULL AND Locked <> ''");
            foreach (DataRow row in ipLogs)
            {
                var cloakType = (int)Enum.Parse(typeof(CloakEnum), Convert.ToString(row["AttackType"]));
                var blockDuration = cloaks.Rows.OfType<DataRow>()
                    .Where(x => x["ID"].ToType<int>() == cloakType)
                    .Select(xs => xs["BlockDuration"].ToType<int>()).FirstOrDefault();
                if (blockDuration > 0 && row["Locked"].ToType<DateTime>().AddDays(blockDuration).Date < DateTime.Now.Date)
                    ServerCloak.UnblockIP(Convert.ToString(row["IP"]));
            }
        }

        void FirewallCheckTimer_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (!FirewallPolicyManager.Instance.FirewallEnabled)
                    FirewallPolicyManager.Instance.FirewallEnabled = true;
            }
            catch { }
        }

        void AttackDetected(AttackDetectedEventArgs data)
        {
            //if (data.CloakName == CloakEnum.WindowsSecurityWhiteList && ServerCloak.WhiteListTemp.Count(x => x.Key == data.IpAddress) == 0)
            //    ServerCloak.WhiteListTemp.Add(new KeyValuePair<string, DateTime>(data.IpAddress, DateTime.Now));
            //else

            File.AppendAllText(string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "log.txt"),
                string.Format("{0}{1} ,  {2} ,  {3} ,  {4}", Environment.NewLine, data.CloakName, data.CreateDate, data.EventId, data.IpAddress),
                Encoding.Unicode);

            if (!FirewallPolicyManager.Instance.IsLocked(data.IpAddress) && ServerCloak.BlockIP(data))
                FirewallPolicyManager.Instance.Block(data.IpAddress);
        }

        protected override void OnStop()
        {
            foreach (var cloak in Cloaks)
                cloak.Stop();
        }
    }
}
