using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AQ.WebMain.Models;
using AQ.IServices;
using AQ.ViewModels;
using Microsoft.AspNetCore.Mvc.Filters;
using AQ.WebMain.Filter;
using AQ.WebMain.Commons;
using Microsoft.Extensions.Logging;

namespace AQ.WebMain.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        ILogger<HomeController> logger;
        public HomeController(ILogger<HomeController> log)
        {
            logger = log;
        }

        [NonAuthorization]
        [TypeFilter(typeof(ActionFilter))]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Main()
        {
            //_logger.Log(Microsoft.Extensions.Logging.LogLevel.Debug, "Action Main");
            //logger.Info("Action Main2");
            return View();
        }

        public IActionResult LoadMenu()
        {
            var data = new
            {
                contentManagement = new List<object>() {
                    new {
                        title = "文章列表",
                        icon = "icon-text",
                        href="/Home/NewsList",
                        spread= false
                    }
                },
                memberCenter = new List<object>() {
                    new {
                        title= "文章列表",
                        icon="icon-text",
                        href="/Home/NewsList",
                        spread= false
                    }
                },
                systemeSttings = new List<object>() {
                    new {
                        title= "文章列表",
                        icon="icon-text",
                        href="/Home/NewsList",
                        spread= false
                    }
                },
                seraphApi = new List<object>() {
                    new {
                        title= "文章列表",
                        icon="icon-text",
                        href="/Home/NewsList",
                        spread= false
                    }
                }
            };
            return Json(data);
        }

        public IActionResult NewsList()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
