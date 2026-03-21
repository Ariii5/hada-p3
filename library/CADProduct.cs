using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class CADProduct
    {
        private string constring;   //cadena a la base de datos
        public CADProduct()
        {
            this.constring = ConfigurationManager.ConnectionStrings["HadaBD"].ToString(); ;
        }
        public bool Create(ENProduct en)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(constring))
                {
                    string comando = "INSERT INTO Products (name, code, amount, price, category, creationDate) VALUES (@name, @code, @amount, @price, @category, @creationDate)";
                    SqlCommand cmd = new SqlCommand(comando, conn);
                    cmd.Parameters.AddWithValue("@name", en.Name);
                    cmd.Parameters.AddWithValue("@code", en.Code);
                    cmd.Parameters.AddWithValue("@amount", en.Amount);
                    cmd.Parameters.AddWithValue("@price", en.Price);
                    cmd.Parameters.AddWithValue("@category", en.Category);
                    cmd.Parameters.AddWithValue("@creationDate", en.CreationDate);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
               
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Ha habido un errror respecto a la creación de producto. Error: {0}", ex.Message);
                return false;
            }
        }
        public bool Update(ENProduct en)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(constring))
                {
                    string comando = "UPDATE Products set name=@name, price=@price, category=@category, amount=@amount where code=@code";
                    SqlCommand cmd = new SqlCommand(comando, conn);
                    cmd.Parameters.AddWithValue("@name", en.Name);
                    cmd.Parameters.AddWithValue("@code", en.Code);
                    cmd.Parameters.AddWithValue("@amount", en.Amount);
                    cmd.Parameters.AddWithValue("@price", en.Price);
                    cmd.Parameters.AddWithValue("@category", en.Category);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Ha habido un errror respecto a la actualización de producto. Error: {0}", ex.Message);
                return false;
            }
        }
        public bool Delete(ENProduct en)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(constring))
                {
                    string comando = "DELETE from Products where code=@code";
                    SqlCommand cmd = new SqlCommand(comando, conn);
                    cmd.Parameters.AddWithValue("@code", en.Code);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
               
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Ha habido un errror respecto a la eliminación del producto. Error: {0}", ex.Message);
                return false;
            }
        }
        public bool Read(ENProduct en)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(constring))
                {
                    string sql = "SELECT * from Products where code=@code";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@code", en.Code);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        en.Name = dr["name"].ToString();
                        en.Amount = (int)dr["amount"];
                        en.Price = Convert.ToSingle(dr["price"]);
                        en.Category = (int)dr["category"];
                        en.CreationDate = Convert.ToDateTime(dr["creationDate"]);
                        return true;
                    }
                }
                return false;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Ha habido un errror para leer la información del producto. Error: {0}", ex.Message);
                return false;
            }
        }
        public bool ReadFirst(ENProduct en)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(constring))
                {
                    string sql = "SELECT TOP 1 * from Products order by code";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        en.Code = dr["code"].ToString();
                        en.Name = dr["name"].ToString();
                        en.Amount = (int)dr["amount"];
                        en.Price = Convert.ToSingle(dr["price"]);
                        en.Category = (int)dr["category"];
                        en.CreationDate = Convert.ToDateTime(dr["creationDate"]);
                        return true;
                    }
                }
                return false;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Ha habido un errror para leer la información del primer producto. Error: {0}", ex.Message);
                return false;
            }
        }
        public bool ReadNext(ENProduct en)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(constring))
                {
                    string sql = "SELECT TOP 1 * from Products where code > @code order by code";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@code", en.Code);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        en.Code = dr["code"].ToString();
                        en.Name = dr["name"].ToString();
                        en.Amount = (int)dr["amount"];
                        en.Price = Convert.ToSingle(dr["price"]);
                        en.Category = (int)dr["category"];
                        en.CreationDate = Convert.ToDateTime(dr["creationDate"]);
                        return true;
                    }
                }
                return false;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Ha habido un errror para leer la información del siguiente producto al escogido. Error: {0}", ex.Message);
                return false;
            }
        }
        public bool ReadPrev(ENProduct en)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(constring))
                {
                    string sql = "SELECT TOP 1 * from Products where code < @code order by code desc";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@code", en.Code);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        en.Code = dr["code"].ToString();
                        en.Name = dr["name"].ToString();
                        en.Amount = (int)dr["amount"];
                        en.Price = Convert.ToSingle(dr["price"]);
                        en.Category = (int)dr["category"];
                        en.CreationDate = Convert.ToDateTime(dr["creationDate"]);
                        return true;
                    }
                }
                return false;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Ha habido un errror para leer la información del producto previo al escogido. Error: {0}", ex.Message);
                return false;
            }
        }
    }
}
