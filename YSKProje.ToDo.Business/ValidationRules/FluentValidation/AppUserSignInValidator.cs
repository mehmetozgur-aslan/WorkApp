using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;

namespace YSKProje.ToDo.Business.ValidationRules.FluentValidation
{
    public class AppUserSignInValidator:AbstractValidator<SignInAppUserDto>
    {
        public AppUserSignInValidator()
        {
            RuleFor(i => i.UserName).NotNull().WithMessage("Kullanıcı Adı alanı boş geçilemez");
            RuleFor(i => i.Password).NotNull().WithMessage("Parola alanı boş geçilemez");
        }
    }
}
