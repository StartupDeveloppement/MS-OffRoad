using OffRoad.Context;
using OffRoad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OffRoad.Methodes
{
    public class CommentaireMethodes
    {
        public EventComment GetCommentaireForEvent(int idComment, DBContext db)
        {
            var requeteCommentaires = from b in db.EventComment
                                      where b.Id == idComment
                                      select b;
            return requeteCommentaires.FirstOrDefault();
        }

        public ArticleComment GetCommentaireForArticle(int idComment, DBContext db)
        {
            var requeteCommentaires = from b in db.ArticleComments
                                      where b.Id == idComment
                                      select b;
            return requeteCommentaires.FirstOrDefault();
        }
    }
}