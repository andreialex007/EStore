using System;
using System.ComponentModel.DataAnnotations;
using EStore.BL.Models._Common;

namespace EStore.BL.Models
{
    public class ArticleItem : ViewModelBase
    {
        public long Id { get; set; }

        [Required]
        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Текст статьи")]
        public string Text { get; set; }
        public DateTime? Date { get; set; }
    }


    public class PublicArticleItem : ArticleItem
    {
        public virtual string PageTitle
        {
            get { return Title; }
            set { }
        }

        public string PageSmallTitle { get; set; }
    }
}