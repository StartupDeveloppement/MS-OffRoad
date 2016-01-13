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
    }
}