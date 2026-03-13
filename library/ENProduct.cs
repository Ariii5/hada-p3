using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    internal class ENProduct
    {
        private string code;   //falta poner propiedades públicas con campo de respaldo para obtener dichos valores
        private string name;
        private int amount;
        private float price;
        private int categoy;
        private DateTime creationDate;

        public ENProduct()
        {
            code = ""; 
            name = ""; 
            amount = 0;
            price = 0.0f;
            categoy = 0;
            creationDate = DateTime.Now;
        }
        public ENProduct(string code, string name, int amount, float price, int category, DateTime creationDate)
        {
            this.code = code;
            this.name = name;
            this.amount = amount;
            this.price = price;
            this.categoy = category;
            this.creationDate = creationDate;
        }
       /* public bool Create()
        {
            if (error)
            {
                return false;
            }
        }*/
    }
}
