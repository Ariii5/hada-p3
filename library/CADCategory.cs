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
    public class CADCategory
    {
        private string constring;   //cadena a la base de datos
        public CADCategory()
        {
            this.constring = ConfigurationManager.ConnectionStrings["HadaBD"].ToString(); ;
        }
        public bool Read(ENCategory en)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(constring))
                {
                    string comando = "SELECT name from Categories where id=@id";
                    SqlCommand cmd = new SqlCommand(comando, conn);
                    cmd.Parameters.AddWithValue("@id", en.Id);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        en.Name = reader["name"].ToString();
                        return true;
                    }
                }
                return false;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Ha habido un errror respecto a la lectura del nombre de categoría. Error: {0}", ex.Message);
                return false;
            }
        }
        public List<ENCategory> readAll()
        {
            List<ENCategory> categorias = new List<ENCategory>();
            try
            {
                using (SqlConnection conn = new SqlConnection(constring))
                {
                    string comando = "SELECT id, name from Categories";
                    SqlCommand cmd = new SqlCommand(comando, conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ENCategory nueva = new ENCategory((int)reader["id"], reader["name"].ToString());
                        categorias.Add(nueva);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Ha habido un errror respecto a la lectura de todas las categorías. Error: {0}", ex.Message);
            }
            return categorias;
        }
    }
}
