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

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
            InitializeComponent();
            var a = AppDomain.CurrentDomain.BaseDirectory+$"Plugins";
            var pluginFiles=Directory.GetFiles(a,"*.dll");
            foreach(var file in pluginFiles)
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
    }
}
