USE [DbHR_Management]
GO

/****** Object:  Table [dbo].[TblEmpDetails]    Script Date: 30-Sep-21 2:17:36 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TblEmpDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmpId] [varchar](20) NULL,
	[FileName] [varchar](50) NULL,
	[FilePath] [varchar](200) NULL,
	[CreatedDate] [varchar](20) NULL,
	[CreatedBy] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


