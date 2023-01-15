using BatchRename;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Newtonsoft.Json;


namespace ChangeExtension
{

    [JsonObject(IsReference = true)]
    public class ChangeExtensionRule : IRule, INotifyPropertyChanged
    {
        [JsonIgnore]
        public string _NewExt;
        public string _arg1;
        public string _arg2 = "";
        public string name = "Change Extension";

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
        public ChangeExtensionWindow ConfigurationUI { get; set; }

        public ChangeExtensionRule()
        {
            //this.ConfigurationUI = new ChangeExtensionWindow(this);
            this._NewExt = "";
            this.Name = this.name;
        }
        public ChangeExtensionRule(string newExt)
        {

            this.Name = this.name;

            this._NewExt = newExt;
            this.ConfigurationUI = new ChangeExtensionWindow(this);
        }


        public IRule Clone()
        {
            return new ChangeExtensionRule(_NewExt);
        }
        public IRule Clone(string arg1, string arg2)
        {
            return new ChangeExtensionRule(arg1);
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
                    //Folder
                    return oldname;
                }
                else
                {
                    //File
                    int lastIndex = oldname.LastIndexOf('.');
                    string[] filenameList = new string[2];
                    if (lastIndex != -1)
                    {
                        filenameList[0] = oldname.Substring(0, lastIndex); // "My. name. is Bond"
                                                                         // "_James Bond!"
                    }

                    filenameList[1] = _NewExt;
                    string result = string.Join(".", filenameList);
                    return result;
                }
            }
            catch
            {
                return oldname;
            }
            
        }

        public void Setup(Dictionary<string, string> agrs, List<string> arrchars)
        {
            _NewExt = agrs["NewExt"];
            _arg1 = agrs["NewExt"];
        }

        public UserControl GetUI()
        {
            return ConfigurationUI;
        }
    }
}
