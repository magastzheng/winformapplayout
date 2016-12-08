using DBAccess.Permission;
using Model.Permission;
using System.Collections.Generic;

namespace BLL.Permission
{
    public class UserBLL
    {
        private UserDAO _userdao = new UserDAO();

        public UserBLL()
        { 
        
        }

        public int Create(User user)
        { 
            return _userdao.Create(user);
        }

        public int Update(User user)
        {
            return _userdao.Update(user);
        }

        public int Delete(string operatorNo)
        {
            return _userdao.Delete(operatorNo);
        }

        public User Get(string operatorNo)
        {
            return _userdao.Get(operatorNo);
        }

        public User GetById(int userId)
        {
            return _userdao.GetById(userId);
        }

        public List<User> GetAll()
        {
            return _userdao.Get();
        }

        public User GetUser(string operatorNo)
        {
            var user = Get(operatorNo);

            //If there is no user in the database, create new one.
            if (user == null || string.IsNullOrEmpty(user.Operator))
            {
                user = new User 
                {
                    Operator = operatorNo,
                    Name = operatorNo,
                    Status = UserStatus.Active,
                };

                int userId = Create(user);
                user.Id = userId;
            }

            return user;
        }
    }
}
