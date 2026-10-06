using NetFwTypeLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;

namespace ServerCloakData
{
    public class FirewallPolicyManager
    {
        private static FirewallPolicyManager _instance;
        public static FirewallPolicyManager Instance { get { return _instance ?? (_instance = new FirewallPolicyManager()); } }

        private INetFwPolicy2 firewallPolicyManager = ((INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2")));
        private INetFwMgr firewalManager = ((INetFwMgr)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwMgr")));

        public bool FirewallEnabled
        {
            get
            {
                return firewalManager.LocalPolicy.CurrentProfile.FirewallEnabled;
            }
            set
            {
                firewalManager.LocalPolicy.CurrentProfile.FirewallEnabled = value;
            }
        }

        public void AddRule(string name, int port, NET_FW_IP_PROTOCOL_ protocol, NET_FW_RULE_DIRECTION_ direction, NET_FW_SCOPE_ scope, NET_FW_ACTION_ action, string remoteAddress)
        {
            bool flag = false;
            string ruleName = GetRuleName(name, port);
            INetFwRule rule = GetRule(ruleName);
            if (rule != null)
                flag = true;
            else
                rule = (INetFwRule)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule", true));

            IPAddress address;
            if (!IPAddress.TryParse(remoteAddress, out address))
                throw new ArgumentOutOfRangeException("IP address must be IPv4 or IPv6!");

            if (!flag)
            {
                rule.Action = action;
                rule.Grouping = "ServerCloaking";
                rule.Protocol = 6;
                rule.Description = "ServerCloaking rule";
                rule.Direction = direction;
                rule.Enabled = true;
                if (port > 0)
                    rule.LocalPorts = port.ToString();
                rule.Name = ruleName;
                rule.RemoteAddresses = remoteAddress;
                firewallPolicyManager.Rules.Add(rule);
            }
            else
            {
                rule.Enabled = true;
                rule.RemoteAddresses = rule.RemoteAddresses.Trim().Equals("*") ? remoteAddress : string.Format("{0},{1}", rule.RemoteAddresses, remoteAddress);
            }
        }

        public void Block(string ipAddress)
        {
            try
            {
                AddRule("BlockAttacker", 0, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY, NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN, NET_FW_SCOPE_.NET_FW_SCOPE_CUSTOM, NET_FW_ACTION_.NET_FW_ACTION_BLOCK, ipAddress);
            }
            catch (Exception exception)
            {
                EventLog.WriteEntry("Create Firewall Rule", exception.Message, EventLogEntryType.Error);
            }
        }

        public void CleanUpRules()
        {
            foreach (INetFwRule rule in FindRules("Blocked by ServerCloaking"))
                firewallPolicyManager.Rules.Remove(rule.Name);
        }

        public List<INetFwRule> FindRules(string name)
        {
            List<INetFwRule> list = new List<INetFwRule>();
            foreach (INetFwRule rule in firewallPolicyManager.Rules)
                if (rule.Name.StartsWith(name))
                    list.Add(rule);
            return list;
        }

        private string GetCleanedRemoteAddresses(string addresses, string removeAddress)
        {
            StringBuilder builder = new StringBuilder();
            var strArray = addresses.Contains<char>(',') ? addresses.Split(new char[] { ',' }) : new string[] { addresses };
            foreach (string str in strArray)
                if (!(str.Contains<char>('/') ? str.Split(new char[] { '/' })[0] : str).Trim().Equals(removeAddress.Trim()) && !str.Trim().Equals(removeAddress.Trim()))
                    builder.Append(str + ",");
            return builder.ToString();
        }

        public INetFwRule GetRule(string name)
        {
            foreach (INetFwRule rule in firewallPolicyManager.Rules)
                if (rule.Name == name)
                    return rule;
            return null;
        }

        public string GetRuleName(string name, int port)
        {
            return string.Format("{0}_{1}_{2}", "Blocked by ServerCloakng", name, (port == 0) ? "AllPorts" : port.ToString());
        }

        public bool IsLocked(string ipAddress)
        {
            try
            {
                var rule = GetRule(GetRuleName("BlockAttacker", 0));
                return rule == null ? false : rule.RemoteAddresses.Contains(ipAddress);
            }
            catch (Exception exception)
            {
                EventLog.WriteEntry("IsLocked encountered an error: ", exception.Message, EventLogEntryType.Error);
            }
            return false;
        }

        public string BlockIPs1
        {
            get
            {
                var test = GetRule("Blocked by ServerCloakng_BlockAttacker_AllPorts");
                return test == null ? string.Empty : test.RemoteAddresses;
            }
        }

        public string BlockIPs2
        {
            get
            {
                var test = GetRule("Blocked by ServerCloakng_BlockAttacker_AllPorts");
                return test == null ? string.Empty : test.RemoteAddresses;
            }
        }

        public void RemoveIpAddressFromBlockList(string ipAddress)
        {
            string ruleName = GetRuleName("BlockAttacker", 0);
            INetFwRule rule = GetRule(ruleName);
            if (!rule.RemoteAddresses.Contains(ipAddress))
                return;

            rule.RemoteAddresses = GetCleanedRemoteAddresses(rule.RemoteAddresses, ipAddress);
            if ((rule.RemoteAddresses == "*") || string.IsNullOrEmpty(rule.RemoteAddresses.Replace(',', ' ').Trim()))
                rule.Enabled = false;
        }
    }
}
