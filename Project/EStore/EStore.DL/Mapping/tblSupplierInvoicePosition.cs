//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EStore.DL.Mapping
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblSupplierInvoicePosition
    {
        public long Id { get; set; }
        public Nullable<long> ProductId { get; set; }
        public Nullable<int> Qty { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Note { get; set; }
        public Nullable<long> SupplierInvoiceId { get; set; }
    
        public virtual tblProduct tblProduct { get; set; }
        public virtual tblSupplier tblSupplier { get; set; }
        public virtual tblSupplierInvoice tblSupplierInvoice { get; set; }
    }
}