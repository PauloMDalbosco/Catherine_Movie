using DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Query
{
    static public class UserQuery
    {
        /// <summary>
        /// faz query na tabela de usuários
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        static public List<User> GetUser(User u)
        {
            using (Context db = new Context(ConnectionString.BaseApiGlobal("CatherineDB")))
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var query = db.Users.ToList().AsQueryable();

                        if (u.Id != 0)
                        {
                            query = query.Where(r => r.Id == u.Id).ToList().AsQueryable();
                        }

                        if (u.Name != null)
                        {
                            query = query.Where(r => r.Name.Contains(u.Name)).ToList().AsQueryable();
                        }

                        if (u.Email != null)
                        {
                            query = query.Where(r => r.Email.Contains(u.Email)).ToList().AsQueryable();
                        }

                        var guidCompare = new Guid("00000000-0000-0000-0000-000000000000");
                        if (u.UserGuid != guidCompare)
                        {
                            query = query.Where(r => r.UserGuid == u.UserGuid).ToList().AsQueryable();
                        }

                        if (u.Password != null)
                        {
                            query = query.Where(r => r.Password.Contains(u.Password)).ToList().AsQueryable();
                        }

                        dbContextTransaction.Commit();
                        return query.ToList();
                    }
                    catch (Exception err)
                    {
                        //escreve log
                        Common.Log.Write("UserPersistence CreateUser", err.Message);
                        dbContextTransaction.Rollback();
                        return null;
                    }
                }
            }
        }
    }
}
