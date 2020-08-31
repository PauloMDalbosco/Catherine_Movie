using DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace DAO.Query
{
    static public class LocationQuery
    {
        static public List<Location> GetLocations()
        {
            using (IDbConnection db = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=CatherineDB;User Id=sa;Password=at4937*@;MultipleActiveResultSets=True"))
            {

                    try
                    {
                        List<Location> l = new List<Location>();
                        var list = db.Query<Location>("Select * From Locations").ToList();
                        return list;
                    }
                    catch (Exception err)
                    {
                        //escreve log
                        Common.Log.Write("UserPersistence CreateUser", err.Message);
                        return null;
                    }
            }
        }

        static public List<LocateRegister> GetLocateRegister(LocateRegister u)
        {
            using (Context db = new Context(ConnectionString.BaseApiGlobal("CatherineDB")))
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        dbContextTransaction.Commit();
                        return db.LocateRegisters.ToList();
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
