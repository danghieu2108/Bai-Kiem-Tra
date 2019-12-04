using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AppG4
{
    class DBUtils
    {
        public static SqlConnection GetDBConnection()
        {
            string datasource = @"TUSNGUYEN\SQLSERVER";

            string database = "QL_Nhanvien";
            string username = "sa";
            string password = "1";

            return DBSQLServerUtils.GetDBConnection(datasource, database, username, password);
        }
    }
}
