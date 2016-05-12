using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Troubleshoot.Common.Entity;
using DataSet = System.Data.DataSet;

namespace Troubleshoot.Common
{
    public class Business
    {
        private readonly string _dbConnectionString;

        public Business()
        {
            _dbConnectionString = ConfigurationManager.ConnectionStrings["SiteDbConnectionString"].ConnectionString;
        }

        public SQLiteConnection GetConnection()
        {
            var conn = new SQLiteConnection(_dbConnectionString);
            return conn;
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

        public int InsertCategory(Category category)
        {

            int i;
            using (var conn = GetConnection())
            {
                var cmd = new SQLiteCommand(string.Format("insert into category (name) values ('{0}')", category.Name.EscapeSingleQuato()), conn);
                conn.Open();
                cmd.ExecuteScalar();
                i = (int) conn.LastInsertRowId;
                conn.Close();
            }
            return i;
        }

        public int InsertProduct(Product product)
        {
            int i;
            using (var conn = GetConnection())
            {
                var cmd = new SQLiteCommand(string.Format("INSERT INTO Product (category, code, name, description) VALUES ({0}, '{1}', '{2}', '{3}');", product.Category, product.Code, product.Name.EscapeSingleQuato(), product.Description.EscapeSingleQuato()), conn);
                conn.Open();
                cmd.ExecuteScalar();
                i = (int) conn.LastInsertRowId;
                conn.Close();
            }
            return i;
        }

        public void GenerateCategory(int count)
        {
            var fake = new Faker<Category>()
                .RuleFor(c => c.Name, faker => faker.Name.JobArea());
            fake.Locale = "tr";
            var categories = fake.Generate(count);
            foreach (var c in categories)
            {
                InsertCategory(c);
            }
        }

        public void GenerateProduct(int count)
        {
            List<int> categories = new List<int>(103);
            for (int i = 1; i < 104; i++)
            {
                categories.Add(i);
            }

            var fake = new Faker<Product>()
                .RuleFor(p => p.Category, faker => faker.PickRandom(categories))
                .RuleFor(p => p.Code, faker => faker.Address.ZipCode("####"))
                .RuleFor(p => p.Name, faker => faker.Name.FirstName())
                .RuleFor(p => p.Description, faker => faker.Lorem.Paragraph(3));
            fake.Locale = "tr";
            var products = fake.Generate(count);
            foreach (var p in products)
            {
                InsertProduct(p);
            }
        }
    }
}
