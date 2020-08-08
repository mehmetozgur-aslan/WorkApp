using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Business.Concrete
{
    public class ReportManager : IReportService
    {

        EfReportRepository reportRepository;

        public ReportManager()
        {
            reportRepository = new EfReportRepository();
        }

        public void Delete(Report entity)
        {
            reportRepository.Delete(entity);
        }

        public List<Report> GetAll()
        {
            return reportRepository.GetAll();
        }

        public Report GetById(int id)
        {
            return reportRepository.GetById(id);
        }

        public void Save(Report entity)
        {
            reportRepository.Save(entity);
        }

        public void Update(Report entity)
        {
            reportRepository.Update(entity);
        }
    }
}
