using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CsharpOSMeteorCodeGenerator.Model
{
    public class OSMyNode : TreeNode
    {
        private object value;
        public object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
    }
}
