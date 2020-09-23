using System;
using System.Collections.Generic;
using System.Text;

namespace YSKProje.ToDo.DTO.DTOs.ReportDtos
{
    public class AddReportDto
    {
        public int TaskId { get; set; }

        //[Required(ErrorMessage = "Tanım boş geçilemez")]
        //[Display(Name = "Tanım")]
        public string Definition { get; set; }

        //[Required(ErrorMessage = "Detay boş geçilemez")]
        //[Display(Name = "Detay")]
        public string Detail { get; set; }

        public Task Task { get; set; }
    }
}
