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
    /// <summary>
    /// Data-Access class for the <c>Products</c> database table.
    /// Follows the Entity / Data-Access (EN/CAD) pattern: all SQL logic lives here,
    /// keeping the entity class (<see cref="ENProduct"/>) free of persistence concerns.
    /// The connection string is read from the <c>HadaBD</c> entry in Web.config.
    /// Every public method accepts an <see cref="ENProduct"/> and reads from / writes to
    /// its properties directly, acting as a two-way data transfer object.
    /// </summary>
    public class CADProduct
    {
        /// <summary>ADO.NET connection string retrieved from the application configuration.</summary>
        private string constring;   //cadena a la base de datos

        /// <summary>
        /// Initialises the data-access object by reading the connection string
        /// named <c>HadaBD</c> from the application's configuration file.
        /// </summary>
        public CADProduct()
        {
            this.constring = ConfigurationManager.ConnectionStrings["HadaBD"].ToString(); ;
        }

        /// <summary>
        /// Inserts a new product row into the <c>Products</c> table using the values
        /// contained in <paramref name="en"/>.
        /// </summary>
        /// <param name="en">Entity whose properties supply all INSERT column values.</param>
        /// <returns>
        /// <c>true</c> if exactly one row was inserted; <c>false</c> on error or if
        /// no rows were affected (e.g. a constraint violation caught as a SQL exception).
        /// </returns>
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
                Console.WriteLine("Ha habido un error respecto a la creación de producto. Error: {0}", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Updates the <c>Products</c> row identified by <paramref name="en"/>'s <c>Code</c>.
        /// Only <c>name</c>, <c>price</c>, <c>category</c>, and <c>amount</c> are overwritten;
        /// <c>code</c> and <c>creationDate</c> remain unchanged by design.
        /// </summary>
        /// <param name="en">Entity whose <c>Code</c> identifies the target row and whose
        /// other properties supply the new values.</param>
        /// <returns>
        /// <c>true</c> if at least one row was updated; <c>false</c> if the code was not found
        /// or a SQL error occurred.
        /// </returns>
        public bool Update(ENProduct en)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(constring))
                {
                    string comando = "UPDATE Products set name=@name, price=@price, category=@category, amount=@amount, creationDate=@creationDate where code=@code";
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
                Console.WriteLine("Ha habido un error respecto a la actualización de producto. Error: {0}", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Deletes the <c>Products</c> row identified by <paramref name="en"/>'s <c>Code</c>.
        /// </summary>
        /// <param name="en">Entity whose <c>Code</c> identifies the row to remove.</param>
        /// <returns>
        /// <c>true</c> if the row was deleted; <c>false</c> if not found or a SQL error occurred.
        /// </returns>
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
                Console.WriteLine("Ha habido un error respecto a la eliminación del producto. Error: {0}", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Reads the product row matching <paramref name="en"/>'s <c>Code</c> and populates
        /// all other properties of <paramref name="en"/> from the database.
        /// </summary>
        /// <param name="en">
        /// Entity whose <c>Code</c> is the lookup key. On success, <c>Name</c>, <c>Amount</c>,
        /// <c>Price</c>, <c>Category</c>, and <c>CreationDate</c> are updated.
        /// </param>
        /// <returns><c>true</c> if the product was found; <c>false</c> otherwise.</returns>
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
                Console.WriteLine("Ha habido un error para leer la información del producto. Error: {0}", ex.Message);
                return false;
            }
        }


        /// <summary>
        /// Reads the first product in ascending alphabetical code order (TOP 1 ORDER BY code)
        /// and populates all properties of <paramref name="en"/>, including <c>Code</c>.
        /// Used to navigate to the start of the product list.
        /// </summary>
        /// <param name="en">Entity object to be populated with the first product's data.</param>
        /// <returns><c>true</c> if at least one product exists; <c>false</c> if the table is empty.</returns>
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
                Console.WriteLine("Ha habido un error para leer la información del primer producto. Error: {0}", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Reads the product immediately after <paramref name="en"/>'s <c>Code</c>
        /// in ascending alphabetical order and populates all properties of <paramref name="en"/>.
        /// Used to implement forward navigation through the product list.
        /// </summary>
        /// <param name="en">
        /// Entity whose <c>Code</c> is the reference point. On success, all properties
        /// (including <c>Code</c>) are overwritten with the next product's data.
        /// </param>
        /// <returns>
        /// <c>true</c> if a next product was found; <c>false</c> if the current product is the last one.
        /// </returns>
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
                Console.WriteLine("Ha habido un error para leer la información del siguiente producto al escogido. Error: {0}", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Reads the product immediately before <paramref name="en"/>'s <c>Code</c>
        /// in ascending alphabetical order and populates all properties of <paramref name="en"/>.
        /// Used to implement backward navigation through the product list.
        /// </summary>
        /// <param name="en">
        /// Entity whose <c>Code</c> is the reference point. On success, all properties
        /// (including <c>Code</c>) are overwritten with the previous product's data.
        /// </param>
        /// <returns>
        /// <c>true</c> if a previous product was found; <c>false</c> if the current product is the first one.
        /// </returns>
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
                Console.WriteLine("Ha habido un error para leer la información del producto previo al escogido. Error: {0}", ex.Message);
                return false;
            }
        }
    }
}
