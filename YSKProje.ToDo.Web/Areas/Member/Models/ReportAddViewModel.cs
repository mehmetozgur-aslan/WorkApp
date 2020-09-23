using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Web.Areas.Member.Models
{
    public class ReportAddViewModel
    {
        public int TaskId { get; set; }

        //[Required(ErrorMessage ="Tanım boş geçilemez")]
        //[Display(Name ="Tanım")]
        public string Definition { get; set; }

        //[Required(ErrorMessage = "Detay boş geçilemez")]
        //[Display(Name = "Detay")]
        public string Detail { get; set; }

         public Task Task { get; set; }


    }
}
