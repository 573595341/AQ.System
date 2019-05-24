using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AQ.IServices;
using AQ.ModelExtension;
using AQ.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AQ.WebMain.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PermissionController : Controller
    {
        private readonly ISysPermissionService _sysPermissionService;
        private readonly ISysRolePermissionLinkService _sysRolePermissionLinkService;

        public PermissionController(
            ISysPermissionService sysPermissionService,
            ISysRolePermissionLinkService sysRolePermissionLinkService)
        {
            _sysPermissionService = sysPermissionService;
            _sysRolePermissionLinkService = sysRolePermissionLinkService;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 菜单
        /// </summary>
        /// <returns></returns>
        public IActionResult Menu()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetModuleData(string roleId)
        {
            var result = _sysPermissionService.GetModuleData(roleId);
            return Json(result);
        }

        [HttpPost]
        public IActionResult GetMenuData(string moduleId, string roleId)
        {
            var result = _sysPermissionService.GetMenuData(moduleId, roleId);
            return Json(result);
        }


        [HttpPost]
        public IActionResult SaveMenu(string roleId, ModulePermissionViewModel module, List<MenuPermissionViewModel> menu)
        {
            var result = _sysRolePermissionLinkService.UpdateMenu(roleId, module, menu);
            return Json(result);
        }

    }
}