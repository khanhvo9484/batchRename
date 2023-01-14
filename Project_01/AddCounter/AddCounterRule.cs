using BatchRename;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;

namespace AddCounter
{

        [JsonObject(IsReference = true)]
        public class AddCounterRule : IRule, INotifyPropertyChanged
        {
            [JsonIgnore]
            public int _pureValue;
            [JsonIgnore]
            public int _start;


            [JsonIgnore]
            public int _step;
            public string _arg1;
            public string _arg2;
            public string name = "Add Counter";

            public event PropertyChangedEventHandler PropertyChanged;
            [JsonIgnore]
            public AddCounterWindow ConfigurationUI { get; set; }
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
                        if (this.isActive == false)
                        {
                            this._start = this._pureValue;


                        }
                    }
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("isActive"));
                }
            }
            public AddCounterRule()
            {
                this._pureValue = 0;
                this._start = 0;
                this._step = 0;
                this.Name = this.name;
                //this.ConfigurationUI = new AddCounterWindow(this);
            }
            public AddCounterRule(int start, int step)
            {
                var builder = new StringBuilder();
                this._arg1 = builder.Append(start).ToString();
                var builder1 = new StringBuilder();
                this._arg2 = builder1.Append(step).ToString();
                this._pureValue = start;
                this._start = start;
                this._step = step;
                this.Name = this.name;
                this.ConfigurationUI = new AddCounterWindow(this);
            }

            public IRule Clone()
            {
                return new AddCounterRule(0, 0);
            }

            public IRule Clone(string arg1, string arg2)
            {
                return new AddCounterRule(Int16.Parse(arg1), Int16.Parse(arg2));
            }

            public string GetName()
            {
                this._start = this._pureValue;
                return this.name;
            }

            public string Rename(string oldname)
            {
            try
            {
                if (!Helper.Instance().isFile(oldname))
                {



                    string result = oldname + " ";
                    result += _start.ToString("00");
                    this._start += _step;
                    return result;

                }
                else
                {
                    //File
                    string[] filenameList = oldname.Split('.');


                    filenameList[0] += " " + _start.ToString("00");
                    string result = string.Join(".", filenameList);
                    _start += _step;
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
                this._start = Int16.Parse(agrs["start"]);
                this._arg1 = agrs["start"];
                this._pureValue = this._start;
                this._step = Int16.Parse(agrs["step"]);
                this._arg2 = agrs["step"];
            }

            public UserControl GetUI()
            {
                return ConfigurationUI;
            }
        }
    
}
