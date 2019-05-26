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
    [Area("Admin")]
    public class MenuController : Controller
    {
        private readonly ILogger<MenuController> _logger;
        private readonly ISysMenuService _menuService;
        private readonly ISysModuleService _moduleService;

        public MenuController(ISysMenuService service, ILogger<MenuController> log, ISysModuleService module)
        {
            _logger = log;
            _menuService = service;
            _moduleService = module;
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
        public ActionResult DataBind(SysMenuCondition data)
        {
            var result = _menuService.GetListPaged(data);
            return Json(result);
        }

        [HttpPost]
        public ActionResult GetInfo(string id)
        {
            var result = _menuService.GetDetail(id);
            return Json(result);
        }

        [HttpPost]
        public ActionResult MenuData()
        {
            var result = _menuService.GetListAll();
            if (result.Data != null && result.Data.Count > 0)
            {
                result.Data = result.Data.Where(m => m.ParentId == "0" && string.IsNullOrEmpty(m.PageUrl)).ToList();
            }
            return Json(result);
        }

        [HttpPost]
        public ActionResult ModuleData()
        {
            var result = _moduleService.GetListAll();
            return Json(result);
        }

        // POST: Menu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(SysMenuViewModel data)
        {
            var result = _menuService.Add(data);
            return Json(result);
        }


        // POST: Menu/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SysMenuViewModel data)
        {
            var result = _menuService.Update(data);
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeStatus(string[] keys, int status)
        {
            var result = _menuService.ChangeStatus(keys, status);
            return Json(result);
        }

        // POST: Menu/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string[] keys)
        {
            var result = _menuService.DeleteLogical(keys);
            return Json(result);
        }
    }
}