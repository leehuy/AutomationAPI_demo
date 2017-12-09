using System;
using BookService.Testing.Core;
using System.Data.SqlClient;
using System.Data;

namespace BookService.Testing.DatabaseConfiguration
{
    public class DbBook
    {
        public static void InsertDataInBookTable(string id, string title, string year, string price, string genre, string authorId)
        {
            string connectionString = IntegrationTestBase.DatabaseManager.ConnectionString;
            SqlConnection con = new
                SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand("SET IDENTITY_INSERT [dbo].[Books] ON", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("INSERT into Books(Id, Title, Year, Price, Genre, AuthorId) VALUES (@id,@title,@year,@price,@genre,@authorId)", con);
            SqlParameter[] parms = new SqlParameter[6];
            parms[0] = new SqlParameter("@Id", SqlDbType.Int);
            parms[0].Value = Convert.ToInt32(id);

            parms[1] = new SqlParameter("@Title", SqlDbType.VarChar);
            parms[1].Value = title;

            parms[2] = new SqlParameter("@Year", SqlDbType.Int);
            parms[2].Value = Convert.ToInt32(year);

            parms[3] = new SqlParameter("@Price", SqlDbType.Decimal);
            parms[3].Value = Convert.ToDecimal(price);

            parms[4] = new SqlParameter("@Genre", SqlDbType.VarChar);
            parms[4].Value = genre;

            parms[5] = new SqlParameter("@AuthorId", SqlDbType.Int);
            parms[5].Value = Convert.ToInt32(authorId);

            cmd.Parameters.AddRange(parms);
            cmd.ExecuteNonQuery();
            con.Close();

        }
    }
}
