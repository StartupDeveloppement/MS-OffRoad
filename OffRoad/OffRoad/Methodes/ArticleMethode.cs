using OffRoad.Context;
using OffRoad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OffRoad.Methodes
{
    public class ArticleMethode
    {
        private DBContext db = new DBContext();

        public List<ArticleComment> GetCommentairesForArticle(int idArticle)
        {
            var requeteCommentaires = from b in db.ArticleComments
                                      where b.Article.Id == idArticle
                                      select b;
            return requeteCommentaires.ToList<ArticleComment>();
        }

        public List<Article> GetArticleByCategory(int idCategory)
        {
            var requeteArticle = from b in db.Articles
                                      where b.Category.Id == idCategory
                                      select b;
            return requeteArticle.ToList<Article>();
        }

        public List<Article> GetRecentsArticles()
        {
            var requeteArticle = (
                                 from b in db.Articles
                                 select b
                                 )
                                 .Take(8).OrderByDescending(b => b.CreateDate);
            return requeteArticle.ToList<Article>();
        }
    }
}