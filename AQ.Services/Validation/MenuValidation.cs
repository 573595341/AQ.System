using AQ.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AQ.Services.Validation
{
    public class MenuValidation : AbstractValidator<SysMenuViewModel>
    {

        public MenuValidation()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleSet("Add", () =>
            {
                RuleFor(m => m.Name).NotEmpty().WithMessage("菜单名称为空");
                RuleFor(m => m.ModuleId).NotEmpty().WithMessage("所属模块为空");
            });

            RuleSet("Update", () =>
            {
                RuleFor(m => m.Id).NotEmpty().WithMessage("参数错误");
                RuleFor(m => m.Name).NotEmpty().WithMessage("菜单名称为空");
                RuleFor(m => m.ModuleId).NotEmpty().WithMessage("所属模块为空");
                RuleFor(m => m.Id).Must((m, s) =>
                {
                    return m.ParentId != s;
                }).WithMessage("上级菜单不能设为自己");
            });
        }
    }
}
