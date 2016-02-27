using System;
using System.Collections.Generic;
using System.Linq;
using EStore.BL.Extensions;
using EStore.BL.Models;
using EStore.BL.Models._Common;
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

        public ArticleItem Edit(long id)
        {
            var articleItem = new ArticleItem();

            if (id != 0)
            {
                articleItem = Db
                    .Set<tblArticle>()
                    .Select(x => new ArticleItem
                    {
                        Id = x.Id,
                        Date = x.Date,
                        Text = x.Text,
                        Title = x.Title
                    })
                    .Single(x => x.Id == id);
            }

            return articleItem;
        }

        public SearchModel<ArticleItem> Search(
            string searchTerm,
            string orderBy,
            bool isAsc = false,
            int? take = null,
            int? skip = null)
        {
            searchTerm = (searchTerm ?? string.Empty).Trim().ToLower();
            var totalRecords = Db.Set<tblArticle>().Count();

            var query = Db.Set<tblArticle>()
                 .Select(x => new ArticleItem
                 {
                     Id = x.Id,
                     Title = x.Title,
                     Date = x.Date,
                     Text = x.Text
                 });

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(x =>
                    x.Text.ToLower().Contains(searchTerm) ||
                    x.Title.ToLower().Contains(searchTerm));
            }


            var recordsFiltered = 0;

            var items = query
                .OrderBy(orderBy, isAsc)
                .TakePage(take, skip)
                .ToList();

            var model = new SearchModel<ArticleItem>
            {
                recordsTotal = totalRecords,
                data = items,
                recordsFiltered = recordsFiltered
            };

            return model;
        }

        public void AppendData(ArticleItem item)
        {

        }
    }
}