using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
//using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.IO;

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public int totalRules { get; set; } = 0;
        List<IRule> RuleList = new List<IRule>();
        ObservableCollection<IRule> UserRuleList = new ObservableCollection<IRule>();

        BindingList<ISystemItem> Files = new BindingList<ISystemItem>();
        BindingList<ISystemItem> Folders = new BindingList<ISystemItem>();
        BindingList<ISystemItem> CurrentBatch = null;

        BindingList<String> NameRules = new BindingList<String>();
        ObservableCollection<Preset> Presets = new ObservableCollection<Preset>();
        public event PropertyChangedEventHandler PropertyChanged;

        public class SetVisibility : INotifyPropertyChanged, ICloneable
        {
            public string Visibility { get; set; } = "Visible";
            public event PropertyChangedEventHandler PropertyChanged;
            public object Clone()
            {
                return MemberwiseClone();
            }
        }
        SetVisibility _dropFileArea = new SetVisibility();
        public MainWindow()
        {
            //CreateDLLFolder();
            InitializeComponent();
            totalRules = RuleFactory.Instance().countRules();
            for (int i = 0; i < totalRules; i++)
            {
                RuleList.Add(RuleFactory.Instance().Create(i));
                NameRules.Add(RuleList[i].GetName());
            }
            var a = AppDomain.CurrentDomain.BaseDirectory + $"Plugins";
            var pluginFiles = Directory.GetFiles(a, "*.dll");
            foreach (var file in pluginFiles)
            {
                var asm = Assembly.LoadFile(file);
                //MessageBox.Show(asm.ToString());
            }
            DropFileRec.DataContext = _dropFileArea;

        }
        public class MyFileInfo
        {
            public string FullPath { get; set; }
            public string ShortPath { get; set; }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        ObservableCollection<MyFileInfo> _listFiles = new ObservableCollection<MyFileInfo>();
        ObservableCollection<MyFileInfo> _listFolders = new ObservableCollection<MyFileInfo>();
        private void AddFilesBtn_Click(object sender, RoutedEventArgs e)
        {
            AddFilesButtonOptions.IsOpen = true;
            //if (FilesTable.ItemsSource == _listFiles)
            //{
            //    var screen = new OpenFileDialog
            //    {
            //        Multiselect = true
            //    };
            //    if (screen.ShowDialog() == true)
            //    {
            //        foreach (var file in screen.FileNames)
            //        {
            //            var fullPath = file;
            //            var info = new FileInfo(fullPath);
            //            var shortName = info.Name;
            //            _listFiles.Add(new MyFileInfo() { FullPath = fullPath, ShortPath = shortName });
            //        }
            //        _dropFileArea.Visibility = "Hidden";
            //    }
            //}
            //else if (FilesTable.ItemsSource == _listFolders)
            //{
            //    CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            //    dialog.IsFolderPicker = true;
            //    if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            //    {
            //        MessageBox.Show("You selected: " + dialog.FileName);
            //    }
            //}
           
           
        }

  
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FilesTable.ItemsSource = _listFiles;     
        }

        private void FilesTable_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                // Assuming you have one file that you care about, pass it off to whatever
                // handling code you have defined.
                _listFiles.Clear();
                foreach (var file in files)
                {
                    var fullPath = file;
                    var info = new FileInfo(fullPath);
                    var shortName = info.Name;
                    _listFiles.Add(new MyFileInfo() { FullPath = fullPath, ShortPath = shortName });
                }
                _dropFileArea.Visibility = "Hidden";
            }
        }

        private void FilesTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            _listFiles.Clear();
            _dropFileArea.Visibility = "Visible";
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
           MessageBox.Show(FilesTable.SelectedIndex.ToString());
        }

        private void FolderSwitch_Click(object sender, RoutedEventArgs e)
        {
            FilesTable.ItemsSource = _listFolders;
        }

        private void FileSwitch_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void CreateDLLFolder()
        {
            string exePath = Assembly.GetExecutingAssembly().Location;
            string folderPath = System.IO.Path.GetDirectoryName(exePath);
            folderPath += @"\Plugins";
            DirectoryInfo folder = new DirectoryInfo(folderPath);
            if (!folder.Exists)
            {
                Directory.CreateDirectory(folderPath);
            }

        }

    }

    public class WorkFlow
    {
        public string isActive { get; set; }
        public string rulesList { get; set; }
        public string filesList { get; set; }
        public string foldersList { get; set; }
    }
    public class FileItem
    {
        public string Name;
        public string Image;
        public string fileExtention;
        public string Path;
        public string newName;
        public string status;
    }
    public class FolderItem
    {
        public string Name;
        public string Image;
        public string Path;
        public string newName;
        public string status;
    }
    public class rulesFromPreset
    {
        public string name { get; set; }
        public string _arg1 { get; set; }
        public string _arg2 { get; set; }
    }
    public class Preset
    {
        public string PresetName { get; set; }
        public string PresetPath { get; set; }
    }
}
