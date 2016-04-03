/****** Object:  Table [dbo].[tblProductFeedback]    Script Date: 04/04/2016 02:24:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblProductFeedback](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Pluses] [nvarchar](max) NULL,
	[Minuses] [nvarchar](max) NULL,
	[Comment] [nvarchar](max) NULL,
	[Stars] [int] NULL,
	[UserName] [nvarchar](250) NULL,
	[ProductId] [bigint] NULL,
 CONSTRAINT [PK_tblProductFeedback] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  ForeignKey [FK_tblProductFeedback_tblProduct]    Script Date: 04/04/2016 02:24:01 ******/
ALTER TABLE [dbo].[tblProductFeedback]  WITH CHECK ADD  CONSTRAINT [FK_tblProductFeedback_tblProduct] FOREIGN KEY([ProductId])
REFERENCES [dbo].[tblProduct] ([Id])
GO
ALTER TABLE [dbo].[tblProductFeedback] CHECK CONSTRAINT [FK_tblProductFeedback_tblProduct]
GO