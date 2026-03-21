using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class ENCategory
    {
        private int id;
        private string name;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
        public ENCategory(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
        public bool Read()
        {
            CADCategory cad = new CADCategory();
            return cad.Read(this);
        }
        public List<ENCategory> readAll()
        {
            CADCategory cad = new CADCategory();
            return cad.readAll();
        }
    }
}
