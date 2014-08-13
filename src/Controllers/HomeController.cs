using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinqToTwitter;
using TheTopPlays.Services;

namespace TheTopPlays.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetVideos(FormCollection form)
        {
            var search = form["search"];
            var service = new YouTubeServices();
            var model = service.GetMatchingVideos(search);

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult EmailMessage(FormCollection form)
        {
            var from = form["from"];
            var to = form["to"];
            var search = form["search"];
            var message = form["message"];
            var videoId = form["videoid"];

            var emailService = new EmailServices
            {
                From = new Mandrill.EmailAddress {name = @from, email = @from},
                To = new List<Mandrill.EmailAddress> {new Mandrill.EmailAddress {email = to, name = ""}},
                Subject = "Watch this play!",
                Message = string.Format("{0} : https://www.youtube.com/watch?v={1}", message, videoId)
            };

            emailService.SendEmail();

            var youtubeService = new YouTubeServices();
            var model = youtubeService.GetMatchingVideos(search);
            return View("Index", model);
        }
        
    }
}