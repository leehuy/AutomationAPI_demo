using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BookService.Testing.Core;


namespace BookService.Testing.Resources
{
    public class GetData
    {
        public static List<Dictionary<string, string>> GetDataFromDatabase(string querry)
        {
            List<Dictionary<string, string>> rows = new List<Dictionary<string, string>>();
            Dictionary<string, string> column;

            string connectionString = IntegrationTestBase.DatabaseManager.ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(querry, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        column = Enumerable.Range(0, dr.FieldCount)
                            .ToDictionary(i => dr.GetName(i), i => dr.GetValue(i).ToString());
                        rows.Add(column);
                    }

                    con.Close();
                    return rows;
                }
            }
        }
    }
}
