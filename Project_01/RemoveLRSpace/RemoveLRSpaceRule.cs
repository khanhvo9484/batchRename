using BatchRename;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
namespace RemoveLRSpace
{
    [JsonObject(IsReference = true)]
    public class RemoveLRSpaceRule : IRule, INotifyPropertyChanged
    {
        public string name = "Remove left right space";
        public string _arg1 = "";
        public string _arg2 = "";

        public event PropertyChangedEventHandler PropertyChanged;
        [JsonIgnore]
        public string Name { get; set; }
        private bool isActive { get; set; } = false;
        [JsonIgnore]
        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
            set
            {
                if (value != this.isActive)
                {
                    this.isActive = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("isActive"));
                }
            }
        }
        [JsonIgnore]
        public RemoveLRSpaceWindow ConfigurationUI { get; set; }

        public RemoveLRSpaceRule()
        {
            this.ConfigurationUI = new RemoveLRSpaceWindow(this);
            this.Name = this.name;
        }
        public IRule Clone()
        {
            return new RemoveLRSpaceRule();
        }
        public IRule Clone(string arg1, string arg2)
        {
            return new RemoveLRSpaceRule();
        }
        public string GetName()
        {
            return this.name;
        }
        public string Rename(string oldname)
        {
            try
            {
                if (!Helper.Instance().isFile(oldname))
                {
                    return oldname.Trim();
                }
                else
                {
                    int index = oldname.LastIndexOf('.');
                    string[] filenameList = new string[2];

                    filenameList[0] = oldname.Substring(0,index).Trim();
                    filenameList[1] = oldname.Substring(index + 1);
                    return string.Join(".", filenameList);
                }
            }
            catch
            {
                return oldname;
            }
            
        }

        public void Setup(Dictionary<string, string> agrs, List<string> arrchars)
        {
            // do nothing
        }

        public UserControl GetUI()
        {
            return ConfigurationUI;
        }
    }
}
