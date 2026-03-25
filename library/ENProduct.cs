using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    /// <summary>
    /// Entity class representing a product in the catalogue.
    /// Follows the Entity / Data-Access (EN/CAD) pattern: this class holds the
    /// product state and delegates every database operation to <see cref="CADProduct"/>.
    /// </summary>
    public class ENProduct
    {
        // Backing fields for the properties of the product.
        private string code;
        private string name;
        private int amount;
        private float price;
        private int category;
        private DateTime creationDate;

        /// <summary>
        /// Gets or sets the unique alphanumeric code that identifies the product
        /// (1–16 characters, used as the primary key for database lookups).
        /// </summary>
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        /// <summary>Gets or sets the product's display name (up to 32 characters).</summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>Gets or sets the stock quantity (0–9 999).</summary>
        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        /// <summary>Gets or sets the unit price (0–9 999.99).</summary>
        public float Price
        {
            get { return price; }
            set { price = value; }
        }

        /// <summary>
        /// Gets or sets the foreign-key identifier of the category this product belongs to.
        /// Corresponds to the <c>id</c> column in the <c>Categories</c> table.
        /// </summary>
        public int Category
        {
            get { return category; }
            set { category = value; }
        }

        /// <summary>Gets or sets the date and time the product record was created.</summary>
        public DateTime CreationDate
        {
            get { return creationDate; }
            set { creationDate = value; }
        }

        /// <summary>
        /// Default constructor. Initialises all fields to safe empty/zero values
        /// and sets <see cref="CreationDate"/> to the current moment.
        /// Useful when only the <see cref="Code"/> will be set before a read or delete.
        /// </summary>
        public ENProduct()
        {
            code = "";
            name = "";
            amount = 0;
            price = 0.0f;
            category = 0;
            creationDate = DateTime.Now;
        }

        /// <summary>
        /// Full constructor. Initialises all fields with the supplied values,
        /// ready for a Create or Update operation.
        /// </summary>
        /// <param name="code">Unique product code (1–16 characters).</param>
        /// <param name="name">Product display name (up to 32 characters).</param>
        /// <param name="amount">Stock quantity (0–9 999).</param>
        /// <param name="price">Unit price (0–9 999.99).</param>
        /// <param name="category">Foreign key of the associated category.</param>
        /// <param name="creationDate">Timestamp of product creation.</param>
        public ENProduct(string code, string name, int amount, float price, int category, DateTime creationDate)
        {
            this.code = code;
            this.name = name;
            this.amount = amount;
            this.price = price;
            this.category = category;
            this.creationDate = creationDate;
        }

        /// <summary>
        /// Inserts this product into the database.
        /// </summary>
        /// <returns><c>true</c> if the row was inserted successfully; <c>false</c> otherwise.</returns>
        public bool Create()
        {
            CADProduct cad = new CADProduct();
            return cad.Create(this);
        }

        /// <summary>
        /// Updates the existing database record matching <see cref="Code"/> with the
        /// current property values (name, amount, price, category).
        /// Note: <see cref="CreationDate"/> is intentionally excluded from updates.
        /// </summary>
        /// <returns><c>true</c> if at least one row was affected; <c>false</c> otherwise.</returns>
        public bool Update()
        {
            CADProduct cad = new CADProduct();
            return cad.Update(this);
        }

        /// <summary>
        /// Deletes the product record identified by <see cref="Code"/> from the database.
        /// </summary>
        /// <returns><c>true</c> if the row was removed; <c>false</c> otherwise.</returns>
        public bool Delete()
        {
            CADProduct cad = new CADProduct();
            return cad.Delete(this);
        }

        /// <summary>
        /// Reads the product record identified by <see cref="Code"/> and populates all
        /// other properties with the values returned from the database.
        /// </summary>
        /// <returns><c>true</c> if the product was found; <c>false</c> otherwise.</returns>
        public bool Read()
        {
            CADProduct cad = new CADProduct();
            return cad.Read(this);
        }

        /// <summary>
        /// Loads the first product in alphabetical code order and populates all properties,
        /// including <see cref="Code"/>.
        /// </summary>
        /// <returns><c>true</c> if any product exists; <c>false</c> if the table is empty.</returns>
        public bool ReadFirst()
        {
            CADProduct cad = new CADProduct();
            return cad.ReadFirst(this);
        }

        /// <summary>
        /// Loads the next product in alphabetical code order after the current <see cref="Code"/>
        /// and populates all properties, including <see cref="Code"/>.
        /// </summary>
        /// <returns><c>true</c> if a next product was found; <c>false</c> if already at the last record.</returns>
        public bool ReadNext()
        {
            CADProduct cad = new CADProduct();
            return cad.ReadNext(this);
        }

        /// <summary>
        /// Loads the previous product in alphabetical code order before the current <see cref="Code"/>
        /// and populates all properties, including <see cref="Code"/>.
        /// </summary>
        /// <returns><c>true</c> if a previous product was found; <c>false</c> if already at the first record.</returns>
        public bool ReadPrev()
        {
            CADProduct cad = new CADProduct();
            return cad.ReadPrev(this);
        }
    }
}
