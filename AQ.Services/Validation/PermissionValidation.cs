using AQ.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AQ.Services.Validation
{
    public class PermissionValidation : AbstractValidator<SysPermissionViewModel>
    {
        public PermissionValidation()
        {
            CascadeMode = FluentValidation.CascadeMode.StopOnFirstFailure;

            RuleSet("Menu", () =>
            {
                RuleFor(m => m.ResourceId).NotEmpty().WithMessage("[ResourceId]参数错误");
                RuleFor(m => m.PerType).NotEmpty().WithMessage("[PerType]参数错误");
            });
        }

    }
}
