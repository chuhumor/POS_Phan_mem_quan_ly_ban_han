using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBDS
{
    class DBconnect
    {
        SqlConnection conn = new SqlConnection();
        // SqlDataAdapter da = new SqlDataAdapter();

        SqlCommand cmd = new SqlCommand();
        private string con;
        public string myConnection()
        {
            con = @"Data Source=DESKTOP-MN9JIQR\SQLEXPRESS;Initial Catalog=QLBDS;Integrated Security=True";
            return con;
        }
        public DataTable getTable(string qury)
        {
            conn.ConnectionString = myConnection();
            cmd = new SqlCommand(qury, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
}
