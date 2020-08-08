using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Entities.Interfaces;

namespace YSKProje.ToDo.Entities.Concrete
{
    public class Urgent : ITable
    {
        public int Id { get; set; }
        public string Definition { get; set; }

        public List<Task> Tasks { get; set; }
    }
}
