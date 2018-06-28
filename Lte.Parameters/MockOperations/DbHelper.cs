using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Parameters.MockOperations
{
    public class DbHelper
    {
        private readonly string _connectionString;

        public DbHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int ExeQuery(string commandText)
        {
            var conn = new SqlConnection(_connectionString);
            using (var cd = new SqlCommand(commandText, conn))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                return cd.ExecuteNonQuery();
            }
        }

        public DataTable GetDataTable(string selectText)
        {
            var conn = new SqlConnection(_connectionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            var nDt = new DataTable();
            var cd = new SqlCommand(selectText, conn);
            var da = new SqlDataAdapter(cd);
            da.Fill(nDt);
            return nDt;
        }

        public void ExecuteSqlBulkCopy(DataTable dt, string tableName)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                var bulk = new SqlBulkCopy(conn)
                {
                    DestinationTableName = tableName,
                    BatchSize = dt.Rows.Count
                };

                if (dt.Rows.Count != 0)
                    bulk.WriteToServer(dt);
                bulk.Close();
                conn.Close();
            }
        }
    }
}
