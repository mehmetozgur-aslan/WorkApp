using System;
using System.Collections.Generic;
using System.Text;

namespace YSKProje.ToDo.DTO.DTOs.UrgentDtos
{
   public class UpdateUrgentDto
    {
        //[Display(Name = "Tanım")]
        //[Required(ErrorMessage = "Tanım alanı boş geçilemez.")]
        public string Definition { get; set; }
        public int Id { get; set; }
    }
}
