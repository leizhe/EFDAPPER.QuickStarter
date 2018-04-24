using System;
using System.Data;
using System.Data.SqlClient;
using ED.Common;


namespace ED.Repositories.Dapper
{
    public class DapperContext 
    {
        
        private readonly string _connstr = Global.QueryDB;
        public DapperContext()
        {
        }

        public DapperContext(string connstr)
        {

            _connstr = connstr;
        }

        public IDbConnection GetConnection()
        {
            var conn = new SqlConnection(_connstr);
            conn.Open();
            return conn;
        }


        public IDbConnection GetConnection(string strConn)
        {
            var conn = new SqlConnection(strConn);
            conn.Open();
            return conn;
        }


        public Tuple<bool, string> TestConn(string connstring)
        {
            bool isopen = false;
            string msg = string.Empty;

            try
            {
                var conn = GetConnection(connstring);
                if (conn.State == ConnectionState.Open)
                {
                    isopen = true;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }


            return new Tuple<bool, string>(isopen, msg);
        }
        public Tuple<bool, string> TestConn()
        {
            bool isopen = false;
            string msg = string.Empty;

            try
            {
                var conn = GetConnection(_connstr);
                if (conn.State == ConnectionState.Open)
                {
                    isopen = true;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }


            return new Tuple<bool, string>(isopen, msg);
        }


    }
}