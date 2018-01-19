using GraphAPIAspNetExample.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GraphAPIAspNetExample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public async Task<ActionResult> Me()
        {
            string res = String.Empty;

            string accessToken = Request.Headers["x-ms-token-aad-access-token"];

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync("https://graph.microsoft.com/v1.0/me");
            var cont = await response.Content.ReadAsStringAsync();

            Me me = JsonConvert.DeserializeObject<Me>(cont);

            ViewData["UserPrincipalName"] = me.UserPrincipalName;
            ViewData["DisplayName"] = me.DisplayName;
            ViewData["Mail"] = me.Mail;
            ViewData["me"] = cont;

            return View();
        }
    }
}