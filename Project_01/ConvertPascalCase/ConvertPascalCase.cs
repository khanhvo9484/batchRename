using BatchRename;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Newtonsoft.Json;

namespace ConvertPascalCase
{
    [JsonObject(IsReference = true)]
    public class ConvertPascalCaseRule : IRule, INotifyPropertyChanged
    {
        public string name = "Convert Pascal Case";
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
        public ConvertPascalCaseWindow ConfigurationUI { get; set; }

        public ConvertPascalCaseRule()
        {
            this.Name = this.name;
            this.ConfigurationUI = new ConvertPascalCaseWindow(this);
        }
        public IRule Clone()
        {
            return new ConvertPascalCaseRule();
        }
        public IRule Clone(string arg1, string arg2)
        {
            return new ConvertPascalCaseRule();
        }
        public string GetName()
        {
            return this.name;
        }
        public string Rename(string oldname)
        {
            string Temp1 = oldname.Substring(0, 1);
            string Temp2 = oldname.Substring(1);
            Temp1 = Temp1.ToUpper();
            return Temp1 + Temp2;
        }

        public void Setup(Dictionary<string, string> agrs, List<string> arrchars)
        {
            //do nothing
        }

        public UserControl GetUI()
        {
            return ConfigurationUI;
        }
    }
}
