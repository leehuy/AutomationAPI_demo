using System;
using BookService.Testing.Core;
using System.Data.SqlClient;
using System.Data;

namespace BookService.Testing.DatabaseConfiguration
{
    public class DbAuthors
    {
        public static void InsertDataInAutherTable(string id, string name)
        {
            string connectionString = IntegrationTestBase.DatabaseManager.ConnectionString;
            SqlConnection con = new
                SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand("SET IDENTITY_INSERT [dbo].[Authors] ON", con);
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("INSERT into Authors(Id, Name) VALUES (@id,@name)", con);
            SqlParameter[] parms = new SqlParameter[2];
            parms[0] = new SqlParameter("@Id", SqlDbType.Int);
            parms[0].Value = Convert.ToInt32(id);

            parms[1] = new SqlParameter("@Name", SqlDbType.VarChar);
            parms[1].Value = name;

            cmd.Parameters.AddRange(parms);
            cmd.ExecuteNonQuery();
            con.Close();

        }
    }
}
