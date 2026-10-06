using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ServerCloakData
{
    public static class IP2C
    {
        public static string IP4Path = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "IPv4.xml");
        public static string IP6Path = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "IPv6.xml");

        public static void ImportIP4()
        {
            var dir = AppDomain.CurrentDomain.BaseDirectory;
            var file = dir + "IpToCountry.csv";
            var zipFile = file + ".zip";
            FileDelete(file);
            FileDelete(zipFile);

            using (var wc = new WebClient())
                wc.DownloadFile("http://software77.net/geo-ip/?DL=2", zipFile);

            ZipFile.ExtractToDirectory(zipFile, dir);
            var fileText = File.ReadAllText(file);
            fileText = fileText.Substring(fileText.LastIndexOf("#") + 1).Trim();

            DataTable dtIP4 = new System.Data.DataTable("IP4");
            dtIP4.Columns.Add("Min");
            dtIP4.Columns.Add("Max");
            dtIP4.Columns.Add("CountryCode2");
            dtIP4.Columns.Add("CountryCode3");
            dtIP4.Columns.Add("CountryName");

            foreach (string row in fileText.SplitTrim('\n'))
            {
                var arr = row.SplitTrim(',').Select(xs => xs.Replace("\"", string.Empty)).ToArray();
                IP4Row(arr[0], arr[1], arr[4], arr[5], arr[6], ref  dtIP4);     //arr[3] is for allocated, not required
            }
            dtIP4.WriteXml(IP4Path, XmlWriteMode.WriteSchema);
        }

        static void IP4Row(string min, string max, string countryCode2, string countryCode3, string countryName, ref DataTable dtIP4)
        {
            DataRow row = dtIP4.NewRow();
            row["Min"] = min;
            row["Max"] = max;
            row["CountryCode2"] = countryCode2;
            row["CountryCode3"] = countryCode3;
            row["CountryName"] = countryName;
            dtIP4.Rows.Add(row);
        }

        public static void ImportIP6()
        {
            var dir = AppDomain.CurrentDomain.BaseDirectory;
            var file = dir + "IpToCountry.6R.csv";
            var zipFile = file + ".gz";
            FileDelete(file);
            FileDelete(zipFile);

            using (var wc = new WebClient())
                wc.DownloadFile("http://software77.net/geo-ip/?DL=7", zipFile);
            FileStream output = new FileStream(file, FileMode.Create);
            var input = new GZipStream(File.OpenRead(zipFile), CompressionMode.Decompress);
            int bytesRead;
            byte[] buffer = new byte[4096];
            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
                output.Write(buffer, 0, bytesRead);

            input.Flush();
            input.Close();
            output.Close();
            input.Dispose();
            output.Dispose();

            var fileText = File.ReadAllText(file);
            fileText = fileText.Substring(fileText.LastIndexOf("#") + 1).Trim();

            DataTable dtIP6 = new System.Data.DataTable("IP6");
            dtIP6.Columns.Add("IPRange");
            dtIP6.Columns.Add("CountryCode");

            foreach (string row in fileText.SplitTrim('\n'))
            {
                var arr = row.SplitTrim(',').Select(xs => xs.Replace("\"", string.Empty)).ToArray();
                IP6Row(arr[0], arr[1], ref  dtIP6);
            }
            dtIP6.WriteXml(IP6Path, XmlWriteMode.WriteSchema);
        }

        static void IP6Row(string IPRange, string countryCode, ref DataTable dtIP6)
        {
            DataRow row = dtIP6.NewRow();
            row["IPRange"] = IPRange;
            row["CountryCode"] = countryCode;
            dtIP6.Rows.Add(row);
        }

        public static string IPFromCode(long ip)
        {
            return IPAddress.Parse(ip.ToString()).ToString();
        }

        public static long IPToCode(string ipAddress)
        {
            IPAddress ip;
            if (IPAddress.TryParse(ipAddress, out ip))
            {
                byte[] bytes = ip.GetAddressBytes();
                return (16777216 * (long)bytes[0] + 65536 * (long)bytes[1] + 256 * (long)bytes[2] + (long)bytes[3]);
            }
            else
                return 0;
        }

        public static DataTable GetIP4()
        {
            var dtIP4 = new DataTable("IP4");
            dtIP4.ReadXml(IP4Path);
            return dtIP4;
        }

        public static DataTable GetIP6()
        {
            var dtIP6 = new DataTable("IP6");
            dtIP6.ReadXml(IP6Path);
            return dtIP6;
        }

        public static string GetCountryFromIP(string userIP)//2a01:4f8:140:53e8:0:0:0:2
        {
            IPAddress ip;
            if (IPAddress.TryParse(userIP, out ip))
            {
                var ipCode = IPToCode(userIP);
                switch (ip.AddressFamily)
                {
                    case AddressFamily.InterNetwork:
                        var dtIP4 = GetIP4().Select(string.Format("'{0}' >= Min AND '{0}' <= Max", ipCode));
                        if (dtIP4.Length > 0)
                            return Convert.ToString(dtIP4[0]["CountryCode2"]).EmptyIfNull().ToUpper().Replace("ZZ", "__");
                        break;
                    case AddressFamily.InterNetworkV6:
                        //var tbl6 = SiteDb.DbTable(string.Format("SELECT CountryCode2 FROM IP2C.DBO.IP4 WHERE '{0}' >= Min AND '{0}' <= Max",
                        //     IPToCode(Request.UserHostAddress)));
                        // if (tbl6.Rows.Count > 0)
                        //     Session["HTTP_CF_IPCOUNTRY"] = _Country = Convert.ToString(tbl6.Rows[0][0]);
                        break;
                }
            }

            return string.Empty;
        }

        //string _Country;
        //protected string Country
        //{
        //    get
        //    {
        //        if (string.IsNullOrWhiteSpace(_Country))
        //            try
        //            {
        //                _Country = Convert.ToString(Session["HTTP_CF_IPCOUNTRY"]);
        //                if (string.IsNullOrWhiteSpace(_Country))
        //                {
        //                    IPAddress ip;
        //                    string userIP = Request.UserHostAddress;//"2a01:4f8:140:53e8:0:0:0:2";
        //                    if (IPAddress.TryParse(userIP, out ip))
        //                    {
        //                        switch (ip.AddressFamily)
        //                        {
        //                            case AddressFamily.InterNetwork:
        //                            case AddressFamily.InterNetworkV6:
        //                                var tbl4 = SiteDb.DbTable(string.Format("SELECT CountryCode2 FROM IP2C.DBO.IP4 WHERE '{0}' >= Min AND '{0}' <= Max",
        //                                    IPToCode(userIP)));
        //                                if (tbl4.Rows.Count > 0)
        //                                    Session["HTTP_CF_IPCOUNTRY"] = _Country = Convert.ToString(tbl4.Rows[0][0]).EmptyIfNull().ToUpper().Replace("ZZ", "__");
        //                                break;
        //                            //case AddressFamily.InterNetworkV6:
        //                            //var tbl6 = SiteDb.DbTable(string.Format("SELECT CountryCode2 FROM IP2C.DBO.IP4 WHERE '{0}' >= Min AND '{0}' <= Max",
        //                            //     IPToCode(Request.UserHostAddress)));
        //                            // if (tbl6.Rows.Count > 0)
        //                            //     Session["HTTP_CF_IPCOUNTRY"] = _Country = Convert.ToString(tbl6.Rows[0][0]);
        //                            //break;
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                _Country = "__";
        //                Lib.Postman.SendEmail("Vik...com :- " + ex.Message, string.Format("{0}<br/>{1}", Request.Url.OriginalString, ex), "vikasrulez@gmail.com");
        //            }
        //        return _Country.EmptyIfNull();
        //        //return _Country.EmptyIfNull().AltifEmpty(_Country = Request["HTTP_CF_IPCOUNTRY"].EmptyIfNull());
        //    }
        //}


        public static bool FileDelete(string path)
        {
            try
            {
                if (File.Exists(path))
                    File.Delete(path);
                return true;
            }
            catch { }
            return false;
        }

        public static void FileMove(string src, string to)
        {
            try
            {
                if (File.Exists(to))
                    FileDelete(to);
                File.Move(src, to);
            }
            catch { }
        }

        public static void FileCopy(string src, string to)
        {
            try
            {
                if (File.Exists(to))
                    FileDelete(to);
                File.Copy(src, to);
            }
            catch { }
        }
    }
}
