using DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Persistence
{
    static public class MoviePersistence
    {
        static public bool CreateMovie(Movie u)
        {

            using (Context db = new Context(ConnectionString.BaseApiGlobal("CatherineDB")))
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        u.CreationDate = DateTime.Now;
                        u.Active = true;
                        u.Del = false;
                        db.Movies.Add(u);
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
