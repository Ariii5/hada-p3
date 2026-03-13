using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
            this.constring = ConfigurationManager.ConnectionStrings["HadaDB"].ToString(); ;
        }
         public bool Create(ENProduct en)
         {
             return false;
         }
    }
}
