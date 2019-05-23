using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AQ.Models;
using AQ.ViewModels;
using AutoMapper;

namespace AQ.WebMain.Profiles
{
    public class ServicesMapperProfiles : Profile
    {
        public ServicesMapperProfiles()
        {
            //CreateMap<AQsysKeyRegulation, AQsysKeyRegulationViewModel>();
            //CreateMap<AQsysMenu, AQsysMenuViewModel>();
            //CreateMap<AQsysModule, AQsysModuleViewModel>();

            //主键规则
            CreateMap<SysKeyRegulation, SysKeyRegulationViewModel>();

            #region SysMenu
            CreateMap<SysMenu, SysMenuViewModel>();
            CreateMap<SysMenuViewModel, SysMenu>().ConvertUsing((s, context) =>
            {
                return new SysMenu()
                {
                    Id = s.Id,
                    Name = s.Name ?? string.Empty,
                    Level = s.Level,
                    PageUrl = s.PageUrl ?? string.Empty,
                    ParentId = s.ParentId ?? string.Empty,
                    ParentName = s.ParentName ?? string.Empty,
                    ModuleId = s.ModuleId ?? string.Empty,
                    ModuleName = s.ModuleName ?? string.Empty,
                    Ico = s.Ico ?? string.Empty,
                    Type = s.Type ?? string.Empty,
                    Sort = s.Sort,
                    Status = s.Status,
                    IsDelete = s.IsDelete,
                    CreateTime = s.CreateTime,
                    CreateUser = s.CreateUser ?? string.Empty,
                    ModifyTime = s.ModifyTime,
                    ModifyUser = s.ModifyUser ?? string.Empty
                };
            });
            #endregion

            #region SysModule
            CreateMap<SysModule, SysModuleViewModel>();
            CreateMap<SysModuleViewModel, SysModule>().ConvertUsing((s, context) =>
            {
                var result = new SysModule()
                {
                    Id = s.Id,
                    Name = s.Name ?? string.Empty,
                    Ico = s.Ico ?? string.Empty,
                    Sort = s.Sort,
                    Status = s.Status,
                    IsDelete = s.IsDelete,
                    CreateTime = s.CreateTime,
                    CreateUser = s.CreateUser ?? string.Empty,
                    ModifyTime = s.ModifyTime,
                    ModifyUser = s.ModifyUser ?? string.Empty,
                };
                return result;
            });
            #endregion

            #region SysUser
            CreateMap<SysUser, SysUserViewModel>();
            CreateMap<SysUserViewModel, SysUser>().ConvertUsing(s =>
            new SysUser()
            {
                Id = s.Id,
                Account = s.Account ?? string.Empty,
                Alias = s.Alias ?? string.Empty,
                BankCard = s.BankCard ?? string.Empty,
                Birthday = s.Birthday,
                CName = s.CName ?? string.Empty,
                EName = s.EName ?? string.Empty,
                IdCard = s.IdCard ?? string.Empty,
                JobCode = s.JobCode ?? string.Empty,
                Mobile = s.Mobile ?? string.Empty,
                NickName = s.NickName ?? string.Empty,
                Photo = s.Photo ?? string.Empty,
                PresentAddrress = s.PresentAddrress ?? string.Empty,
                Pwd = s.Pwd ?? string.Empty,
                Sex = s.Sex,
                Status = s.Status,
                IsDelete = s.IsDelete,
                CreateTime = s.CreateTime,
                CreateUser = s.CreateUser ?? string.Empty,
                ModifyTime = s.ModifyTime,
                ModifyUser = s.ModifyUser ?? string.Empty
            });
            #endregion

            #region SysRole
            CreateMap<SysRole, SysRoleViewModel>();
            CreateMap<SysRoleViewModel, SysRole>().ConvertUsing(s =>
            new SysRole()
            {
                Id = s.Id,
                Name = s.Name ?? string.Empty,
                Status = s.Status,
                IsDelete = s.IsDelete,
                CreateTime = s.CreateTime,
                CreateUser = s.CreateUser ?? string.Empty,
                ModifyTime = s.ModifyTime,
                ModifyUser = s.ModifyUser ?? string.Empty
            });
            #endregion

            #region SysPermission
            CreateMap<SysPermission, SysPermissionViewModel>();
            CreateMap<SysPermissionViewModel, SysPermission>().ConvertUsing(s =>
            new SysPermission()
            {
                Id = s.Id,
                PerType = s.PerType,
                ResourceId = s.ResourceId,
                Status = s.Status,
                IsDelete = s.IsDelete,
                CreateTime = s.CreateTime,
                CreateUser = s.CreateUser ?? string.Empty,
                ModifyTime = s.ModifyTime,
                ModifyUser = s.ModifyUser ?? string.Empty
            });
            #endregion
        }
    }
}
