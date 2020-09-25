using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DTO.DTOs.ReportDtos
{
    public class UpdateReportDto
    {
        public int TaskId { get; set; }
        public int ReportId { get; set; }

        //[Required(ErrorMessage = "Tanım boş geçilemez")]
        //[Display(Name = "Tanım")]
        public string Definition { get; set; }

        //[Required(ErrorMessage = "Detay boş geçilemez")]
        //[Display(Name = "Detay")]
        public string Detail { get; set; }

        public Task Task { get; set; }
    }
}
