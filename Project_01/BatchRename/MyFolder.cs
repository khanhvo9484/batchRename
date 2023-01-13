using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;

namespace BatchRename
{
    public class MyFolder : INotifyPropertyChanged, ISystemItem
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string NewName { get; set; }
        public string Status { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public MyFolder(string fileDir)
        {
            FileInfo myfile = new FileInfo(fileDir);
            this.Name = myfile.Name;
            this.NewName = this.Name;
            this.Path = fileDir;
            this.Status = "";
        }

        public MyFolder(string name, string newName, string path, string status)
        {
            this.Name = name;
            this.NewName = newName;
            this.Path = path;
            this.Status = status;
        }
    }
}
