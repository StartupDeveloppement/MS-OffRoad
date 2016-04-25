using OffRoad.Context;
using OffRoad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OffRoad.Methodes
{
    public class EventMethode
    {
        private DBContext db = new DBContext();

        public List<Event> GetRecentsEvents()
        {
            var requeteEvent = (
                                 from b in db.Events
                                 select b
                                 )
                                 .Take(3).OrderByDescending(b => b.CreateDate);
            return requeteEvent.ToList<Event>();
        }

        public List<EventComment> GetCommentairesForEvent(int idEvent)
        {
            var requeteCommentaires = from b in db.EventComment
                                      where b.Event.Id == idEvent
                                      select b;
            return requeteCommentaires.ToList<EventComment>();
        }
    }
}