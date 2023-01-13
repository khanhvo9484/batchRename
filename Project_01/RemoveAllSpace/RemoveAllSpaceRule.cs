﻿using BatchRename;
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
                string[] filenameList = oldname.Split('.');
                string strResult = filenameList[0];
                string pattern = @"\s";
                strResult = Regex.Replace(strResult, pattern, "");
                filenameList[0] = strResult;
                return string.Join(".", filenameList);
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