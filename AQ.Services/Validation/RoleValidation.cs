using AQ.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AQ.Services.Validation
{
    public class RoleValidation : AbstractValidator<SysRoleViewModel>
    {
        public RoleValidation()
        {
            CascadeMode = FluentValidation.CascadeMode.StopOnFirstFailure;

            RuleSet("Add", () =>
            {
                RuleFor(m => m.Name).NotEmpty().WithMessage("角色名称为空");
            });

            RuleSet("Update", () =>
            {
                RuleFor(m => m.Id).NotEmpty().WithMessage("参数错误");
                RuleFor(m => m.Name).NotEmpty().WithMessage("角色名称为空");
            });
        }
    }
}
