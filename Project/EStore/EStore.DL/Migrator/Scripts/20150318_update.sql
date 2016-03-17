ALTER TABLE tblUser ADD Email [nvarchar](250) NULL
GO

ALTER TABLE tblProduct ADD ManufacterId [bigint] NULL
GO

/****** Object:  Table [dbo].[tblManufacter]    Script Date: 03/18/2016 00:53:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblManufacter](
	[Id] [bigint] NOT NULL,
	[Name] [nvarchar](250) NULL,
 CONSTRAINT [PK_tblManufacter] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[tblSupplierInvoice]    Script Date: 03/18/2016 00:53:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSupplierInvoice](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[SupplierNumber] [nvarchar](250) NULL,
	[BuyDate] [datetime] NULL,
	[Notes] [nvarchar](max) NULL,
 CONSTRAINT [PK_tblSupplierInvoice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[tblSupplier]    Script Date: 03/18/2016 00:53:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSupplier](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NULL,
 CONSTRAINT [PK_tblSupplier] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[tblOrder]    Script Date: 03/18/2016 00:53:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblOrder](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NULL,
	[State] [int] NULL,
 CONSTRAINT [PK_tblOrder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[tblSupplierInvoicePosition]    Script Date: 03/18/2016 00:53:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSupplierInvoicePosition](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductId] [bigint] NULL,
	[Qty] [int] NULL,
	[Price] [decimal](18, 2) NULL,
	[Note] [nvarchar](max) NULL,
	[SupplierInvoiceId] [bigint] NULL,
 CONSTRAINT [PK_tblSupplierInvoicePosition] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[tblProductSingle]    Script Date: 03/18/2016 00:59:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblProductSingle](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[BuyPrice] [decimal](18, 2) NULL,
	[SellPrice] [decimal](18, 2) NULL,
	[IsNew] [bigint] NULL,
	[ProductId] [bigint] NULL,
	[IsSelling] [bit] NULL,
	[OrderId] [bigint] NULL,
 CONSTRAINT [PK_tblProductOne] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  ForeignKey [FK_tblProduct_tblManufacter]    Script Date: 03/18/2016 01:03:26 ******/
ALTER TABLE [dbo].[tblProduct]  WITH CHECK ADD  CONSTRAINT [FK_tblProduct_tblManufacter] FOREIGN KEY([ManufacterId])
REFERENCES [dbo].[tblManufacter] ([Id])
GO
ALTER TABLE [dbo].[tblProduct] CHECK CONSTRAINT [FK_tblProduct_tblManufacter]
GO
/****** Object:  ForeignKey [FK_tblOrder_tblUser]    Script Date: 03/18/2016 01:03:26 ******/
ALTER TABLE [dbo].[tblOrder]  WITH CHECK ADD  CONSTRAINT [FK_tblOrder_tblUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[tblUser] ([Id])
GO
ALTER TABLE [dbo].[tblOrder] CHECK CONSTRAINT [FK_tblOrder_tblUser]
GO
/****** Object:  ForeignKey [FK_tblSupplierInvoicePosition_tblProduct]    Script Date: 03/18/2016 01:03:26 ******/
ALTER TABLE [dbo].[tblSupplierInvoicePosition]  WITH CHECK ADD  CONSTRAINT [FK_tblSupplierInvoicePosition_tblProduct] FOREIGN KEY([ProductId])
REFERENCES [dbo].[tblProduct] ([Id])
GO
ALTER TABLE [dbo].[tblSupplierInvoicePosition] CHECK CONSTRAINT [FK_tblSupplierInvoicePosition_tblProduct]
GO
/****** Object:  ForeignKey [FK_tblSupplierInvoicePosition_tblSupplier]    Script Date: 03/18/2016 01:03:26 ******/
ALTER TABLE [dbo].[tblSupplierInvoicePosition]  WITH CHECK ADD  CONSTRAINT [FK_tblSupplierInvoicePosition_tblSupplier] FOREIGN KEY([SupplierInvoiceId])
REFERENCES [dbo].[tblSupplier] ([Id])
GO
ALTER TABLE [dbo].[tblSupplierInvoicePosition] CHECK CONSTRAINT [FK_tblSupplierInvoicePosition_tblSupplier]
GO
/****** Object:  ForeignKey [FK_tblSupplierInvoicePosition_tblSupplierInvoice]    Script Date: 03/18/2016 01:03:26 ******/
ALTER TABLE [dbo].[tblSupplierInvoicePosition]  WITH CHECK ADD  CONSTRAINT [FK_tblSupplierInvoicePosition_tblSupplierInvoice] FOREIGN KEY([SupplierInvoiceId])
REFERENCES [dbo].[tblSupplierInvoice] ([Id])
GO
ALTER TABLE [dbo].[tblSupplierInvoicePosition] CHECK CONSTRAINT [FK_tblSupplierInvoicePosition_tblSupplierInvoice]
GO
/****** Object:  ForeignKey [FK_tblProductSingle_tblOrder]    Script Date: 03/18/2016 01:03:26 ******/
ALTER TABLE [dbo].[tblProductSingle]  WITH CHECK ADD  CONSTRAINT [FK_tblProductSingle_tblOrder] FOREIGN KEY([OrderId])
REFERENCES [dbo].[tblOrder] ([Id])
GO
ALTER TABLE [dbo].[tblProductSingle] CHECK CONSTRAINT [FK_tblProductSingle_tblOrder]
GO
/****** Object:  ForeignKey [FK_tblProductSingle_tblProduct]    Script Date: 03/18/2016 01:03:26 ******/
ALTER TABLE [dbo].[tblProductSingle]  WITH CHECK ADD  CONSTRAINT [FK_tblProductSingle_tblProduct] FOREIGN KEY([ProductId])
REFERENCES [dbo].[tblProduct] ([Id])
GO
ALTER TABLE [dbo].[tblProductSingle] CHECK CONSTRAINT [FK_tblProductSingle_tblProduct]
GO
