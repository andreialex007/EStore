using System;
using System.Collections.Generic;
using System.Linq;
using EStore.BL.Extensions;
using EStore.BL.Models;
using EStore.BL.Services._Common;
using EStore.DL.Mapping;

namespace EStore.BL.Services
{
    public class ArticleService : ServiceBase
    {
        public ArticleService(EStoreEntities entities) : base(entities)
        {
        }

        public void Save(ArticleItem item)
        {
            var article = new tblArticle
            {
                Id = item.Id,
                Date = DateTime.Now,
                Text = item.Text,
                Title = item.Title
            };

            Db.Save(article);

            item.Id = article.Id;
        }

        public List<ArticleItem> All()
        {
            var items = Db.Set<tblArticle>()
                .Select(x => new ArticleItem
                {
                    Id = x.Id,
                    Date = x.Date,
                    Text = x.Text,
                    Title = x.Title
                })
                .ToList();
            return items;
        }
    }
}