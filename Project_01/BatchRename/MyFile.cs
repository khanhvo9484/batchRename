using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;

namespace BatchRename
{
    public class MyFile: INotifyPropertyChanged, ISystemItem
    {
        public string Name { get; set; }
        public string FileExtention { get; set; }
        public string Path { get; set; }
        public string NewName { get; set; }
        public string Status { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public MyFile(string fileDir)
        {
            FileInfo myfile = new FileInfo(fileDir);
            this.Name = myfile.Name;
            this.NewName = this.Name;
            this.Path = fileDir;
            this.FileExtention = myfile.Extension;
            this.Status = "";
        }
        public MyFile(string fileDir,string newName)
        {
            FileInfo myfile = new FileInfo(fileDir);
            this.Name = myfile.Name;
            this.NewName = this.Name;
            this.Path = fileDir;
            this.FileExtention = myfile.Extension;
            this.Status = "";
        }

        public MyFile(string name, string fileExtension, string newName, string path, string status)
        {
            this.Name = name;
            this.NewName = newName;
            this.Path = path;
            this.FileExtention = fileExtension;
            this.Status = status;
        }

    }
}
