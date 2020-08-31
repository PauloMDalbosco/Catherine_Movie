using DAO.Models;

namespace Catherine_Movie.BusinessRules
{
    static public class UserBusinessRules
    {
        /// <summary>
        /// Verifica se usuário já existe no banco
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        static public bool VerifyUserCont(string user)
        {
            User u = new User();
            u.Name = user;

            if(DAO.Query.UserQuery.GetUser(u).Count > 0)
                return false;

            return true;
        }
    }
}