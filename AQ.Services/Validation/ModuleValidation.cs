using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using AQ.ModelExtension;
using AQ.ViewModels;

namespace AQ.Services.Validation
{
    public class ModuleValidation : AbstractValidator<SysModuleViewModel>
    {
        public ModuleValidation()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleSet("Add", () =>
            {
                RuleFor(m => m.Name).NotEmpty().WithMessage("模块名称为空");
            });

            RuleSet("Update", () =>
            {
                RuleFor(m => m.Id).NotEmpty().WithMessage("参数错误");
                RuleFor(m => m.Name).NotEmpty().WithMessage("模块名称为空");
            });
        }
    }
}
