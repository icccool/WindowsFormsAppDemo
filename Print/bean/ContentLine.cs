using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppDemo.Print.bean
{
    public class ContentLine
    {
        public int sortWeight { get; set; }
        public List<ContentBlock> cols;
    }
}
