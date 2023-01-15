using BatchRename;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

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
        public string FirstLetterToUpper(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }
        public string Rename(string oldname)
        {
            try
            {
                if (!Helper.Instance().isFile(oldname))
                {
                    //FOLDER
                    Regex regex = new Regex(@"[ ]{2,}", RegexOptions.None);
                    var temp = regex.Replace(oldname, @" "); // "words with multiple spaces"
                    string[] tokens = temp.Split(new[] { " " }, StringSplitOptions.None);
                    var newName = "";
                    foreach (string token in tokens)
                    {
                        newName += FirstLetterToUpper(token);
                    }
                    return newName;
                }
                else
                {
                    var index = oldname.LastIndexOf('.');
                    string[] filenameList=new string[2];
                    filenameList[0]=oldname.Substring(0, index);
                    filenameList[1]=oldname.Substring(index + 1);

                    Regex regex = new Regex(@"[ ]{2,}", RegexOptions.None);
                    var temp = regex.Replace(filenameList[0], @" "); // "words with multiple spaces"
                    string[] tokens = temp.Split(new[] { " " }, StringSplitOptions.None);
                    var newName = "";
                    foreach (string token in tokens)
                    {
                        newName += FirstLetterToUpper(token);
                    }
                    filenameList[0]=newName;
                    
                    return string.Join(".",filenameList);
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
