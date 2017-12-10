using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.SqlServer.Management.Common;

namespace BookService.Testing.Core
{
    public class IntegrationTestLocalDbManager : IDisposable
    {
        static IntegrationTestLocalDbManager()
        {
            DatabaseDirectory = "Data";
        }

        public IntegrationTestLocalDbManager(string databaseName = null)
        {
            DatabaseName = string.IsNullOrWhiteSpace(databaseName)
                ? Guid.NewGuid().ToString("N").ToLower()
                : databaseName;

            try
            {
                CreateDatabase();
                RestoreDatabase();
                Trace.WriteLine(
                    string.Format("DatabaseManager created the local database {0}", Path.Combine(DatabaseDirectory, DatabaseName)));
            }
            catch (SqlException e)
            {
                Trace.WriteLine(string.Format("DatabaseManager could not create database {0}.\n\r {1} ", DatabaseName, e.Message));
                throw;
            }
        }

        public static string DatabaseDirectory { get; set; }

        public string ConnectionString { get; private set; }

        public string DatabaseName { get; private set; }

        public string OutputFolder { get; private set; }

        public string DatabaseMdfPath { get; private set; }

        public string DatabaseLogPath { get; private set; }

        public IDbConnection OpenConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public void Dispose()
        {
            DetachDatabase();
        }

        public void ExecuteString(string script)
        {
            var sqlConnection = (SqlConnection)OpenConnection();

            var server = new Microsoft.SqlServer.Management.Smo.Server(new ServerConnection(sqlConnection));
            server.ConnectionContext.ExecuteNonQuery(script);
            sqlConnection.Close();
        }

        private void RestoreDatabase()
        {
            //var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            //var assemblyDirectory = Path.GetDirectoryName(assemblyLocation);

            //if (!System.IO.File.Exists(OutputFolder + "\\Books.bak"))
            //{
            //    //string Projectpath = Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, @"..\..\..\Data\"));
            //    System.IO.File.Copy(assemblyDirectory + "\\Books.bak", OutputFolder + "\\Books.bak");
            //}

            SqlConnection con = new
              SqlConnection(@"Data Source=(LocalDb)\mssqllocaldb;Initial Catalog=master;Integrated Security=True");

            con.Open();
            SqlCommand cmd;
            cmd = new SqlCommand("RESTORE DATABASE "+ DatabaseName + " FROM DISK = '" + OutputFolder + "\\Books.bak' WITH FILE = 1, NOUNLOAD, STATS = 10, RECOVERY, REPLACE, MOVE 'BookServiceContext-20171126185836.mdf' TO '"+ DatabaseMdfPath + "', MOVE 'BookServiceContext-20171126185836_log.ldf' TO '"+ DatabaseLogPath + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void CreateDatabase()
        {
            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            var assemblyDirectory = Path.GetDirectoryName(assemblyLocation);

            if (assemblyDirectory == null)
            {
                throw new DirectoryNotFoundException();
            }

            OutputFolder = Path.Combine(assemblyDirectory, DatabaseDirectory);
            var mdfFilename = string.Format("{0}.mdf", DatabaseName);
            DatabaseMdfPath = Path.Combine(OutputFolder, mdfFilename);
            DatabaseLogPath = Path.Combine(OutputFolder, string.Format("{0}_log.ldf", DatabaseName));

            if (!System.IO.Directory.Exists(OutputFolder))
            {
                System.IO.Directory.CreateDirectory(OutputFolder);
            }

            var connectionString = @"Data Source=(LocalDb)\mssqllocaldb;Initial Catalog=master;Integrated Security=True";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    var sb = new StringBuilder(1000);
                    sb.AppendLine(string.Format("EXECUTE (N'CREATE DATABASE {0}", DatabaseName));
                    sb.AppendLine(string.Format("ON PRIMARY (NAME = N''{0}'', FILENAME = ''{1}'')", DatabaseName, DatabaseMdfPath));
                    sb.AppendLine(string.Format("LOG ON (NAME = N''{0}_log'',  FILENAME = ''{1}'')')", DatabaseName, DatabaseLogPath));

                    cmd.CommandText = sb.ToString();
                    cmd.ExecuteNonQuery();

                    cmd.CommandText =
                        "EXEC sp_MSForEachTable 'DISABLE TRIGGER ALL ON ?'  EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL' EXEC sp_MSForEachTable 'DELETE FROM ?'  EXEC sp_MSForEachTable 'ALTER TABLE ? CHECK CONSTRAINT ALL'  EXEC sp_MSForEachTable 'ENABLE TRIGGER ALL ON ?'";
                    cmd.ExecuteNonQuery(); 
                }

            }

            ConnectionString =
                string.Format(@"Data Source=(LocalDb)\mssqllocaldb;AttachDBFileName={1};Initial Catalog={0};Integrated Security=True;", DatabaseName, DatabaseMdfPath);
        }

        private void DetachDatabase()
        {
            try
            {
                var connectionString =
                    @"Data Source=(LocalDb)\mssqllocaldb;Initial Catalog=master;Integrated Security=True";
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var cmd = connection.CreateCommand())
                    {
                        try
                        {
                            cmd.CommandText =
                                string.Format(
                                    "ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE; exec sp_detach_db '{0}'",
                                    DatabaseName);
                            cmd.ExecuteNonQuery();
                        }
                        catch (SqlException e)
                        {
                            Trace.TraceError(e.Message);

                            cmd.CommandText = string.Format("DROP DATABASE {0}; exec sp_detach_db '{0}'", DatabaseName);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
            finally
            {
                if (File.Exists(DatabaseMdfPath))
                {
                    File.Delete(DatabaseMdfPath);
                }

                if (File.Exists(DatabaseLogPath))
                {
                    File.Delete(DatabaseLogPath);
                }
            }
        }
    }
}
