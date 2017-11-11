using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessCard.Controllers
{
    public class HomeController : Controller
    {
        BusinessCardEntities bce=new BusinessCardEntities();
        public ActionResult Index()
        {
            List<Cards> cards = bce.Cards.OrderByDescending(x => x.Importance).ToList(); //Öneme göre sıralayıp listeliyor
            return View(cards);
        }

        public ActionResult AddBusinessCard()
        {           
            return View();
        }

        [HttpPost]
        public ActionResult AddBusinessCard(Cards card)
        {
            Cards cardInfo = new Cards
            {
                Name = card.Name,
                Surname = card.Surname,
                Phone = card.Phone,
                Address = card.Address,
                Email = card.Email,              
                Importance=card.Importance,
                Place=card.Place,
                Time=card.Time
            };//Formdan gelen modeli oluşturuyor
            bce.Cards.Add(cardInfo);
            bce.SaveChanges(); //DB ye kaydediyor
            return RedirectToAction("BusinessCards");
        }

        [HttpPost]
        public ActionResult DeleteCard(int id)
        {
            var deletingCard = new Cards { id = id }; //Formdan gelen id değerini alıp o idye ait veriyi siliyor
            bce.Entry(deletingCard).State = EntityState.Deleted;
            bce.SaveChanges();
            return RedirectToAction("BusinessCards");
        }

    }
}