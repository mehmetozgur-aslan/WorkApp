using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Entities.Concrete;
using Task = YSKProje.ToDo.Entities.Concrete.Task;

namespace YSKProje.ToDo.Web.Areas.Admin.Models
{
    public class AssignPersonelToTaskListViewModel
    {
        public AppUserListViewModel AppUser { get; set; }
        public TaskListViewModel Task { get; set; }
    }
}
