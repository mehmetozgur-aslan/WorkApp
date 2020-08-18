using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Business.Interfaces
{
    public interface IAppUserService
    {
        List<AppUser> GetUserWithoutAdmin();
        List<AppUser> GetUserWithoutAdmin(out int totalPage, string name, int activePage);
    }
}
