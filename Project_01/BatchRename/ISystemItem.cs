using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public interface ISystemItem
    {
        string Name { get; set; }
        string Path { get; set; }
        string NewName { get; set; }
        string Status { get; set; }
    }
}
