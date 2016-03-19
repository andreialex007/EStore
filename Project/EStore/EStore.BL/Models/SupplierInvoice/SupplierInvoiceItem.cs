using System;
using System.Collections.Generic;
using EStore.BL.Models._Common;

namespace EStore.BL.Models.SupplierInvoice
{
    public class SupplierInvoiceItem : ViewModelBase
    {
        public long Id { get; set; }
        public string SupplierNumber { get; set; }
        public DateTime? BuyDate { get; set; }
        public string Notes { get; set; }

        public List<SupplierInvoicePositionItem> Positions { get; set; } = new List<SupplierInvoicePositionItem>();
    }
}