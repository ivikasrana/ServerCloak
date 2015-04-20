ServerCloak is an Intrusion detection Windows Service works with windows firewall defense system developed to protect any windows operating system from attacks that are intended to hack the server or provide any operational damage. By keeping eye on event logs in windows event viewer and system's network activities Server Cloak capture and log any failed/denied inbound calls from IPv4 as well as IPv6. Once the calls reaches its limit, the service immediately tells windows firewall to block the attacking IP by adding a denial inbound firewall rule. 

Features
Server Cloak verifies and keep windows firewall enabled all the times. With having intrusion detections enabled and firewall defense system for windows ServerCloak protects the following windows features

    Mail Server
    Microsoft Sql Server
    File Transfer Protocol
    Remote Desktop Protocol TLS/SSL must be enabled to make the detection possible
    File Maker
    Windows Security - NTLMv2
    Routing and Remote Access
    Windows Firewall if firewall got disabled, Server Cloak re-enables it within minutes

Supported Operating Systems

    Windows 7 or later. Developed and tested on Windows 8.1
    Windows server 2008 or later. Tested and deployed on Windows Server 2008 R2 & Windows Server 2012 R2 based production servers

System Requirements

    Microsoft .Net Framework 4.5, Latest version available here
    1Ghz higher multi-core CPU, Intel Dual Core or better
    1GB RAM
    Windows firewall must be installed, If not enabled Server Cloak will try to enable windows firewall automatically.
    This software uses Sql Server as back-end to save logs and enable developers to generate detailed reports.