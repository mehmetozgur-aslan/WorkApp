using System;
using System.Collections.Generic;
using System.Text;

namespace YSKProje.ToDo.DTO.DTOs.TaskDtos
{
    public class UpdateTaskDto
    {
        public int Id { get; set; }

        //[Display(Name = "Ad")]
        //[Required(ErrorMessage = "Ad alanı gereklidir.")]
        public string Name { get; set; }
        //[Display(Name = "Açıklama")]
        //[Required(ErrorMessage = "Açıklama alanı gereklidir.")]
        public string Description { get; set; }
        //[Range(0, int.MaxValue, ErrorMessage = "Lütfen bir aciliyet durumu seçiniz.")]
        public int UrgentId { get; set; }
    }
}
