using library;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace proWeb
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ENCategory cat = new ENCategory(0, "");
                List<ENCategory> categories = cat.readAll();
                foreach (ENCategory c in categories)
                {
                    ddlCategory.Items.Add(new ListItem(c.Name, c.Id.ToString()));
                }
            }
        }

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

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateFields()) return;

                ENProduct check = new ENProduct();
                check.Code = tbCode.Text;
                if (check.Read())
                {
                    lblMessage.Text = "Error: a product with that code already exists.";
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateFields()) return;

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