using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace DAO.Models
{
    static public class ConnectionString
    {
        static public DbConnection BaseApiGlobal(string NomeBd)
        {
			try
			{
                // string de conexão
				return new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=" + NomeBd + ";User Id=sa;Password=at4937*@;MultipleActiveResultSets=True");
			}
			catch (Exception err)
			{
                //escreve log
                Common.Log.Write("ConnectionString BaseApiGlobal", err.Message);
                return new SqlConnection("");
            }
        }
    }
}
