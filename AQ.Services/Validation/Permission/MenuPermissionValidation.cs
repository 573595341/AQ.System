using AQ.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AQ.Services.Validation
{
    public class MenuPermissionValidation : AbstractValidator<MenuPermissionViewModel>
    {
        public MenuPermissionValidation()
        {
            CascadeMode = FluentValidation.CascadeMode.StopOnFirstFailure;

            RuleSet("Save", () =>
            {
                RuleFor(m => m.Id).NotEmpty().WithMessage("[ResourceId]参数错误");
                RuleFor(m => m.Name).NotEmpty().WithMessage("[PerType]参数错误");
                RuleFor(m => m.Value).Must((m, s) =>
                {
                    return m.Value >= 0;
                }).WithMessage("上级菜单不能设为自己");
            });
        }
    }
}
