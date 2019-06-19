SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[T_MarkUserCertificate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MarkUserId] [int] NULL,
	[CertificateId] [int] NULL,
 CONSTRAINT [PK_T_MarkUserCertificate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
) 
)

GO
