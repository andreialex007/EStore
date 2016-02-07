using System;
using EStore.BL.Models._Common;

namespace EStore.BL.Models
{
    public class ArticleItem : ViewModelBase
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime? Date { get; set; }
    }
}