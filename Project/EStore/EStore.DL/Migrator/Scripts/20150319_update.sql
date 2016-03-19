ALTER TABLE tblFile ADD SupplierInvoiceId [bigint] NULL
GO

ALTER TABLE tblSupplierInvoice ADD SupplierId [bigint] NULL
GO

ALTER TABLE tblSupplierInvoicePosition DROP CONSTRAINT FK_tblSupplierInvoicePosition_tblSupplier

/****** Object:  ForeignKey [FK_tblFile_tblSupplierInvoice]    Script Date: 03/19/2016 18:17:39 ******/
ALTER TABLE [dbo].[tblFile]  WITH CHECK ADD  CONSTRAINT [FK_tblFile_tblSupplierInvoice] FOREIGN KEY([SupplierInvoiceId])
REFERENCES [dbo].[tblSupplierInvoice] ([Id])
GO
ALTER TABLE [dbo].[tblFile] CHECK CONSTRAINT [FK_tblFile_tblSupplierInvoice]
GO

/****** Object:  ForeignKey [FK_tblSupplierInvoice_tblSupplier]    Script Date: 03/19/2016 18:17:39 ******/
ALTER TABLE [dbo].[tblSupplierInvoice]  WITH CHECK ADD  CONSTRAINT [FK_tblSupplierInvoice_tblSupplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[tblSupplier] ([Id])
GO
ALTER TABLE [dbo].[tblSupplierInvoice] CHECK CONSTRAINT [FK_tblSupplierInvoice_tblSupplier]
GO