using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AQ.IServices;
using AQ.ModelExtension;
using AQ.ViewModels;

namespace AQ.WebMain.Controllers.Admin
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly ISysUserServiceNew _userService;
        private readonly ISysKeyRegulationService _keyService;
        public UserController(ISysUserServiceNew userService, ILogger<UserController> logger, ISysKeyRegulationService keyService)
        {
            _logger = logger;
            _userService = userService;
            _keyService = keyService;
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // GET: User/Details/5
        public ActionResult Details(string id)
        {
            ViewBag.Id = id ?? "";
            return View();
        }

        [HttpPost]
        public ActionResult DataBind(SysUserCondition data)
        {
            var result = _userService.GetListPaged(data);
            return Json(result);
        }

        [HttpPost]
        public ActionResult GetInfo(string id)
        {
            var result = _userService.GetDetail(id);
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(SysUserViewModel data)
        {
            var result = _userService.Add(data);
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SysUserViewModel data)
        {
            var result = _userService.Update(data);
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string[] keys)
        {
            var result = _userService.DeleteLogical(keys);
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeStatus(string[] keys, int status)
        {
            var result = _userService.ChangeStatus(keys, status);
            return Json(result);
        }
    }
}