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
            string dbname = "";

            {
                SqlConnection con = new SqlConnection(constr);
                con.Open();

                try
                {
                    Console.WriteLine("connect");

                    {
                        string sqlstr = "  SELECT * FROM sysobjects WHERE xtype = 'u'";
                        SqlCommand com = new SqlCommand(sqlstr, con);
                        SqlDataReader sdr = com.ExecuteReader();

                        Console.WriteLine("databaselist:");

                        while (sdr.Read() == true)
                        {
                            dbname = (string)sdr[0];
                            Console.WriteLine(string.Format("{0}", dbname));


                        }
                    }

                }
                finally
                {
                    con.Close();
                }

            }

            {
                SqlConnection con = new SqlConnection(constr);
                con.Open();

                try
                {
                    Console.WriteLine("connect");

                    {
                        string sqlstr = "select * from ydb1.dbo.ytbl1";
                        SqlCommand com = new SqlCommand(sqlstr, con);
                        SqlDataReader sdr = com.ExecuteReader();

                        Console.WriteLine("databaselist:");

                        while (sdr.Read() == true)
                        {
                            int uid = (int)sdr["uid"];
                            string name = (string)sdr["name"];
                            Console.WriteLine(string.Format("uid:{0} name:{1}", uid.ToString(), name));
                        }
                    }

                }
                finally
                {
                    con.Close();
                }

            }
            //{
            //    SqlConnection con = new SqlConnection(constr);
            //    con.Open();

            //    try
            //    {
            //        Console.WriteLine("connect");

            //        {
            //            SqlCommand com2 = new SqlCommand("SELECT COUNT(*) FROM dbo." + dbname, con);
            //            SqlDataReader sdr2 = com2.ExecuteReader();
            //            while (sdr2.Read() == true)
            //            {
            //                //decimal cnt = (decimal)sdr2[0];
            //                //Console.WriteLine(string.Format("{0}", cnt));
            //            }
            //        }

            //    }
            //    finally
            //    {
            //        con.Close();
            //    }

            //}
        }
    }
}
