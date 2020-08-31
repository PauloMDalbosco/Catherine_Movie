using DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Persistence
{
    public class LocateRegisterPersistence
    {
        static public bool CreateRegister(LocateRegister u)
        {

            using (Context db = new Context(ConnectionString.BaseApiGlobal("CatherineDB")))
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.LocateRegisters.Add(u);
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
