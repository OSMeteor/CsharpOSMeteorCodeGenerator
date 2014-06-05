using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsharpOSMeteorCodeGenerator.Model
{
    public class Table
    {
        #region Attributes

        private string catalog;
        private string schema;
        private string name;
        private List<Column> columns;

        #endregion

        #region Constructor

        public Table()
        {
            columns = new List<Column>();
        }

        #endregion

        #region Properties

        public string Catalog
        {
            get { return catalog; }
            set { catalog = value; }
        }

        public string Schema
        {
            get { return schema; }
            set { schema = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public List<Column> Columns
        {
            get { return columns; }

        }

        public void AddColumn(Column column)
        {
            columns.Add(column);
        }

        #endregion
    }
}
