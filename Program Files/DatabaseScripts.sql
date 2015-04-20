USE [ServerCloak]
GO

/****** Object:  Table [dbo].[Cloak]    Script Date: 4/8/2015 3:01:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Cloak](
	[ID] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Port] [int] NOT NULL,
	[Limit] [int] NOT NULL,
 CONSTRAINT [PK_Cloak] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[IPLog]    Script Date: 4/8/2015 3:01:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[IPLog](
	[IP] [varchar](500) NOT NULL,
	[Attacks] [int] NOT NULL,
	[Locked] [datetime] NULL,
	[Created] [datetime] NOT NULL,
	[AttackType] [varchar](500) NOT NULL,
 CONSTRAINT [PK_IPLog] PRIMARY KEY CLUSTERED 
(
	[IP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[IPSoftUnlocked]    Script Date: 4/8/2015 3:01:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[IPSoftUnlocked](
	[IP] [varchar](500) NOT NULL,
	[Created] [datetime] NOT NULL,
 CONSTRAINT [PK_IPSoftUnlocked] PRIMARY KEY CLUSTERED 
(
	[IP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



/****** Object:  StoredProcedure [dbo].[GetStatus]    Script Date: 4/8/2015 3:01:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetStatus]
AS
SELECT
	Name,
	CASE WHEN Port = 0 THEN 'N/A' ELSE CAST(Port AS VARCHAR(10)) END Port,
	(SELECT SUM(Attacks) FROM (SELECT (ATTACKS * ((SELECT COUNT(*) FROM IPSoftUnlocked IPS WHERE IPS.IP = IL0.IP) + 1)) Attacks, AttackType FROM IPLog IL0) IL WHERE IL.AttackType = CK.Name) Attacks,
	(SELECT COUNT(*) FROM IPLog IL WHERE IL.AttackType = CK.Name AND Locked IS NOT NULL) Locked
FROM CLOAK CK
GO

/****** Object:  StoredProcedure [dbo].[IpAttacks]    Script Date: 4/8/2015 3:01:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[IpAttacks]
	@IP VARCHAR(500),
	@AttackType VARCHAR(500)
AS

IF EXISTS(SELECT * FROM IPLog WHERE IP = @IP AND AttackType = @AttackType)
	UPDATE IPLog
	SET Attacks = Attacks + 1
	WHERE IP = @IP AND AttackType = @AttackType
ELSE
	INSERT IPLog VALUES(@IP, 1, NULL, GETUTCDATE(), @AttackType)


SELECT
	SUM(Attacks) TotalAttacks
FROM IPLog
WHERE IP = @IP AND AttackType = @AttackType

GO

/****** Object:  StoredProcedure [dbo].[IPsSoftUnlock]    Script Date: 4/8/2015 3:01:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[IPsSoftUnlock]
AS

SELECT
	IP
FROM IPLog
WHERE Locked IS NOT NULL
	AND Created < DATEADD(DAY, -45, GETUTCDATE())
	AND IP NOT IN (SELECT IP FROM IPSoftUnlocked)
GO

IF NOT EXISTS(SELECT * FROM Cloak WHERE NAME = 'File Maker')
	INSERT Cloak VALUES('File Maker', 0, 5)
IF NOT EXISTS(SELECT * FROM Cloak WHERE NAME = 'Ftp')
	INSERT Cloak VALUES('Ftp', 21, 5)
IF NOT EXISTS(SELECT * FROM Cloak WHERE NAME = 'Rdp')
	INSERT Cloak VALUES('Rdp', 3389, 5)
IF NOT EXISTS(SELECT * FROM Cloak WHERE NAME = 'Remote and Routing Access')
	INSERT Cloak VALUES('Remote and Routing Access', 0, 5)
IF NOT EXISTS(SELECT * FROM Cloak WHERE NAME = 'Smtp')
	INSERT Cloak VALUES('Smtp', 25, 5)
IF NOT EXISTS(SELECT * FROM Cloak WHERE NAME = 'Sql')
	INSERT Cloak VALUES('Sql', 0, 5)
IF NOT EXISTS(SELECT * FROM Cloak WHERE NAME = 'Windows Authentication')
	INSERT Cloak VALUES('Windows Security - NTLMv2', 0 , 5)