CREATE DATABASE animal_Shelter
GO
USE [animal_Shelter]
GO
/****** Object:  Table [dbo].[animal_types]    Script Date: 3/1/2016 4:41:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[animal_types](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[type] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[animals]    Script Date: 3/1/2016 4:41:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[animals](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[gender] [varchar](255) NULL,
	[dateOfAdmittance] [date] NULL,
	[breed] [varchar](255) NULL,
	[animal_type_id] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
