using BatchRename;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LowerCase
{
    [JsonObject(IsReference = true)]
    public class LowerCaseRule : IRule, INotifyPropertyChanged
    {
        public string name = "Lower case";
        public string _arg1 = "";
        public string _arg2 = "";

        public event PropertyChangedEventHandler PropertyChanged;
        [JsonIgnore]
        public LowerCaseWindow ConfigurationUI { get; set; }
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
        public LowerCaseRule()
        {
            this.ConfigurationUI = new LowerCaseWindow(this);

            this.Name = this.name;
        }
        public IRule Clone()
        {
            return new LowerCaseRule();
        }
        public IRule Clone(string arg1, string arg2)
        {
            return new LowerCaseRule();
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
                    //FOLDER
                    return oldname.ToLower();
                }
                else
                {
                    //FILE
                    int index=oldname.LastIndexOf('.');
                    string[] filenameList = new string[2];
                    filenameList[0] = oldname.Substring(0, index);
                    filenameList[1] = oldname.Substring(index + 1);

                    filenameList[0] = filenameList[0].ToLower();
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
            //do nothing
        }

        public UserControl GetUI()
        {
            return ConfigurationUI;
        }
    }
}
