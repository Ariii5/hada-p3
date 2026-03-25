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
    /// Data-Access class for the <c>Categories</c> database table.
    /// Follows the Entity / Data-Access (EN/CAD) pattern: all SQL logic lives here,
    /// keeping the entity class (<see cref="ENCategory"/>) free of persistence concerns.
    /// The connection string is read from the <c>HadaBD</c> entry in Web.config.
    /// </summary>
    public class CADCategory
    {
        /// <summary>ADO.NET connection string retrieved from the application configuration.</summary>
        private string constring;   //cadena a la base de datos

        /// <summary>
        /// Initialises the data-access object by reading the connection string
        /// named <c>HadaBD</c> from the application's configuration file.
        /// </summary>
        public CADCategory()
        {
            this.constring = ConfigurationManager.ConnectionStrings["HadaBD"].ToString(); ;
        }

        /// <summary>
        /// Reads the name of a single category identified by <paramref name="en"/>'s <c>Id</c>
        /// and stores it in <paramref name="en"/>'s <c>Name</c> property.
        /// </summary>
        /// <param name="en">
        /// An <see cref="ENCategory"/> whose <c>Id</c> is used as the lookup key.
        /// On success, its <c>Name</c> property is updated with the value from the database.
        /// </param>
        /// <returns>
        /// <c>true</c> if a matching row was found and <c>Name</c> was populated;
        /// <c>false</c> if no row matched or a SQL error occurred.
        /// </returns>
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

        /// <summary>
        /// Retrieves every row from the <c>Categories</c> table and returns them as a list.
        /// Used on page load to populate the category drop-down in the UI.
        /// </summary>
        /// <returns>
        /// A <see cref="List{T}"/> of <see cref="ENCategory"/> objects (one per database row).
        /// Returns an empty list if the table is empty or a SQL error occurs.
        /// </returns>
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
