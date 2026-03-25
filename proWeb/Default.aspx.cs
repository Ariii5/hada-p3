using library;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace proWeb
{
    /// <summary>
    /// Code-behind for the Default.aspx page.
    /// Provides a full CRUD (Create, Read, Update, Delete) interface for the
    /// <c>Products</c> table, as well as sequential navigation (First, Previous, Next).
    /// All business-rule validation is centralised in <see cref="ValidateFields"/>.
    /// Database access is handled by the <see cref="ENProduct"/> / <see cref="CADProduct"/> layer.
    /// </summary>
    public partial class Default : System.Web.UI.Page
    {
        /// <summary>
        /// Fires on every page request. On the initial (non-postback) load, retrieves
        /// all categories from the database and binds them to the <c>ddlCategory</c>
        /// drop-down list so the user can select a category when creating or updating
        /// a product. Skipped on postbacks to avoid redundant database calls.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve all categories and populate the drop-down list.
                ENCategory cat = new ENCategory(0, "");
                List<ENCategory> categories = cat.readAll();
                foreach (ENCategory c in categories)
                {
                    ddlCategory.Items.Add(new ListItem(c.Name, c.Id.ToString()));
                }
            }
        }

        /// <summary>
        /// Validates all form fields before any write operation (Create or Update).
        /// Sets <c>lblMessage</c> with a descriptive error and returns <c>false</c> on
        /// the first failed rule, following a fail-fast strategy.
        /// Rules:
        /// <list type="bullet">
        ///   <item><description>Code: 1–16 characters (required).</description></item>
        ///   <item><description>Name: 0–32 characters.</description></item>
        ///   <item><description>Amount: integer in the range 0–9 999.</description></item>
        ///   <item><description>Price: float in the range 0–9 999.99.</description></item>
        ///   <item><description>Creation Date: must match the format <c>dd/MM/yyyy HH:mm:ss</c>.</description></item>
        /// </list>
        /// </summary>
        /// <returns><c>true</c> if all fields pass validation; <c>false</c> otherwise.</returns>
        private bool ValidateFields()
        {
            if (tbCode.Text.Length < 1 || tbCode.Text.Length > 16)
            {
                lblMessage.Text = "Error: Code must be between 1 and 16 characters.";
                return false;
            }
            if (tbName.Text.Length > 32)
            {
                lblMessage.Text = "Error: Name must be at most 32 characters.";
                return false;
            }
            int amount;
            if (!int.TryParse(tbAmount.Text, out amount) || amount < 0 || amount > 9999)
            {
                lblMessage.Text = "Error: Amount must be an integer between 0 and 9999.";
                return false;
            }
            float price;
            if (!float.TryParse(tbPrice.Text, out price) || price < 0 || price > 9999.99f)
            {
                lblMessage.Text = "Error: Price must be a value between 0 and 9999.99.";
                return false;
            }
            DateTime date;
            if (!DateTime.TryParseExact(tbCreationDate.Text, "dd/MM/yyyy HH:mm:ss",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out date))
            {
                lblMessage.Text = "Error: Creation Date must follow format dd/MM/yyyy HH:mm:ss.";
                return false;
            }
            return true;
        }

        /// <summary>
        /// Handles the Create button click. Validates all fields, checks that no product
        /// with the entered code already exists, then inserts a new product record.
        /// Displays a success or error message via <c>lblMessage</c>.
        /// </summary>
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateFields()) return;

                // Duplicate-code guard: abort if a product with this code already exists.
                ENProduct check = new ENProduct();
                check.Code = tbCode.Text;
                if (check.Read())
                {
                    lblMessage.Text = "Error: a product with that code already exists.";
                    return;
                }

                // Build the full product entity and persist it.
                ENProduct en = new ENProduct(
                    tbCode.Text,
                    tbName.Text,
                    int.Parse(tbAmount.Text),
                    float.Parse(tbPrice.Text),
                    int.Parse(ddlCategory.SelectedValue),
                    DateTime.ParseExact(tbCreationDate.Text, "dd/MM/yyyy HH:mm:ss",
                        System.Globalization.CultureInfo.InvariantCulture)
                );
                if (en.Create())
                    lblMessage.Text = "Product created successfully.";
                else
                    lblMessage.Text = "Error: could not create product.";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
                lblMessage.Text = "Error: " + ex.Message;
            }
        }

        /// <summary>
        /// Handles the Update button click. Validates all fields, verifies that a product
        /// with the entered code exists, then updates the existing record.
        /// Displays a success or error message via <c>lblMessage</c>.
        /// </summary>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateFields()) return;

                // Existence guard: abort if no product with this code is found.
                ENProduct check = new ENProduct();
                check.Code = tbCode.Text;
                if (!check.Read())
                {
                    lblMessage.Text = "Error: no product with that code exists.";
                    return;
                }

                ENProduct en = new ENProduct(
                    tbCode.Text,
                    tbName.Text,
                    int.Parse(tbAmount.Text),
                    float.Parse(tbPrice.Text),
                    int.Parse(ddlCategory.SelectedValue),
                    DateTime.ParseExact(tbCreationDate.Text, "dd/MM/yyyy HH:mm:ss",
                        System.Globalization.CultureInfo.InvariantCulture)
                );

                if (en.Update())
                    lblMessage.Text = "Product updated successfully.";
                else
                    lblMessage.Text = "Error: could not update product.";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
                lblMessage.Text = "Error: " + ex.Message;
            }
        }

        /// <summary>
        /// Handles the Delete button click. Validates the code field length, then attempts
        /// to delete the matching product. On success, all form fields are cleared to
        /// signal that the record no longer exists.
        /// Displays a success or error message via <c>lblMessage</c>.
        /// </summary>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbCode.Text.Length < 1 || tbCode.Text.Length > 16)
                {
                    lblMessage.Text = "Error: Code must be between 1 and 16 characters.";
                    return;
                }
                ENProduct en = new ENProduct();
                en.Code = tbCode.Text;
                if (en.Delete())
                {
                    lblMessage.Text = "Product deleted successfully.";
                    tbCode.Text = "";
                    tbName.Text = "";
                    tbAmount.Text = "";
                    tbPrice.Text = "";
                    tbCreationDate.Text = "";
                }
                else
                    lblMessage.Text = "Error: could not delete product.";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
                lblMessage.Text = "Error: " + ex.Message;
            }
        }

        /// <summary>
        /// Handles the Read button click. Looks up the product by the code entered in
        /// <c>tbCode</c> and fills all other fields with the retrieved data.
        /// Displays a success or error message via <c>lblMessage</c>.
        /// </summary>
        protected void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbCode.Text.Length < 1 || tbCode.Text.Length > 16)
                {
                    lblMessage.Text = "Error: Code must be between 1 and 16 characters.";
                    return;
                }
                ENProduct en = new ENProduct();
                en.Code = tbCode.Text;
                if (en.Read())
                {
                    tbName.Text = en.Name;
                    tbAmount.Text = en.Amount.ToString();
                    tbPrice.Text = en.Price.ToString();
                    ddlCategory.SelectedValue = en.Category.ToString();
                    tbCreationDate.Text = en.CreationDate.ToString("dd/MM/yyyy HH:mm:ss");
                    lblMessage.Text = "Product read successfully.";
                }
                else
                    lblMessage.Text = "Error: product not found.";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
                lblMessage.Text = "Error: " + ex.Message;
            }
        }


        /// <summary>
        /// Handles the Read First button click. Loads the first product in alphabetical
        /// code order and populates all form fields with its data.
        /// Does not require any input from the user.
        /// Displays a success or error message via <c>lblMessage</c>.
        /// </summary>
        protected void btnReadFirst_Click(object sender, EventArgs e)
        {
            try
            {
                ENProduct en = new ENProduct();
                if (en.ReadFirst())
                {
                    tbCode.Text = en.Code;
                    tbName.Text = en.Name;
                    tbAmount.Text = en.Amount.ToString();
                    tbPrice.Text = en.Price.ToString();
                    ddlCategory.SelectedValue = en.Category.ToString();
                    tbCreationDate.Text = en.CreationDate.ToString("dd/MM/yyyy HH:mm:ss");
                    lblMessage.Text = "First product read successfully.";
                }
                else
                    lblMessage.Text = "Error: no products found.";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
                lblMessage.Text = "Error: " + ex.Message;
            }
        }


        /// <summary>
        /// Handles the Read Prev button click. Using the code currently shown in
        /// <c>tbCode</c> as a reference point, loads the previous product in
        /// alphabetical order and updates all form fields.
        /// Displays a success or error message via <c>lblMessage</c>.
        /// </summary>
        protected void btnReadPrev_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbCode.Text.Length < 1 || tbCode.Text.Length > 16)
                {
                    lblMessage.Text = "Error: Code must be between 1 and 16 characters.";
                    return;
                }
                ENProduct en = new ENProduct();
                en.Code = tbCode.Text;
                if (en.ReadPrev())
                {
                    tbCode.Text = en.Code;
                    tbName.Text = en.Name;
                    tbAmount.Text = en.Amount.ToString();
                    tbPrice.Text = en.Price.ToString();
                    ddlCategory.SelectedValue = en.Category.ToString();
                    tbCreationDate.Text = en.CreationDate.ToString("dd/MM/yyyy HH:mm:ss");
                    lblMessage.Text = "Previous product read successfully.";
                }
                else
                    lblMessage.Text = "Error: no previous product found.";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
                lblMessage.Text = "Error: " + ex.Message;
            }
        }

        /// <summary>
        /// Handles the Read Next button click. Using the code currently shown in
        /// <c>tbCode</c> as a reference point, loads the next product in alphabetical
        /// order and updates all form fields.
        /// Displays a success or error message via <c>lblMessage</c>.
        /// </summary>
        protected void btnReadNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbCode.Text.Length < 1 || tbCode.Text.Length > 16)
                {
                    lblMessage.Text = "Error: Code must be between 1 and 16 characters.";
                    return;
                }
                ENProduct en = new ENProduct();
                en.Code = tbCode.Text;
                if (en.ReadNext())
                {
                    tbCode.Text = en.Code;
                    tbName.Text = en.Name;
                    tbAmount.Text = en.Amount.ToString();
                    tbPrice.Text = en.Price.ToString();
                    ddlCategory.SelectedValue = en.Category.ToString();
                    tbCreationDate.Text = en.CreationDate.ToString("dd/MM/yyyy HH:mm:ss");
                    lblMessage.Text = "Next product read successfully.";
                }
                else
                    lblMessage.Text = "Error: no next product found.";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Product operation has failed. Error: {0}", ex.Message);
                lblMessage.Text = "Error: " + ex.Message;
            }
        }
    }
}