using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportDL
{
    public class BaseData
    {

        public static SqlCommand GetCommand()
        {
            SqlConnection conn = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                var appSettingReader=new System.Configuration.AppSettingsReader();
                conn=new SqlConnection ();
                conn.ConnectionString=(string) appSettingReader.GetValue("ConnectionString",typeof(String));
                if (conn.State==System.Data.ConnectionState.Closed)
	               conn.Open();
                SqlTransaction sqlTransaction=conn.BeginTransaction();
                cmd.Transaction=sqlTransaction;
                cmd.Connection=conn;
                cmd.Parameters.Clear();
                cmd.CommandType=CommandType.StoredProcedure;
                return cmd;

            }
            catch (Exception)
            {

                throw new Exception("Connection string not valid");
            }
        }

    }
}
