using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BatchRename
{
    public interface IRule
    {
        string Name { get; set; }
        bool IsActive { get; set; }
        string Rename(string oldname);
        void Setup(Dictionary<string, string> agrs, List<string> arrchars);
        string GetName();
        IRule Clone();
        IRule Clone(string arg1, string arg2);
        UserControl GetUI();
    }
}
