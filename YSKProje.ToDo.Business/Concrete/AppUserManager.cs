using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Business.Concrete
{
    public class AppUserManager : IAppUserService
    {
        private readonly IUserDal _userDal;

        public AppUserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public List<DualHelper> GetUsersWithMostCompletedTask()
        {
            return _userDal.GetUsersWithMostCompletedTask();
        }

        public List<DualHelper> GetUsersWithMostWorkingTask()
        {
            return _userDal.GetUsersWithMostWorkingTask();
        }

        public List<AppUser> GetUserWithoutAdmin()
        {
            return _userDal.GetUserWithoutAdmin();
        }

        public List<AppUser> GetUserWithoutAdmin(out int totalPage,string name, int activePage)
        {
            return _userDal.GetUserWithoutAdmin(out totalPage,name, activePage);
        }
    }
}
