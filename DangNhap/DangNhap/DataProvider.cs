using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DangNhap
{
    class DataProvider
    {
        public static string ChuoiKetNoi = @"Data Source = NGOCHOP\SQLEXPRESS\SQLEXPRESS;Initial Catalog =QLTHUVIEN;Integrated Security=true";

        public static SqlConnection KetNoiDB()
        {
            SqlConnection conn = new SqlConnection(ChuoiKetNoi);
            conn.Open();
            return conn;
        }
    }
}
