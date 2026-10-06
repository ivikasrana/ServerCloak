using ServerCloakData;
using System;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;

namespace ServerCloakService
{
    public class WindowsSecurityCloakWhiteList : BaseCloak
    {
        internal const string EVENT_LOG_QUERY_WINDOWS_LOGIN_DENIED = "<QueryList>\r\n                  <Query Id=\"0\" Path=\"Security\">\r\n                    <Select Path=\"Security\">\r\n                        *[System[(EventID=4624) and\r\n                        TimeCreated[timediff(@SystemTime) &lt;= 86400000]]]\r\n                    </Select>\r\n                  </Query>\r\n                </QueryList>";
        private EventLogQuery query;
        private EventLogWatcher watcher;

        internal override CloakEnum CloakName
        {
            get
            {
                return CloakEnum.WindowsSecurityWhiteList;
            }
        }

        protected override void OnStart()
        {
            query = new EventLogQuery("Security", PathType.LogName, string.Format("<QueryList>\r\n                  <Query Id=\"0\" Path=\"Security\">\r\n                    <Select Path=\"Security\">\r\n                        *[System[(EventID=4624) and\r\n                        TimeCreated[timediff(@SystemTime) &lt;= 86400000]]]\r\n                    </Select>\r\n                  </Query>\r\n                </QueryList>", new object[0]));
            watcher = new EventLogWatcher(query);
            watcher.EventRecordWritten += new EventHandler<EventRecordWrittenEventArgs>(watcher_EventRecordWritten);
            watcher.Enabled = true;
        }

        protected override void OnStop()
        {
            watcher.Enabled = false;
            watcher = null;
            query = null;
        }

        private void watcher_EventRecordWritten(object sender, EventRecordWrittenEventArgs e)
        {
            try
            {
                string[] propertyQueries = new string[] { "Event/EventData/Data[@Name=\"IpAddress\"]" };
                EventLogPropertySelector propertySelector = new EventLogPropertySelector(propertyQueries);
                string str = ((EventLogRecord)e.EventRecord).GetPropertyValues(propertySelector)[0].ToString();
                AttackDetectedEventArgs data = new AttackDetectedEventArgs
                {
                    CloakName = CloakName,
                    CreateDate = e.EventRecord.TimeCreated.Value,
                    EventId = e.EventRecord.Id,
                    IpAddress = str
                };
                base.OnAttackDetected(this, data);
            }
            catch (Exception exception)
            {
                WriteEntry("ServerCloak.WindowsSecurityWhiteList", exception.Message);
            }
        }
    }
}

