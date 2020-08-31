using DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Persistence
{
    static public class UserPersistence
    {
        /// <summary>
        /// Persiste um novo usuário
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        static public bool CreateUser(User u) {

            using (Context db = new Context(ConnectionString.BaseApiGlobal("CatherineDB"))) {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Users.Add(u);
                        db.SaveChanges();
                        dbContextTransaction.Commit();
                        return true;
                    }
                    catch (Exception err)
                    {
                        //escreve log
                        Common.Log.Write("UserPersistence CreateUser", err.Message);
                        dbContextTransaction.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}