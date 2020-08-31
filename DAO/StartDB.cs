using DAO.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class StartDB
    {
        static public void CreateTables(bool deleteBD)
        {
            try
            {
                using (Context db = new Context(ConnectionString.BaseApiGlobal("CatherineDB")))
                {
                    // se flag for true deleta o banco
                    if (deleteBD)
                        db.Database.Delete();

                    // cria tabela de usuários e persiste dados padrões
                    var user = new User { Name = "Paulo", Password = Common.MD5HashClass.Generate("123"), ConfirmPassword = Common.MD5HashClass.Generate("123"), UserGuid = Guid.NewGuid(), Email = "paulomarcelodal@icloud.com" };
                    db.Users.Add(user);

                    // cria tabela de gêneros e persiste dados padrões
                    var genre = new Genre {Name="Drama", CreationDate =  DateTime.Now, Active = true };
                    db.Genres.Add(genre);

                    // cria tabela de filmes e persiste dados padrões
                    var movie = new Movie { Name = "Brilho Eterno de Uma Mente Sem Lembranças", Active = true, CreationDate = DateTime.Now, GenreId = genre.Id, Del = false };
                    db.Movies.Add(movie);

                    //cria tabela de locações e persiste dados padrões
                    var location = new Location { CPF = "000.000.000-00", LocationDate = DateTime.Now };
                    db.Locations.Add(location);

                    //cria tabela de relacionamentos de locações
                    var locateRegister = new LocateRegister { IdLocation = 1, IdMovie = 1 };
                    db.LocateRegisters.Add(locateRegister);

                    db.SaveChanges();
                }
            }
            catch (DbEntityValidationException err)
            {
                //escreve log
                Common.Log.Write("StartDB CreateTables", err.Message);
            }

        }
    }
}

