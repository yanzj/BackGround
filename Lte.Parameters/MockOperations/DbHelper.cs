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
    }
}
