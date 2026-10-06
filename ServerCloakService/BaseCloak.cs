using ServerCloakData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ServerCloakService
{
    public delegate void AttackDetectedHandler(AttackDetectedEventArgs data);

    public class BaseCloak
    {
        public event AttackDetectedHandler AttackDetected;

        CloakConfiguration _configuration;
        internal CloakConfiguration Configuration
        {
            get
            {
                if (this._configuration == null)
                {
                    this._configuration = new CloakConfiguration();
                }
                return this._configuration;
            }
            set
            {
                this._configuration = value;
            }
        }

        protected void OnAttackDetected(object sender, AttackDetectedEventArgs data)
        {
            if (AttackDetected != null)
                AttackDetected(data);
        }

        protected virtual void OnStart()
        {
        }

        protected virtual void OnStop()
        {
        }

        public void Start()
        {
            if (!IsRunning)
            {
                OnStart();
                IsRunning = true;
            }
        }

        public void Stop()
        {
            if (IsRunning)
            {
                OnStop();
                IsRunning = false;
            }
        }

        internal void WriteEntry(string source, string msg)
        {
            EventLog.WriteEntry(source, msg);
        }

        internal virtual bool IsRunning { get; set; }

        internal virtual CloakEnum CloakName { get { return default(CloakEnum); } }

        protected bool EventRecordWrittenForWatcher(EventRecordWrittenEventArgs e, out AttackDetectedEventArgs data)
        {
            data = null;
            string ip;
            foreach (EventProperty property in e.EventRecord.Properties)
                if (IsValidIP(Convert.ToString(property.Value), out ip))
                {
                    IPAddress address;
                    if (IPAddress.TryParse(ip, out address) && ((address.AddressFamily == AddressFamily.InterNetwork) || (address.AddressFamily == AddressFamily.InterNetworkV6)))
                    {
                        data = new AttackDetectedEventArgs
                        {
                            CreateDate = e.EventRecord.TimeCreated.Value,
                            EventId = e.EventRecord.Id,
                            IpAddress = ip,
                            CloakName = CloakName
                        };
                        return true;
                    }
                }
            return false;
        }

        private const string v4 = "(?:[0-9]{1,3}.){3}[0-9]{1,3}";
        private const string v6 = @"^s*((([0-9A-Fa-f]{1,4}:){7}([0-9A-Fa-f]{1,4}|:))|(([0-9A-Fa-f]{1,4}:){6}(:[0-9A-Fa-f]{1,4}|((25[0-5]|2[0-4]d|1dd|[1-9]?d)(.(25[0-5]|2[0-4]d|1dd|[1-9]?d)){3})|:))|(([0-9A-Fa-f]{1,4}:){5}(((:[0-9A-Fa-f]{1,4}){1,2})|:((25[0-5]|2[0-4]d|1dd|[1-9]?d)(.(25[0-5]|2[0-4]d|1dd|[1-9]?d)){3})|:))|(([0-9A-Fa-f]{1,4}:){4}(((:[0-9A-Fa-f]{1,4}){1,3})|((:[0-9A-Fa-f]{1,4})?:((25[0-5]|2[0-4]d|1dd|[1-9]?d)(.(25[0-5]|2[0-4]d|1dd|[1-9]?d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){3}(((:[0-9A-Fa-f]{1,4}){1,4})|((:[0-9A-Fa-f]{1,4}){0,2}:((25[0-5]|2[0-4]d|1dd|[1-9]?d)(.(25[0-5]|2[0-4]d|1dd|[1-9]?d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){2}(((:[0-9A-Fa-f]{1,4}){1,5})|((:[0-9A-Fa-f]{1,4}){0,3}:((25[0-5]|2[0-4]d|1dd|[1-9]?d)(.(25[0-5]|2[0-4]d|1dd|[1-9]?d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){1}(((:[0-9A-Fa-f]{1,4}){1,6})|((:[0-9A-Fa-f]{1,4}){0,4}:((25[0-5]|2[0-4]d|1dd|[1-9]?d)(.(25[0-5]|2[0-4]d|1dd|[1-9]?d)){3}))|:))|(:(((:[0-9A-Fa-f]{1,4}){1,7})|((:[0-9A-Fa-f]{1,4}){0,5}:((25[0-5]|2[0-4]d|1dd|[1-9]?d)(.(25[0-5]|2[0-4]d|1dd|[1-9]?d)){3}))|:)))(%.+)?s*";
        internal bool IsValidIP(string address, out string ip)
        {
            ip = string.Empty;
            if (Regex.IsMatch(address, v4))
            {
                ip = Regex.Match(address, v4).Value;
                return true;
            }
            if (Regex.IsMatch(address, v6))
            {
                ip = Regex.Match(address, v6).Value;
                return true;
            }
            return false;
        }
    }
}
