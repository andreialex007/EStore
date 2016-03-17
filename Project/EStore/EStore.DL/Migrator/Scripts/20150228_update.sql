/****** Object:  Table [dbo].[tblFile]    Script Date: 02/29/2016 00:02:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblFile](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Path] [nvarchar](250) NULL,
	[Description] [nvarchar](250) NULL,
	[ProductId] [bigint] NULL,
 CONSTRAINT [PK_tblFile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_tblFile_tblProduct]    Script Date: 02/29/2016 00:02:38 ******/
ALTER TABLE [dbo].[tblFile]  WITH CHECK ADD  CONSTRAINT [FK_tblFile_tblProduct] FOREIGN KEY([ProductId])
REFERENCES [dbo].[tblProduct] ([Id])
GO
ALTER TABLE [dbo].[tblFile] CHECK CONSTRAINT [FK_tblFile_tblProduct]
GO