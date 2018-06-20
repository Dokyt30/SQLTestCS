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

    static public class Define
    {
        public static readonly string DatabaseName = "ydb1";
        public static readonly string DataBaseTableName= "dbo.ytbl1";
    }
    static public class Utility
    {
        static public SqlConnection OpenCreate(string constr)
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            clog("connect");
            return con;
        }
        static public SqlDataReader CommandExeRead(string sqlstr, SqlConnection con)
        {
            SqlCommand com = new SqlCommand(sqlstr, con);
            SqlDataReader sdr = com.ExecuteReader();
            clog("command exec:" + sqlstr);
            return sdr;
        }
        static public bool ReadNext(SqlDataReader sdr)
        {
            return sdr.Read();
        }
        static void clog(string msg)
        {
            //Console.WriteLine("Utility:" + msg);
        }
    }

    class Program
    {



        static string select_dblist(string constr)
        {
            SqlConnection con = Utility.OpenCreate(constr);
            string dbname = "";
            try
            {
                SqlDataReader sdr = Utility.CommandExeRead("SELECT * FROM sysobjects WHERE xtype = 'u'", con);
                Console.WriteLine("databaselist:");
                while (Utility.ReadNext(sdr))
                {
                    dbname = (string)sdr[0];
                    Console.WriteLine(string.Format("{0}", dbname));
                }
            }
            finally
            {
                con.Close();
            }
            return dbname;
        }

        static int select(string constr)
        {
            int maxuid = 1;
            SqlConnection con = Utility.OpenCreate(constr);
            try
            {
                SqlDataReader sdr = Utility.CommandExeRead(string.Format("select * from {0}.{1}", Define.DatabaseName, Define.DataBaseTableName), con);
                Console.WriteLine("select "+ Define.DatabaseName+"."+ Define.DataBaseTableName +":");
                while (Utility.ReadNext(sdr))
                {
                    int uid = (int)sdr["uid"];
                    string name = (string)sdr["name"];
                    Console.WriteLine(string.Format("uid:{0} name:{1}", uid.ToString(), name));
                    maxuid = maxuid < uid ? uid : maxuid;
                }
            }
            finally
            {
                con.Close();
            }
            return maxuid;
        }

        static void insert(string constr, int uid, string name)
        {
            SqlConnection con = Utility.OpenCreate(constr);
            try
            {
                SqlDataReader sdr = Utility.CommandExeRead(string.Format("insert {0}.{1} (uid, name) VALUES ({2}, '{3}')", Define.DatabaseName, Define.DataBaseTableName, uid, name), con);
                Console.WriteLine("insert");
            }
            finally
            {
                con.Close();
            }
        }

        static void Main(string[] args)
        {
            string constr = string.Format(@"Data Source=localhost\SQLEXPRESS;Database={0};Integrated Security=True", Define.DatabaseName);
            string dbname = select_dblist(constr);
            int maxuid = 1;
            maxuid = select(constr);
            maxuid++;
            //insert(constr, maxuid, "nm_" + maxuid.ToString());
            maxuid = select(constr);

            // count
            {
                SqlConnection con = Utility.OpenCreate(constr);
                try
                {
                    SqlDataReader sdr = Utility.CommandExeRead(string.Format("SELECT COUNT(*) from {0}.{1}", Define.DatabaseName, Define.DataBaseTableName), con);
                    Console.WriteLine("select " + Define.DatabaseName + "." + Define.DataBaseTableName + ":");
                    while (Utility.ReadNext(sdr))
                    {
                        int cnt = (int)sdr[0];
                        Console.WriteLine(string.Format("{0}", cnt));
                    }
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}
