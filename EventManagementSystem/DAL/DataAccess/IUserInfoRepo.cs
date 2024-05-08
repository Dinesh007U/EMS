using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public interface IUserInfoRepo<T>
    {
        //initializing all the methods
        public bool SaveUserInfo(T UserInfo);

        public bool DeleteUserInfo(string emailId);

        public bool UpdateUserInfo(T UserInfo);

        public T GetUserInfoByEmail(string emailId);

        public ICollection<T> GetAllUserInfo();
    }
}
