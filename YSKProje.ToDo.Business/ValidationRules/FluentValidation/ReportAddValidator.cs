using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.DTO.DTOs.ReportDtos;

namespace YSKProje.ToDo.Business.ValidationRules.FluentValidation
{
   public class ReportAddValidator:AbstractValidator<AddReportDto>
    {
        public ReportAddValidator()
        {
            RuleFor(i => i.Definition).NotNull().WithMessage("Tanım alanı boş geçilemez.");
            RuleFor(i => i.Detail).NotNull().WithMessage("Detay alanı boş geçilemez.");
        }
    }
}
