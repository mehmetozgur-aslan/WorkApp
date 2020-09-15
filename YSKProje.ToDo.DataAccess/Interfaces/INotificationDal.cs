using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Interfaces
{
    public interface INotificationDal : IGenericDal<Notification>
    {
        List<Notification> GetNotReadByUserId(int appUserId);
        int GetNotReadNotificationCountByUserId(int appUserId);
    }
}
