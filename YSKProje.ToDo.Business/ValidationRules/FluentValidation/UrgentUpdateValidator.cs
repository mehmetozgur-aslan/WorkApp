using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.DTO.DTOs.UrgentDtos;

namespace YSKProje.ToDo.Business.ValidationRules.FluentValidation
{
    public class UrgentUpdateValidator : AbstractValidator<UpdateUrgentDto>
    {
        public UrgentUpdateValidator()
        {
            RuleFor(I => I.Definition).NotNull().WithMessage("Tanım alanı boş geçilemez.");
        }
    }
}
