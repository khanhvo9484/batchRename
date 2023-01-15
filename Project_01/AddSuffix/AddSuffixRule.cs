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
                try
                {
                    int index = oldname.LastIndexOf('.');
            
     
                    string[] filenameList = new string[2];
                    if (index != -1)
                    {
                        filenameList[0] = oldname.Substring(0, index);
                        filenameList[1] = oldname.Substring(index + 1);
                    }


                    if (Helper.Instance().hasSuffix(filenameList[0], _Suffix))
                    {
                        return oldname;
                    }
                    else
                    {
                        filenameList[0] = filenameList[0] + " " + _Suffix;
                    }
                    return string.Join(".", filenameList);
                }
                catch
                {
                    return oldname;
                }
               
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
