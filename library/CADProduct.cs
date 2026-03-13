using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    internal class CADProduct
    {
        private string constring;   //cadena a la base de datos
        public CADProduct(string direccion)
        {
            this.constring = direccion;
        }
         public bool Create(ENProduct en)
         {
             return false;
         }
    }
}
