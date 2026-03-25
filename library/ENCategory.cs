using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    /// <summary>
    /// Entity class representing a product category.
    /// Follows the Entity / Data-Access (EN/CAD) pattern: this class owns the
    /// category data and delegates all database operations to <see cref="CADCategory"/>.
    /// </summary>
    public class ENCategory
    {
        // Backing fields for the category identifier and display name.
        private int id;
        private string name;

        /// <summary>Gets or sets the unique numeric identifier of the category (primary key).</summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>Gets or sets the readable name of the category.</summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Initialises a category instance with the given identifier and name.
        /// </summary>
        /// <param name="id">The category's primary key in the database.</param>
        /// <param name="name">The category's display name.</param>
        public ENCategory(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        /// <summary>
        /// Reads the category name from the database using the current <see cref="Id"/>.
        /// Delegates to <see cref="CADCategory.Read"/>.
        /// </summary>
        /// <returns>
        /// <c>true</c> if a matching record was found and <see cref="Name"/> was populated;
        /// <c>false</c> otherwise.
        /// </returns>
        public bool Read()
        {
            CADCategory cad = new CADCategory();
            return cad.Read(this);
        }

        /// <summary>
        /// Retrieves every category stored in the database.
        /// Delegates to <see cref="CADCategory.readAll"/>.
        /// </summary>
        /// <returns>
        /// A list of all <see cref="ENCategory"/> objects; empty if none exist or an error occurs.
        /// </returns>
        public List<ENCategory> readAll()
        {
            CADCategory cad = new CADCategory();
            return cad.readAll();
        }
    }
}
