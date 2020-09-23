using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;

namespace YSKProje.ToDo.Business.ValidationRules.FluentValidation
{
    public class AppUserAddValidator : AbstractValidator<AddAppUserDto>
    {
        public AppUserAddValidator()
        {
            RuleFor(i => i.UserName).NotNull().WithMessage("Kullanıcı Adı boş geçilemez");
            RuleFor(i => i.Password).NotNull().WithMessage("Parola alanı boş geçilemez");
            RuleFor(i => i.ConfirmPassword).NotNull().WithMessage("Parola onay alanı boş geçilemez");
            RuleFor(i => i.ConfirmPassword).Equal(i => i.Password).WithMessage("Parolalar eşleşmiyor");
            RuleFor(i => i.Email).NotNull().WithMessage("Email alanı boş geçilemez").EmailAddress().WithMessage("Geçersiz e-Posta");
            RuleFor(i => i.Name).NotNull().WithMessage("Ad alanı boş geçilemez");
            RuleFor(i => i.Surname).NotNull().WithMessage("Soyad alanı boş geçilemez");

        }
    }
}
