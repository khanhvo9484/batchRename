using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BatchRename
{
    public class Helper
    {

        private static Helper _instance = null;

        private Helper()
        {
            // Do nothing
        }


        public static Helper Instance()
        {
            if (_instance == null)
            {
                _instance = new Helper();
            }
            return _instance;
        }

        public bool isExistInList(BindingList<ISystemItem> list, ISystemItem item)
        {
            foreach (ISystemItem item2 in list)
            {
                if(item2.Path == item.Path)
                {
                    return true;
                }

            }
            return false;

        }

        public bool isFile(string name)
        {
            string pattern = @"\.";
            Regex rg = new Regex(pattern);

            var countMatches = rg.Matches(name).Count;

            if (countMatches == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool hasEndDigit(string name)
        {
            string pattern = @"\d+$";
            Regex rg = new Regex(pattern);
            var countMatches = rg.Matches(name).Count;
            if (countMatches == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool hasPrefix(string name, string prefix)
        {
            string pattern = $@"^({prefix})";
            Regex rg = new Regex(pattern);
            var countMatches = rg.Matches(name).Count;
            if (countMatches == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool hasSuffix(string name, string suffix)
        {
            string pattern = $@"({suffix})$";
            Regex rg = new Regex(pattern);
            var countMatches = rg.Matches(name).Count;
            if (countMatches == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

