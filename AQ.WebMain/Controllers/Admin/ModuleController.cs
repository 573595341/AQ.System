using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AQ.IServices;
using AQ.ModelExtension;
using AQ.Services.Validation;
using AQ.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AQ.WebMain.Controllers.Admin
{
    public class ModuleController : Controller
    {
        private ISysModuleService moduleService;
        public ModuleController(ISysModuleService service)
        {
            moduleService = service;
        }

        // GET: Module
        public ActionResult Index()
        {
            return View();
        }

        // GET: Module/Details/5
        public ActionResult Details(string id)
        {
            ViewBag.Id = id ?? "";
            return View();
        }

        [HttpPost]
        public ActionResult LoadData(SysModuleCondition data)
        {
            var result = moduleService.GetListPaged(data);
            return Json(result);
        }

        [HttpPost]
        public ActionResult GetInfo(string id)
        {
            var result = moduleService.GetDetail(id);
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(SysModuleViewModel data)
        {
            var result = moduleService.Add(data);
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SysModuleViewModel data)
        {
            var result = moduleService.Update(data);
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string[] keys)
        {
            var result = moduleService.DeleteLogical(keys);
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeStatus(string[] keys, int status)
        {
            var result = moduleService.ChangeStatus(keys, status);
            return Json(result);
        }

    }
}