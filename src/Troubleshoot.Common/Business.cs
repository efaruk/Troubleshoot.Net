using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troubleshoot.Common
{
    public class Business
    {
        private readonly string _dbConnectionString;

        public Business()
        {
            _dbConnectionString = ConfigurationManager.ConnectionStrings["SiteDbConnectionString"].ConnectionString;
        }

        public DataSet GetProductsWithCategories()
        {
            var ds = new DataSet();
            var dt = GetCategories();
            if (dt.Rows.Count == 0) return ds;
            var categories = "0";
            foreach (var row in dt.Rows)
            {
                var dr = (DataRow) row;
                categories += ", " + dr["id"];
            }
            var sql = string.Format("select * from product where id in ({0})", categories);
            using (var conn = GetConnection())
            {
                var da = new SQLiteDataAdapter(sql, conn);
                da.Fill(ds);
            }
            return ds;
        }

        public DataSet GetProductsByCategories()
        {
            var ds = new DataSet();
            var dt = GetCategories();
            if (dt.Rows.Count == 0) return ds;
            foreach (var row in dt.Rows)
            {
                var dr = (DataRow)row;
                var sql = string.Format("select * from product where category={0}", dr["id"]);
                using (var conn = GetConnection())
                {
                    var da = new SQLiteDataAdapter(sql, conn);
                    da.Fill(ds);
                }
            }
            return ds;
        }

        public DataTable GetCategories()
        {
            var dt = new DataTable();
            using (var conn = GetConnection())
            {
                var da = new SQLiteDataAdapter("select * from category", conn);
                da.Fill(dt);
            }
            return dt;
        }

        public SQLiteConnection GetConnection()
        {
            var conn = new SQLiteConnection(_dbConnectionString);
            return conn;
        }
    }
}
