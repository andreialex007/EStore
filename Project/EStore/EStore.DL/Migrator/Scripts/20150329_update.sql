ALTER TABLE tblProduct ADD CategoryId bigint NULL
GO

/****** Object:  Table [dbo].[tblProductCategory]    Script Date: 03/29/2016 00:12:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblProductCategory](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NULL,
	[ParentCategoryId] [bigint] NULL,
 CONSTRAINT [PK_tblProductCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  ForeignKey [FK_tblProductCategory_tblProductCategory]    Script Date: 03/29/2016 00:12:16 ******/
ALTER TABLE [dbo].[tblProductCategory]  WITH CHECK ADD  CONSTRAINT [FK_tblProductCategory_tblProductCategory] FOREIGN KEY([ParentCategoryId])
REFERENCES [dbo].[tblProductCategory] ([Id])
GO
ALTER TABLE [dbo].[tblProductCategory] CHECK CONSTRAINT [FK_tblProductCategory_tblProductCategory]
GO

/****** Object:  ForeignKey [FK_tblProduct_tblProductCategory]    Script Date: 03/29/2016 00:12:16 ******/
ALTER TABLE [dbo].[tblProduct]  WITH CHECK ADD  CONSTRAINT [FK_tblProduct_tblProductCategory] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[tblProductCategory] ([Id])
GO
ALTER TABLE [dbo].[tblProduct] CHECK CONSTRAINT [FK_tblProduct_tblProductCategory]
GO