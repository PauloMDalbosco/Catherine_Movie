using DAO.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Delete
{
    /// <summary>
    /// deleta registro de filme
    /// </summary>
    static public class MoviesDelete
    {
        static public bool DeleteMovie(int Id)
        {

            using (Context db = new Context(ConnectionString.BaseApiGlobal("CatherineDB")))
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Movie u = db.Movies.Find(Id);
                        db.Entry(u).State = EntityState.Deleted;
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
