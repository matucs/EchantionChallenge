using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebClientConsummer.Models;

namespace WebClientConsummer.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            MessagingModel mmlist = new MessagingModel();          
            return View(mmlist);
        }
        [HttpPost]
        public IActionResult Messaging(MessagingModel mm)
        {
            mm._ResultModel.Response= "hello";
            mm._ListModel.Items.Add(mm._ResultModel);

            return View("Index", mm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
