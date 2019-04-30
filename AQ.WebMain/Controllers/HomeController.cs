﻿using System;
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
            logger.LogDebug("Index");
            //logger.Info("Action Index");
            //throw new Exception("action error");

            //var r1 = _articleCategoryService.Insert(new ArticleCategoryViewModel()
            //{
            //    //Title = "wangfan",
            //    //Sort = 0,
            //    //ParentId = 0
            //});

            //var r2 = _articleCategoryService.GetList();
            //Program.CreateWebHostBuilder(null);
            return View();
        }

        public IActionResult Main()
        {
            //_logger.Log(Microsoft.Extensions.Logging.LogLevel.Debug, "Action Main");
            //logger.Info("Action Main2");
            return View();
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
