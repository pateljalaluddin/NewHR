USE [DbHR_Management]
GO

/****** Object:  Table [dbo].[TblEmployee]    Script Date: 30-Sep-21 2:19:33 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TblEmployee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmpId] [varchar](10) NULL,
	[EmpName] [varchar](50) NULL,
	[EmpEmail] [varchar](50) NULL,
	[EmpDesignation] [varchar](50) NULL,
	[CreatedDate] [varchar](20) NULL,
	[CreatedBy] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


