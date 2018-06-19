using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SQLTestCS
{
    class Program
    {
        static void Main(string[] args)
        {
            string constr = @"Data Source=localhost\SQLEXPRESS;Database=ydb1;Integrated Security=True";

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            try
            {
                Console.WriteLine("connect");

                string sqlstr = "select * from sysdatabases";
                SqlCommand com = new SqlCommand(sqlstr, con);
                SqlDataReader sdr = com.ExecuteReader();

                Console.WriteLine("databaselist:");

                while (sdr.Read() == true)
                {
                    string model = (string)sdr[0];
                    Console.WriteLine(string.Format("{0}", model));
                }
            }
            finally
            {
                con.Close();
            }
        }
    }
}
