using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_House_Manager
{
   public class Class1
    {
        string _connectionstring;
        public Class1()
        {
            _connectionstring = @"Data Source=DESKTOP-QKTUOJ6\SQLEXPRESS;Initial Catalog=test;Integrated Security=True";
        }

        public DataTable  getClublist()
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionstring);
            SqlDataAdapter adapter = new SqlDataAdapter("select * from club", sqlConnection);
            DataTable dataSet = new DataTable();
            adapter.Fill(dataSet);
            return dataSet;

        }

    }
}
