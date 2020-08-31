using DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Query
{
    public class GenreQuery
    {
        /// <summary>
        /// fas query na tabela de gêneros
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        static public List<Genre> GetGenres(Genre u)
        {
            using (Context db = new Context(ConnectionString.BaseApiGlobal("CatherineDB")))
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var query = db.Genres.ToList().AsQueryable();

                        if (u.Id != 0)
                        {
                            query = query.Where(r => r.Id == u.Id).ToList().AsQueryable();
                        }

                        if (u.Name != null)
                        {
                            query = query.Where(r => r.Name.Contains(u.Name)).ToList().AsQueryable();
                        }

                        //if (u.CreationDate != null)
                        //{
                        //    query = query.Where(r => r.CreationDate == u.CreationDate).ToList().AsQueryable();
                        //}

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
