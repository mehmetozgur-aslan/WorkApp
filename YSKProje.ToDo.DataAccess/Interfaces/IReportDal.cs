using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Interfaces
{
    public interface IReportDal : IGenericDal<Report>
    {
        Report GetReportWithTaskById(int id);
        int GetReportCountByUserId(int userId);
    }
}
