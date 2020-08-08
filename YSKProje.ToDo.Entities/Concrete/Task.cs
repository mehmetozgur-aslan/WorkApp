using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YSKProje.ToDo.Entities.Interfaces;

namespace YSKProje.ToDo.Entities.Concrete
{
    public class Task : ITable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool State { get; set; }
        public DateTime CreatedDate { get; set; }

        public int? AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int UrgentId { get; set; }
        public Urgent Urgent { get; set; }

        public List<Report> Reports { get; set; }
    }
}
