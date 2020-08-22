using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.Entities.Concrete;
using Task = YSKProje.ToDo.Entities.Concrete.Task;

namespace YSKProje.ToDo.Web.TagHelpers
{
    [HtmlTargetElement("TaskListByUser")]
    public class TaskListByUserTagHelper : TagHelper
    {
        private readonly ITaskService _taskService;
        public TaskListByUserTagHelper(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public int AppUserId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            List<Task> taskList = _taskService.GetTasksByAppUserId(AppUserId);

            int completedTaskCount = taskList.Where(i => i.State).Count();
            int notCompletedTaskCount = taskList.Where(i => !i.State).Count();

            string htmlString = $"<strong> Tamamladığı görev sayısı: </strong> {completedTaskCount}<br> <strong> Üstünde çalıştığı görev sayısı: </strong>{notCompletedTaskCount}";

            output.Content.SetHtmlContent(htmlString);

        }
    }
}
