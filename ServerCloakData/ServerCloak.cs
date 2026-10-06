using ServerCloakData.IPTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerCloakData
{
    public enum BlockDurationEnum
    {
        Permanent = 0,
        Day = 1,
        Week = 7,
        Month = 30,
        Month3 = 90,
        Year = 365
    }

    public enum CloakEnum
    {
        ActiveDirectory = 100,
        FileMaker = 200,
        Ftp = 300,
        Kerberos = 400,
        Rdp = 500,
        RoutingAndRemoteAccess = 600,
        Smtp = 700,
        Sql = 800,
        WindowsSecurity = 900
        //,WindowsSecurityWhiteList = 999

    }

    public class AttackDetectedEventArgs
    {
        public CloakEnum CloakName { get; set; }
        public DateTime CreateDate { get; set; }
        public int EventId { get; set; }
        public string EventMessage { get; set; }
        public string IpAddress { get; set; }
    }

    public static class ServerCloak
    {
        public static string IPLogPath = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "IPLog.xml");
        public static string CloakFilePath = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "Cloak.xml");

        public static void Initialize()
        {
            if (!File.Exists(CloakFilePath))
            {
                DataTable dtCloak = new System.Data.DataTable("Cloak");
                dtCloak.Columns.Add("ID");
                dtCloak.Columns.Add("Name");
                dtCloak.Columns.Add("Port");
                dtCloak.Columns.Add("Limit");
                dtCloak.Columns.Add("BlockDuration");

                InitializeCloakRow(CloakEnum.ActiveDirectory, 0, 5, BlockDurationEnum.Month3, ref dtCloak);
                InitializeCloakRow(CloakEnum.FileMaker, 0, 5, BlockDurationEnum.Month3, ref dtCloak);
                InitializeCloakRow(CloakEnum.Ftp, 21, 5, BlockDurationEnum.Month3, ref dtCloak);
                InitializeCloakRow(CloakEnum.Kerberos, 0, 5, BlockDurationEnum.Month3, ref dtCloak);
                InitializeCloakRow(CloakEnum.Rdp, 3389, 5, BlockDurationEnum.Month3, ref dtCloak);
                InitializeCloakRow(CloakEnum.RoutingAndRemoteAccess, 0, 5, BlockDurationEnum.Month3, ref dtCloak);
                InitializeCloakRow(CloakEnum.Smtp, 25, 5, BlockDurationEnum.Month3, ref dtCloak);
                InitializeCloakRow(CloakEnum.Sql, 0, 5, BlockDurationEnum.Month3, ref dtCloak);
                InitializeCloakRow(CloakEnum.WindowsSecurity, 0, 5, BlockDurationEnum.Month3, ref dtCloak);

                dtCloak.WriteXml(CloakFilePath, XmlWriteMode.WriteSchema);
            }

            if (!File.Exists(IPLogPath))
            {
                DataTable dtIPLog = new System.Data.DataTable("IPLog");
                dtIPLog.Columns.Add("IP");
                dtIPLog.Columns.Add("Attacks");
                dtIPLog.Columns.Add("Locked");
                dtIPLog.Columns.Add("Created");
                dtIPLog.Columns.Add("AttackType");
                dtIPLog.WriteXml(IPLogPath, XmlWriteMode.WriteSchema);
            }
        }

        static void InitializeCloakRow(CloakEnum cloakEnum, int port, int limit, BlockDurationEnum blockDuration, ref DataTable dtCloak)
        {
            DataRow row = dtCloak.NewRow();
            row["ID"] = Convert.ToInt32(cloakEnum);
            row["Name"] = Convert.ToString(cloakEnum);
            row["Port"] = port;
            row["Limit"] = limit;
            row["BlockDuration"] = (int)blockDuration;
            dtCloak.Rows.Add(row);
        }

        public static DataTable GetIPLogs()
        {
            var dtIPLog = new DataTable("IPLog");
            dtIPLog.ReadXml(IPLogPath);
            return dtIPLog;
        }

        public static bool BlockIP(AttackDetectedEventArgs data)
        {
            if (IsWhiteListed(data))
                return false;

            int limit = 3;
            if (File.Exists(CloakFilePath))
            {
                var dtCloak = GetCloaks();
                var cloaks = dtCloak.Select(string.Format("Name='{0}'", data.CloakName.ToString()));
                if (cloaks != null && cloaks.Length > 0)
                    limit = cloaks[0]["Limit"].ToType<int>();
            }

            int attacks = SetIPLog(data, limit, IPLogPath);
            return attacks >= limit;
        }

        public static void UnblockIP(string ipAddress)
        {
            if (File.Exists(CloakFilePath))
            {
                FirewallPolicyManager.Instance.RemoveIpAddressFromBlockList(ipAddress);
                DataTable dtIPLog = GetIPLogs();
                DataRow row;
                var rows = dtIPLog.Select(string.Format("IP='{0}'", ipAddress));
                if (rows != null && rows.Length > 0)
                    row = rows[0];
                else return;
                row["Attacks"] = 0;
                row["Locked"] = string.Empty;
                dtIPLog.WriteXml(IPLogPath, XmlWriteMode.WriteSchema);
            }
        }

        private static int SetIPLog(AttackDetectedEventArgs data, int limit, string IPLogPath)
        {
            int attacks = 0;
            try
            {
                DataRow row;
                DataTable dtIPLog = GetIPLogs();
                var rows = dtIPLog.Select(string.Format("AttackType='{0}' AND IP='{1}'", data.CloakName.ToString(), data.IpAddress));
                row = rows != null && rows.Length > 0 ? rows[0] : dtIPLog.NewRow();
                row["IP"] = data.IpAddress;
                attacks = row["Attacks"].ToType<int>();
                row["Attacks"] = attacks + 1;
                if (attacks >= limit)
                    row["Locked"] = DateTime.Now;
                if (!row["Created"].ToTypeOrNull<DateTime>().HasValue)
                    row["Created"] = DateTime.Now;
                row["AttackType"] = data.CloakName.ToString();
                if (rows == null || rows.Length == 0)
                    dtIPLog.Rows.Add(row);
                dtIPLog.WriteXml(IPLogPath, XmlWriteMode.WriteSchema);
            }
            catch (Exception ex)
            {
                File.AppendAllText(string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "ErrorLog.txt"),
                  string.Format("{0}{1} ,  {2} ,  {3} ,  {4}{0}{5}", Environment.NewLine, data.CloakName, data.CreateDate, data.EventId, data.IpAddress, ex.ToString()),
              Encoding.Unicode);
            }
            return attacks;
        }

        public static string WhiteListFile = AppDomain.CurrentDomain.BaseDirectory + "\\WhiteList.txt";

        public static List<KeyValuePair<string, DateTime>> _WhiteListTemp;
        public static List<KeyValuePair<string, DateTime>> WhiteListTemp
        {
            get { return _WhiteListTemp ?? (_WhiteListTemp = new List<KeyValuePair<string, DateTime>>()); }
            set { _WhiteListTemp = value; }
        }
        public static bool IsWhiteListed(AttackDetectedEventArgs data)
        {
            if (WhiteListTemp.Count > 0)
                WhiteListTemp = WhiteListTemp.FindAll(x => x.Value.AddHours(6) >= DateTime.Now);
            List<string> WhiteListAll = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork || x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6).Select(xs => xs.ToString()).ToList();
            if (WhiteListAll == null)
                WhiteListAll = new List<string>();
            if (WhiteListTemp.Count > 0)
                WhiteListAll.AddRange(WhiteListTemp.Select(Xs => Xs.Key));
            if (File.Exists(WhiteListFile))
            {
                var whiteListData = (File.ReadAllText(WhiteListFile) ?? string.Empty).Trim().Split(new char[] { ',', ';', '\n' }, StringSplitOptions.RemoveEmptyEntries).Where(x => !string.IsNullOrWhiteSpace(x)).Select(xs => xs.Trim()).ToList();
                if (whiteListData.Count > 0)
                    WhiteListAll.AddRange(whiteListData);
            }
            IPAddress ip;
            if (WhiteListAll.Count > 0 && WhiteListAll.Count(x => x == data.IpAddress || (!IPAddress.TryParse(x, out ip) && IPAddress.TryParse(data.IpAddress, out ip) && IPAddressRange.Parse(x).Contains(ip))) > 0)
                return true;
            return false;
        }

        public static DataTable GetCloaks()
        {
            var dtCloak = new DataTable("Cloak");
            dtCloak.ReadXml(CloakFilePath);
            return dtCloak;
        }

        public static int GetCloakPort(CloakEnum cloakEnum)
        {
            var dtCloak = GetCloaks();
            var cloaks = dtCloak.Select(string.Format("Name='{0}'", cloakEnum.ToString()));
            if (cloaks != null && cloaks.Length > 0)
                return cloaks[0]["Port"].ToType<int>();
            return 0;
        }

        public static void SetCloakPort(CloakEnum cloakEnum, int port)
        {
            var dtCloak = GetCloaks();
            var cloaks = dtCloak.Select(string.Format("Name='{0}'", cloakEnum.ToString()));
            if (cloaks != null && cloaks.Length > 0)
            {
                var cloak = cloaks[0];
                cloak["Port"] = port;
                dtCloak.WriteXml(CloakFilePath, XmlWriteMode.WriteSchema);
            }
        }
    }
}
