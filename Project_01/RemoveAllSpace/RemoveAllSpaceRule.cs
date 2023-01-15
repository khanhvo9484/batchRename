using BatchRename;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using Newtonsoft.Json;

namespace RemoveAllSpace
{
    [JsonObject(IsReference = true)]
    public class RemoveAllSpaceRule : IRule, INotifyPropertyChanged
    {
        public string name = "Remove All Space";
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
        public RemoveAllSpaceWindow ConfigurationUI { get; set; }

        public RemoveAllSpaceRule()
        {
            this.ConfigurationUI = new RemoveAllSpaceWindow(this);

            this.Name = this.name;
        }
        public IRule Clone()
        {
            return new RemoveAllSpaceRule();
        }
        public IRule Clone(string arg1, string arg2)
        {
            return new RemoveAllSpaceRule();
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
                    string strResult = oldname;
                    string pattern = @"\s";
                    strResult = Regex.Replace(strResult, pattern, "");
                    return strResult;
                }
                else
                {
                    //FILE
                    int index=oldname.LastIndexOf('.');
                    string[] filenameList = new string[2];
                    filenameList[0] = oldname.Substring(0,index);
                    filenameList[1] = oldname.Substring(index+1);

                    string pattern = @"\s";
                    filenameList[0] = Regex.Replace(filenameList[0], pattern, "");

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
