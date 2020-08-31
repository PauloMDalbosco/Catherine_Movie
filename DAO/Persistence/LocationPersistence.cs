using DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Persistence
{
    static public class LocationPersistence
    {
        static public int CreateLocation(Location u)
        {
            using (Context db = new Context(ConnectionString.BaseApiGlobal("CatherineDB")))
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        u.LocationDate = DateTime.Now;
                        db.Locations.Add(u);
                        db.SaveChanges();
                        dbContextTransaction.Commit();
                        return u.Id;
                    }
                    catch (Exception err)
                    {
                        //escreve log
                        Common.Log.Write("UserPersistence CreateUser", err.Message);
                        dbContextTransaction.Rollback();
                        return 0;
                    }
                }
            }
        }
    }
}
