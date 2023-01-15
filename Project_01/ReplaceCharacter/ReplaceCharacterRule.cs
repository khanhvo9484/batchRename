using BatchRename;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;

namespace ReplaceCharacter
{
    [JsonObject(IsReference = true)]
    public class ReplaceCharacterRule : IRule, INotifyPropertyChanged
    {
        public string name = "Replace character";
        public string _arg1;
        public string _arg2;
        public List<string> _ArrChar;
        [JsonIgnore]
        public string _TargetChar;

        public event PropertyChangedEventHandler PropertyChanged;
        [JsonIgnore]
        public ReplaceCharacterWindow ConfigurationUI { get; set; }
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

        public ReplaceCharacterRule()
        {
            this.Name = this.name;
            this._ArrChar = null;
            this._TargetChar = "";
        }
        public string GetName()
        {
            return this.name;
        }


        public ReplaceCharacterRule(List<string> ArrChar, string TargetChar)
        {

            this.Name = this.name;

            this._ArrChar = ArrChar;
            this._TargetChar = TargetChar;
            this.ConfigurationUI = new ReplaceCharacterWindow(this);
        }

        public string Rename(string oldname)
        {
            try
            {
                if (!Helper.Instance().isFile(oldname))
                {
                    //FOLDER
                    string resultName = oldname;
                    foreach (var replacedCharacter in _ArrChar)
                    {
                        string pattern = $@"({replacedCharacter})";
                        resultName = Regex.Replace(resultName, pattern, _TargetChar);
                    }
                    return resultName;
                }
                else
                {
                    //FILE
                    int index = oldname.LastIndexOf('.');
                    string[] filenameList = new string[2];
                    filenameList[0] = oldname.Substring(0, index);
                    filenameList[1] = oldname.Substring(index + 1);


                    foreach (var replacedCharacter in _ArrChar)
                    {
                        string pattern = $@"({replacedCharacter})";
                        filenameList[0] = Regex.Replace(filenameList[0], pattern, _TargetChar);
                    }
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
            _ArrChar = arrchars;
            _arg1 = arrchars[0];
            _TargetChar = agrs["TargetChar"];
            _arg2 = agrs["TargetChar"];
        }

        public IRule Clone()
        {
            return new ReplaceCharacterRule(_ArrChar, _TargetChar);
        }
        public IRule Clone(string arg1, string arg2)
        {
            List<string> temp = new List<string>(); ;
            temp.Add(arg1);
            return new ReplaceCharacterRule(temp, arg2);
        }
        public UserControl GetUI()
        {
            return ConfigurationUI;
        }
    }
}
