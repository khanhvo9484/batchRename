using BatchRename;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;
using Newtonsoft.Json;

namespace AddPrefix
{
    [JsonObject(IsReference = true)]
    public class AddPrefixRule:IRule, INotifyPropertyChanged
    {
        [JsonIgnore]
        public string _Prefix;
        public string name = "Add Prefix";
        public string _arg1;
        public string _arg2 = "";
        [JsonIgnore]
        public AddPrefixWindow ConfigurationUI { get; set; }
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
        //string IRule.Name { get; set; }

        public AddPrefixRule()
        {
            this._Prefix = "";
            this.Name = this.name;

        }
        public AddPrefixRule(string prefix)
        {
            this.Name = this.name;

            _Prefix = prefix;
            this.ConfigurationUI = new AddPrefixWindow(this);

        }
        public override string ToString()
        {
            return this.name;
        }
        public string GetName()
        {
            return this.name;
        }
        public string Rename(string oldname)
        {
            try
            {
                if (Helper.Instance().hasPrefix(oldname, _Prefix))
                {
                    return oldname;
                }
                else
                {
                    return _Prefix + " " + oldname;
                }
            }
            catch
            {
                return oldname;
            }
            
        }
        public void Setup(Dictionary<string, string> agrs, List<string> arrchars)
        {
            _Prefix = agrs["Prefix"];
            _arg1 = agrs["Prefix"];
        }
        public IRule Clone()
        {
            return new AddPrefixRule(_Prefix);
        }
        public IRule Clone(string arg1, string arg2)
        {
            return new AddPrefixRule(arg1);
        }

        public UserControl GetUI()
        {
            return ConfigurationUI;
        }

        UserControl IRule.GetUI()
        {
            throw new NotImplementedException();
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
