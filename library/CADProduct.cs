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
    internal class CADProduct
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
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Ha habido un errror respecto a la creación de producto. Error: {0}", ex.Message);
                return false;
            }
        }
    }
}
