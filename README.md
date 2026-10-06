# ServerCloak

A lightweight intrusion detection and defense system for Windows. ServerCloak watches network activity and
event logs for failed or denied inbound calls (IPv4 and IPv6), and when an attacker reaches the limit it adds a
deny rule to Windows Firewall for that IP.

[![License: Dual](https://img.shields.io/badge/license-MIT%20%2B%20Commercial-blue.svg)](LICENSE)

## What it protects

| Service | Detection |
|---|---|
| Mail server (SMTP) | Packet sniffer |
| Microsoft SQL Server | Failed-login monitoring |
| FTP | Packet sniffer |
| Remote Desktop (RDP) | Packet sniffer. TLS/SSL must be enabled for detection to work. |
| FileMaker | Failed-login monitoring |
| Windows Authentication (other than RDP) | Windows security events, Kerberos, Active Directory |
| Routing and Remote Access | RRAS security events |
| Windows Firewall | Re-enabled within minutes if someone turns it off |

IP addresses are mapped to countries (IPv4 and IPv6 lists), so you can review and block attackers by country.

## Solution layout

| Project | Purpose |
|---|---|
| `ServerCloakService` | Windows service. Runs the cloaks (SMTP, SQL, FTP, RDP, FileMaker, Windows security, RRAS) and manages firewall rules. |
| `ServerCloakData` | Shared library: firewall policy manager, service installer, IP-to-country lookup, IP range helpers. |
| `ServerCloak2` | Desktop UI (Material design) for installing the service, viewing attacker IP logs and country logs. |
| `ConsoleTest` | Console harness for testing detection code. |

## Requirements

- Windows 7 / Windows Server 2008 or later (64-bit)
- .NET Framework 4.8
- Windows Firewall installed (ServerCloak tries to enable it automatically)
- SQL Server 2005 or later, used to store logs and for reporting
- Dual-core 1 GHz CPU and 1 GB RAM or better
- Visual Studio 2019 or later to build from source

## Build

```sh
msbuild ServerCloak2.sln /p:Configuration=Release /p:Platform="Any CPU"
```

Or open `ServerCloak2.sln` in Visual Studio and build.

## Setup

ServerCloak is not an installer. Place the build output in a folder of your choice (for example
`C:\Program Files\ServerCloak`).

1. Create the database on SQL Server using `ServerCloak.Old/DatabaseScripts.sql`.
2. Set the connection string in `ConnectionString.txt`.
3. Run `ServerCloak2.exe` **as Administrator** and install/start the service.
4. Make sure Windows Firewall is working, because blocking is done through firewall rules.

Ready-made binaries and the database script from the previous release are in [`ServerCloak.Old`](ServerCloak.Old).

## How it works

1. Each cloak sniffs its protocol or reads the matching event log.
2. Failed or denied calls are logged per source IP.
3. When an IP exceeds the limit, the service adds an inbound deny rule to Windows Firewall.
4. The UI lets you review blocked IPs and countries.

## License

Dual-licensed, see [LICENSE](LICENSE).

- **MIT License:** free for personal, educational, and non-commercial use.
- **Commercial License:** required for business or commercial use. Contact
  [vikasrulez@gmail.com](mailto:vikasrulez@gmail.com).
