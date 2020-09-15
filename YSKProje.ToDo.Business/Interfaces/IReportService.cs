using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Business.Interfaces
{
    public interface IReportService:IGenericService<Report>
    {
        Report GetReportWithTaskById(int id);
        int GetReportCountByUserId(int userId);

    }
}
