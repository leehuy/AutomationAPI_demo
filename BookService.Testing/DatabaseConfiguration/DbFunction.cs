using System;
using System.Data.SqlClient;
using BookService.Testing.Core;

namespace BookService.Testing.DatabaseConfiguration
{
    public class DbFunction
    {
        public static void CreateAllTable()
        {
            string connectionString = IntegrationTestBase.DatabaseManager.ConnectionString;
            SqlConnection con = new
                SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand("CREATE TABLE [dbo].[Authors]([Id][int] IDENTITY(1, 1) NOT NULL,[Name][nvarchar](max) NOT NULL,CONSTRAINT[PK_dbo.Authors] PRIMARY KEY CLUSTERED( [Id] ASC )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY] ) ON[PRIMARY] TEXTIMAGE_ON[PRIMARY]", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("CREATE TABLE [dbo].[Books]([Id][int] IDENTITY(1, 1) NOT NULL, [Title][nvarchar](max) NOT NULL,[Year][int] NOT NULL,[Price][decimal](18, 2) NOT NULL,[Genre][nvarchar](max) NULL,[AuthorId][int] NOT NULL, CONSTRAINT[PK_dbo.Books] PRIMARY KEY CLUSTERED([Id] ASC)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON[PRIMARY] TEXTIMAGE_ON[PRIMARY]", con);
            cmd.ExecuteNonQuery();


            con.Close();
        }
            public static void DeleteAllData(string tableName)
        {
            string connectionString = IntegrationTestBase.DatabaseManager.ConnectionString;
            SqlConnection con = new
                SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand("DELETE FROM " + tableName, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void DeleteAllData(string tableSchema, string tableName)
        {
            string connectionString = IntegrationTestBase.DatabaseManager.ConnectionString;
            SqlConnection con = new
                SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            var sql = "SELECT COUNT(*) FROM information_schema.tables WHERE TABLE_SCHEMA ='" + tableSchema + "' AND table_name = '" + tableName + "'";
            cmd = new SqlCommand(sql, con);
            var count = Convert.ToInt32(cmd.ExecuteScalar());
            if (count > 0)
            {
                cmd = new SqlCommand("DELETE FROM " + tableSchema + "." + tableName, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
