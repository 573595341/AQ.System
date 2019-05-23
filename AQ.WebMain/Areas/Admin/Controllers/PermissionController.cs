using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AQ.IServices;
using AQ.ModelExtension;
using Microsoft.AspNetCore.Mvc;

namespace AQ.WebMain.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PermissionController : Controller
    {
        ISysMenuService _menuService;

        public PermissionController(ISysMenuService sysMenuService)
        {
            _menuService = sysMenuService;

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
        public IActionResult GetMenuData()
        {
            var result = new BaseResult<List<Object>>() { Data = new List<object>() };
            var data = _menuService.GetListAll();
            result.ResultCode = data.ResultCode;
            result.ResultMsg = data.ResultMsg;
            data.Data.ForEach(menu =>
            {
                result.Data.Add(new
                {
                    Id = menu.Id,
                    Name = menu.Name,
                    ParentId = menu.ParentId
                });
            });
            return Json(result);
        }

    }
}