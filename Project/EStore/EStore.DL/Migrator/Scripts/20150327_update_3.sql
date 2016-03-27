ALTER TABLE tblProductSingle ADD SupplierInvoicePositionId bigint NULL
GO

ALTER TABLE tblProductSingle ADD SupplierInvoiceId bigint NULL
GO

/****** Object:  ForeignKey [FK_tblProductSingle_tblSupplierInvoice]    Script Date: 03/27/2016 20:59:48 ******/
ALTER TABLE [dbo].[tblProductSingle]  WITH CHECK ADD  CONSTRAINT [FK_tblProductSingle_tblSupplierInvoice] FOREIGN KEY([SupplierInvoiceId])
REFERENCES [dbo].[tblSupplierInvoice] ([Id])
GO
ALTER TABLE [dbo].[tblProductSingle] CHECK CONSTRAINT [FK_tblProductSingle_tblSupplierInvoice]
GO

/****** Object:  ForeignKey [FK_tblProductSingle_tblSupplierInvoicePosition]    Script Date: 03/27/2016 20:59:48 ******/
ALTER TABLE [dbo].[tblProductSingle]  WITH CHECK ADD  CONSTRAINT [FK_tblProductSingle_tblSupplierInvoicePosition] FOREIGN KEY([SupplierInvoicePositionId])
REFERENCES [dbo].[tblSupplierInvoicePosition] ([Id])
GO
ALTER TABLE [dbo].[tblProductSingle] CHECK CONSTRAINT [FK_tblProductSingle_tblSupplierInvoicePosition]
GO