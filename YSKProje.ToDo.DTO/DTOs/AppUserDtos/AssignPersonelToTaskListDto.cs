using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.DTO.DTOs.TaskDtos;

namespace YSKProje.ToDo.DTO.DTOs.AppUserDtos
{
    public class AssignPersonelToTaskListDto
    {
        public ListAppUserDto AppUser { get; set; }
        public ListTaskDto Task { get; set; }
    }
}
