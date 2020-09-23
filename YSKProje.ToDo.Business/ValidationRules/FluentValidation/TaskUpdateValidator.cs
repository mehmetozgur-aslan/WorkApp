using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.DTO.DTOs.TaskDtos;

namespace YSKProje.ToDo.Business.ValidationRules.FluentValidation
{
   public class TaskUpdateValidator:AbstractValidator<UpdateTaskDto>
    {
        public TaskUpdateValidator()
        {
            RuleFor(i => i.Name).NotNull().WithMessage("Ad alanı gereklidir.");
            RuleFor(i => i.UrgentId).ExclusiveBetween(1, int.MaxValue).WithMessage("Lütfen bir aciliyet durumu seçiniz.");
        }
    }
}
