using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DTO.DTOs.TaskDtos
{
    public class ListTaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool State { get; set; }
        public DateTime CreatedDate { get; set; }

        public int UrgentId { get; set; }
        //public Urgent Urgent { get; set; }
    }
}

