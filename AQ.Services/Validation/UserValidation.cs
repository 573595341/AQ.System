using AQ.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AQ.Services.Validation
{
    /// <summary>
    /// 数据验证
    /// </summary>
    public class UserValidation : AbstractValidator<SysUserViewModel>
    {
        public UserValidation()
        {
            CascadeMode = FluentValidation.CascadeMode.StopOnFirstFailure;

            RuleSet("Add", () =>
            {
                RuleFor(m => m.Account).NotEmpty().WithMessage("用户账号为空");
                RuleFor(m => m.Pwd).NotEmpty().WithMessage("用户密码为空");
            });

            RuleSet("Update", () =>
            {
                RuleFor(m => m.Id).NotEmpty().WithMessage("参数错误");
                RuleFor(m => m.Account).NotEmpty().WithMessage("用户账号为空");
                RuleFor(m => m.Pwd).NotEmpty().WithMessage("用户密码为空");
            });
        }
    }
}
