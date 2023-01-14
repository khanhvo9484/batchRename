using BatchRename;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Newtonsoft.Json;


namespace AddSuffix
{
    [JsonObject(IsReference = true)]
    public class AddSuffixRule : IRule, INotifyPropertyChanged
    {
        [JsonIgnore]
        public string _Suffix;
        public string name = "Add Suffix";
        public string _arg1;
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
        public AddSuffixWindow ConfigurationUI { get; set; }

        public AddSuffixRule()
        {
            this._Suffix = "";
            this.Name = this.name;
        }

        public AddSuffixRule(string Suffix)
        {
            this.Name = this.name;

            this._Suffix = Suffix;
            this.ConfigurationUI = new AddSuffixWindow(this);
        }

        public IRule Clone()
        {
            return new AddSuffixRule(_Suffix);
        }
        public IRule Clone(string arg1, string arg2)
        {
            return new AddSuffixRule(arg1);
        }
        public string GetName()
        {
            return this.name;
        }
        public string Rename(string oldname)
        {
            if (!Helper.Instance().isFile(oldname))
            {
                //FOLDER
                if (Helper.Instance().hasSuffix(oldname, _Suffix))
                {
                    return oldname;
                }
                else
                {
                    return oldname + " " + _Suffix;
                }
            }
            else
            {
                //FILE
                int index = -1;
                index = oldname.LastIndexOf('.');
                string resultStr="";
                string[] filenameList = {};
                if (index != -1)
                {
                    resultStr = oldname.Substring(0,index);
                    filenameList[1]= oldname.Substring(index+1, oldname.Length-index-1);
                }
  
                
                if (Helper.Instance().hasSuffix(resultStr, _Suffix))
                {
                    return oldname;
                }
                else
                {
                    resultStr = resultStr + " " + _Suffix;
                }
                filenameList[0] = resultStr;
                return string.Join(".", filenameList);
            }
        }

        public void Setup(Dictionary<string, string> agrs, List<string> arrchars)
        {
            _Suffix = agrs["Suffix"];
            _arg1 = agrs["Suffix"];
        }

        public UserControl GetUI()
        {
            return ConfigurationUI;
        }
    }
    }
