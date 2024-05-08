using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    //This is the serived class for IUserInfoRepo
    public class UserInfoRepo : IUserInfoRepo<UserInfo>
    {
        //This method is used for Deleting Userinfos
        public bool DeleteUserInfo(string emailId)
        {
            using (EventManagementDbContext dbContext = new EventManagementDbContext())
            {
                var allUserDetails = dbContext.UserInfos.Where(d => d.IsDeleted != true).ToList();

                var user = allUserDetails.Where(e => e.EmailId.Equals(emailId)).FirstOrDefault();

                if (user != null)
                {
                    dbContext.UserInfos.Remove(user);
                    dbContext.SaveChanges();
                    return true;
                }
            }
            return false;

        }

        //This method is used for Getting all the userInfo
        public ICollection<UserInfo> GetAllUserInfo()
        {
            using (EventManagementDbContext dbContext = new EventManagementDbContext())
            {
                return dbContext.UserInfos.Where(d => d.IsDeleted != true).ToList();
            }
        }

        //This method is used to get userInfo by emailId
        public UserInfo GetUserInfoByEmail(string emailId)
        {
            using (EventManagementDbContext dbContext = new EventManagementDbContext())
            {
                var allUserDetails = dbContext.UserInfos.Where(d => d.IsDeleted != true).ToList();

                return allUserDetails.Where(e => e.EmailId.Equals(emailId)).FirstOrDefault();

            }
        }

        //This method is used to save userInfo
        public bool SaveUserInfo(UserInfo userInfo)
        {
            using (EventManagementDbContext dbContext = new EventManagementDbContext())
            {
                dbContext.UserInfos.Add(userInfo);

                dbContext.SaveChanges();

                return true;
            }
        }

        //This method is used to update the userInfo
        public bool UpdateUserInfo(UserInfo userInfo)
        {

            using (EventManagementDbContext dbContext = new EventManagementDbContext())
            {
                var allUserDetails = dbContext.UserInfos.Where(d => d.IsDeleted != true).ToList();

                var oldDetails = allUserDetails.Where(d => d.EmailId == userInfo.EmailId).FirstOrDefault();

               if(oldDetails != null)
                {
                    oldDetails.UserName = userInfo.UserName;

                    oldDetails.Password = userInfo.Password;

                    dbContext.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
