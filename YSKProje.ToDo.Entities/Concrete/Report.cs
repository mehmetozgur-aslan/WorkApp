using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Entities.Interfaces;

namespace YSKProje.ToDo.Entities.Concrete
{
    public class Report : ITable
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }

        public int TaskId { get; set; }
        public Task Task { get; set; }
    }
}
