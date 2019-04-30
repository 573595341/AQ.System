using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AQ.IServices;
using AQ.ModelExtension;
using AQ.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AQ.WebMain.Controllers.Admin
{
    public class MenuController : Controller
    {
        ILogger<MenuController> logger;
        private ISysMenuService menuService;
        private ISysModuleService moduleService;

        public MenuController(ISysMenuService service, ILogger<MenuController> log, ISysModuleService module)
        {
            logger = log;
            menuService = service;
            moduleService = module;
        }

        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

        // GET: Menu/Details/5
        public ActionResult Details(string id)
        {
            ViewBag.Id = id ?? "";
            return View();
        }

        // POST: Menu/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LoadData(SysMenuCondition data)
        {
            var result = menuService.GetListPaged(data);
            return Json(result);
        }

        [HttpPost]
        public ActionResult GetInfo(string id)
        {
            var result = menuService.GetDetail(id);
            return Json(result);
        }

        [HttpPost]
        public ActionResult MenuData()
        {
            var result = menuService.GetListAll();
            return Json(result);
        }

        [HttpPost]
        public ActionResult ModuleData()
        {
            var result = moduleService.GetListAll();
            return Json(result);
        }

        // POST: Menu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(SysMenuViewModel data)
        {
            var result = menuService.Add(data);
            return Json(result);
        }


        // POST: Menu/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SysMenuViewModel data)
        {
            var result = menuService.Update(data);
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeStatus(string[] keys, int status)
        {
            var result = menuService.ChangeStatus(keys, status);
            return Json(result);
        }

        // POST: Menu/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string[] keys)
        {
            var result = menuService.DeleteLogical(keys);
            return Json(result);
        }
    }
}