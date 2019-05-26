using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AQ.IServices;
using AQ.ModelExtension;
using AQ.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AQ.WebMain.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly ILogger<RoleController> _logger;
        private readonly ISysRoleService _roleService;
        private readonly ISysKeyRegulationService _keyService;
        public RoleController(ISysRoleService roleService, ILogger<RoleController> logger, ISysKeyRegulationService keyService)
        {
            _logger = logger;
            _roleService = roleService;
            _keyService = keyService;
        }

        // GET: User
        public IActionResult Index()
        {
            return View();
        }

        // GET: User/Details/5
        public IActionResult Details(string id)
        {
            ViewBag.Id = id ?? "";
            return View();
        }

        [HttpPost]
        public IActionResult DataBind(SysRoleCondition data)
        {
            var result = _roleService.GetListPaged(data);
            return Json(result);
        }

        [HttpPost]
        public IActionResult GetInfo(string id)
        {
            var result = _roleService.GetDetail(id);
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(SysRoleViewModel data)
        {
            var result = _roleService.Add(data);
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SysRoleViewModel data)
        {
            var result = _roleService.Update(data);
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string[] keys)
        {
            var result = _roleService.DeleteLogical(keys);
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeStatus(string[] keys, int status)
        {
            var result = _roleService.ChangeStatus(keys, status);
            return Json(result);
        }
    }
}