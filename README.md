#ServerCloak
Its a light weight Intrusion detection and defense system works with windows firewall to protect any windows operating system from attacks that are intended to hack the server or provide any operational damage.
  By keeping eye on event logs in windows event viewer and system's network activities ServerCloak capture and log any failed/denied inbound calls from IPv4 as well as IPv6.
  Once the calls reaches its limit, the service immediately tells windows firewall to block the attacking IP by adding a denial inbound firewall rule.
<p>
  <a href="http://www.vikasrana.com/Download/ServerCloak.zip" target="_blank">
  Download ServerCloak
</a>
</p>
<p>
  <b>
  Features
  </b>
  <br>
  ServerCloak verifies and keep windows firewall enabled all the times. With having intrusion detections enabled and firewall defense system for windows ServerCloak protects the following windows features<br>
</p>

<ol>
  <li>
  Mail Server
  </li>
  <li>
  Microsoft Sql Server
  </li>
  <li>
  File Transfer Protocol
  </li>
  <li>
  Remote Desktop Protocol <small>
  TLS/SSL must be enabled to make the detection possible
  </small>
  </li>
  <li>
  File Maker
  </li>
  <li>
  Windows Authentication <small>(Other than Rdp)</small>
  </li>
  <li>
  Routing and Remote Access
  </li>
  <li>
  Windows Firewall <small>
  if firewall got disabled, ServerCloak re-enables it within minutes
  </small>
  </li>
</ol>
<p>
</p>
<p>
  <b>
  Supported Operating Systems
  </b>
</p>

<ol>
  <li>
  Windows 7 or later. <small>
  Developed and tested on Windows 8.1
 </small>
  </li>
  <li>
  Windows server 2008 or later. <small>
  Tested and deployed on Windows Server 2008 R2 &amp; Windows Server 2012 R2 based production servers
  </small>
  </li>
</ol>
<p>
</p>
<p>
  <b>
  System Requirements
  </b>
</p>

<ol>
  <li>
  Microsoft .Net Framework 4.5, Latest version available <a href="http://www.microsoft.com/Net" target="_blank">
  here
  </a>
  </li>
  <li>
  1Ghz higher multi-core CPU, Intel Dual Core or better
  </li>
  <li>
  1GB RAM
  </li>
  <li>
  Windows firewall must be installed, If not enabled ServerCloak will try to enable windows firewall automatically.
  </li>
  <li>
  This software uses Sql Server as back-end to save logs and enable developers to generate detailed reports.
  </li>
</ol>
