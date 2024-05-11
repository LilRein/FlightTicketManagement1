using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data;

namespace DAL
{
    public class SqlConnectionData
    {
        public static SqlConnection Connect()
        {
            string strcon = @"Data Source=LAPTOP-I5KB10L7;Initial Catalog=QUANLYBANVECHUYENBAY;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
            SqlConnection conn = new SqlConnection(strcon);
            return conn;
        }
    }

    public class DatabaseAccess
    {
        public static string CheckSchedules(CHUYENBAY schedule)
        {
            string machuyenbay = null;
            SqlConnection conn = SqlConnectionData.Connect();
            conn.Open();
            SqlCommand cmd = new SqlCommand("proc_schedule", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@machuyenbay", schedule.MaChuyenBay);

            return machuyenbay;
        }
    }
}
