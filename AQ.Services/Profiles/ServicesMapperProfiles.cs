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

            CreateMap<SysKeyRegulation, SysKeyRegulationViewModel>();
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

            CreateMap<SysModuleViewModel, SysModule>().ConstructUsing(s =>
            new SysModule()
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
            });
        }
    }
}
