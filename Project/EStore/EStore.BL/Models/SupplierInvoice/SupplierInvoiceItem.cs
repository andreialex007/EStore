using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EStore.BL.Models.Product;
using EStore.BL.Models._Common;
using EStore.BL.Utils;
using Newtonsoft.Json;

namespace EStore.BL.Models.SupplierInvoice
{
    public class SupplierInvoiceItem : ViewModelBase
    {
        public long Id { get; set; }

        [Display(Name = "Номер накладной у поставщика")]
        [Required]
        public string SupplierNumber { get; set; }

        [Display(Name = "Дата накладной")]
        [Required]
        [JsonConverter(typeof(OnlyDateConverter))]
        public DateTime? BuyDate { get; set; }

        [Display(Name = "Примечания")]
        public string Notes { get; set; }

        public int PositionsQty { get; set; }
        public decimal Total { get; set; }

        [Display(Name = "Поставщик")]
        [Required]
        public long? SupplierId { get; set; }

        public List<SupplierInvoicePositionItem> Positions { get; set; } = new List<SupplierInvoicePositionItem>();
        public List<SupplierItem> AvaliableSuppliers { get; set; } = new List<SupplierItem>();
        public List<FileItem> Files { get; set; } = new List<FileItem>();
    }
}