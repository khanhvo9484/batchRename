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
using Newtonsoft.Json;
using RestoreWindowPlace;
//using System.Windows.Forms;


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
        public string Type { get; set; } // select file or folder


        ObservableCollection<IRule> UserRuleList = new ObservableCollection<IRule>();
        Helper myHelper = Helper.Instance();
        BindingList<ISystemItem> Files = new BindingList<ISystemItem>();
        BindingList<ISystemItem> Folders = new BindingList<ISystemItem>();


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
            CreateDLLFolder();
            InitializeComponent();
            totalRules = RuleFactory.Instance().countRules();
            for (int i = 0; i < totalRules; i++)
            {
                RuleList.Add(RuleFactory.Instance().Create(i));
                NameRules.Add(RuleList[i].GetName());
            }
            Type = "file";
            DropFileRec.DataContext = _dropFileArea;
            ruleCombobox.ItemsSource = NameRules;
            rulesSideBar.ItemsSource = UserRuleList;
            FilesTable.ItemsSource = Files;
            presetCombobox.ItemsSource = Presets;
            FileSwitch_Click(null, null);
            ((App)Application.Current).WindowPlace.Register(this);

        }
        
        private void AddFilesBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Type == "file")
            {
                AddFilesButtonOptions.IsOpen = true;
            }
            else
            {
                CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                dialog.IsFolderPicker = true;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    MyFolder MyFileInfo= new MyFolder(dialog.FileName);
                    if(!myHelper.isExistInList(Folders, MyFileInfo))
                    {
                        Folders.Add(MyFileInfo);
                    }
                    
                    //MessageBox.Show("You selected: " + dialog.FileName);
                }
            }

        }

  
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FilesTable.ItemsSource = Files;
            loadAllPreset();
        }
        private void Box_checked_click(object sender, RoutedEventArgs e)
        {
            CheckBox b = sender as CheckBox;
            IRule rule = b.CommandParameter as IRule;

            Update_Preview();
        }

        private void Box_unchecked_click(object sender, RoutedEventArgs e)
        {
            CheckBox b = sender as CheckBox;
            IRule rule = b.CommandParameter as IRule;
            int index = UserRuleList.IndexOf(rule);
            if (index != -1)
            {
                if (checkAllRules.IsChecked == true)
                {
                    checkAllRules.IsChecked = false;
                }
                Update_Preview();

            }

        }
        private void FilesTable_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                List<MyFile> tempFiles = new List<MyFile>();
                List<MyFolder> tempFolders = new List<MyFolder>();
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                // Assuming you have one file that you care about, pass it off to whatever
                // handling code you have defined.


                    foreach (var file in files)
                    {
                    if (myHelper.isFile(file))
                    {
                        var MyFileInfo = new MyFile(file);
                        if (!myHelper.isExistInList(Files,MyFileInfo))
                        {
                            Files.Add(MyFileInfo);
                        }
                    }
                    else
                    {
                        var MyFileInfo = new MyFolder(file);
                        if (!myHelper.isExistInList(Folders, MyFileInfo))
                        {
                            Folders.Add(MyFileInfo);
                        }
                    }
                    }
     
                _dropFileArea.Visibility = "Hidden";
            }
        }

        private void FilesTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Type == "file")
            {
                Files.Clear();
            }
            else
            {
                Folders.Clear();
            }
            _dropFileArea.Visibility = "Visible";
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
           MessageBox.Show(FilesTable.SelectedIndex.ToString());
        }

        private void FolderSwitch_Click(object sender, RoutedEventArgs e)
        {
            FileSwitch.Background = Brushes.Transparent;
            FolderSwitch.Background=Brushes.Beige;
            FilesTable.ItemsSource = Folders;
            Type= "folder";
        }

        private void FileSwitch_Click(object sender, RoutedEventArgs e)
        {
            FolderSwitch.Background = Brushes.Transparent;
            FileSwitch.Background = Brushes.Beige;
            FilesTable.ItemsSource = Files;
            Type = "file";
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

        private void ruleCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ruleCombobox.SelectedIndex;
            if (index != -1)
            {
                UserRuleList.Add(RuleList[index].Clone());
            }
            ruleCombobox.SelectedIndex = -1;
        }

        private void checkAllRules_Click(object sender, RoutedEventArgs e)
        {
            foreach (var rule in UserRuleList)
            {
                rule.IsActive = (bool)checkAllRules.IsChecked;
            }
        }
        private void checkAllRules_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void Update_Preview()
        {
            List<string> listFileName = new List<string>();
            List<string> listFolderName = new List<string>();

            for (int i = 0; i < Files.Count; i++)
            {
                listFileName.Add(Files[i].Name);
                Files[i].NewName = Files[i].Name;
            }

            for (int i = 0; i < Folders.Count; i++)
            {
                listFolderName.Add(Folders[i].Name);
                Folders[i].NewName = Folders[i].Name;
            }

            List<int> flag = new List<int>();

            for (int i = 0; i < UserRuleList.Count; i++)
            {
                flag.Add(0);
            }

            for (int i = 0; i < UserRuleList.Count; i++)
            {
                if (UserRuleList[i].IsActive == true)
                {
                    if (UserRuleList[i].Name == "Add Counter" && flag[i] == 0)
                    {
                        string temp = UserRuleList[i].GetName();
                        flag[i] = 1;
                    }
                    List<string> tempFile = new List<string>();
                    List<string> tempFolder = new List<string>();
                    // Doi file
                    for (int j = 0; j < listFileName.Count; j++)
                    {
                        tempFile.Add(UserRuleList[i].Rename(listFileName[j]));
                        Files[j].NewName = tempFile[j];
                        listFileName[j] = tempFile[j];
                    }
                    // Doi folder
                    for (int j = 0; j < listFolderName.Count; j++)
                    {
                        tempFolder.Add(UserRuleList[i].Rename(listFolderName[j]));
                        Folders[j].NewName = tempFolder[j];
                        listFolderName[j] = tempFolder[j];
                    }
                }
            }
        }
        private void Remove_Rule_Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            IRule rule = (IRule)b.CommandParameter;
            UserRuleList.Remove(rule);
            Update_Preview();
        }

        private void rulesSideBar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update_Preview();
        }

        private void AddByFiles_Click(object sender, RoutedEventArgs e)
        {
            if (Type=="file")
            {
                var screen = new OpenFileDialog
                {
                    Multiselect = true
                };
                if (screen.ShowDialog() == true)
                {
                    foreach (var file in screen.FileNames)
                    {
                        var MyFileInfo=new MyFile(file);
                        if (!myHelper.isExistInList(Files,MyFileInfo))
                        {
                            Files.Add(MyFileInfo);
                        }
                    }
                    if(Files.Count > 0)
                    {
                        _dropFileArea.Visibility = "Hidden";
                    }
                   
                }
            }

        }

        private void DeleteRecordBtn_Click(object sender, RoutedEventArgs e)
        {
            int index = FilesTable.SelectedIndex;
            if(index != -1)
            {
                if (Type == "file")
                {
                    Files.RemoveAt(index);
                    if(Files.Count == 0)
                    {
                        _dropFileArea.Visibility = "Visible";
                    }
                }
                else
                {
                    Folders.RemoveAt(index);
                    if (Folders.Count == 0)
                    {
                        _dropFileArea.Visibility = "Visible";
                    }
                }
            }
      

        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            int index = rulesSideBar.SelectedIndex;
            if (index != -1)
            {
                if (index < UserRuleList.Count-1)
                {
                    UserRuleList.Move(index, index + 1);
                }
                else
                {

                }
            }
        }

        private void DownMultiButton_Click(object sender, RoutedEventArgs e)
        {
            int index = rulesSideBar.SelectedIndex;
            if (index != -1)
            {
                if (index < UserRuleList.Count - 1)
                {
                    UserRuleList.Move(index, UserRuleList.Count - 1);
                }
                else
                {

                }
            }
        }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            int index = rulesSideBar.SelectedIndex;
            if (index != -1)
            {
                if (index > 0)
                {
                    UserRuleList.Move(index, index - 1);
                }
                else
                {

                }
            }

        }

        private void UpMultiButton_Click(object sender, RoutedEventArgs e)
        {
            int index = rulesSideBar.SelectedIndex;
            if (index != -1)
            {
                if (index > 0)
                {
                    UserRuleList.Move(index, 0);
                }
                else
                {

                }
            }
        }

        private void StartBatchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Type == "file")
            {
                if (Files.Count == 0)
                {
                    MessageBox.Show("List of files is empty, please insert one");
                    return;
                }
                foreach (MyFile file in Files)
                {
                    if (file.NewName != null)
                    {
                        
                        try
                        {
                            string newPath = file.Path.Substring(0, file.Path.LastIndexOf('\\') + 1) + file.NewName;
                            if (file.Path == newPath)
                            {
                                continue;
                            }
                            System.IO.File.Move(file.Path, newPath);
                            file.Path = newPath;
                            file.Name = file.NewName;
                            file.Status = "Successful";
                        }
                        catch
                        {
                            file.Status = "Failed";
                        }
                    }
                }
                MessageBox.Show("Batching files has been done","Notification");
                return;
            }
            else
            {
                if (Folders.Count == 0)
                {
                    MessageBox.Show("List of folders is empty, please insert one");
                    return;
                }
                foreach (MyFolder folder in Folders)
                {
                    if (folder.NewName != null)
                    {
                        string newPath = folder.Path.Substring(0, folder.Path.LastIndexOf('\\') + 1) + folder.NewName;
                        try
                        {
                            if (folder.Path == newPath)
                            {
                                continue;
                            }
                            Directory.Move(folder.Path, newPath);
                            folder.Path = newPath;
                            folder.Name = folder.NewName;
                            folder.Status = "Successful";
                        }
                        catch
                        {
                            folder.Status = "Failed";
                        }
                    }
                }
                MessageBox.Show("Batching folders has been done", "Notification");
                return;
            }
            
        }

        private void PreviewButton_Click(object sender, RoutedEventArgs e)
        {
            Update_Preview();
        }

        private void AddByFolder_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string[] ListFiles = Directory.GetFiles($"{dialog.FileName}", "*.*", SearchOption.AllDirectories);
                foreach (string File in ListFiles)
                {
                    MyFolder MyFileInfo = new MyFolder(File);
                    if (!myHelper.isExistInList(Files, MyFileInfo))
                    {
                        Files.Add(MyFileInfo);
                    }
                }
            }
            if(Files.Count > 0)
            {
                _dropFileArea.Visibility = "Hidden";
            }
           
        }

        public void loadAllPreset()
        {
            string[] fileArray = Directory.GetFiles("Preset", "*.json");
            Presets.Clear();
            foreach (string file in fileArray)
            {
                string fileName = file.Substring(file.LastIndexOf('\\') + 1);
                fileName = fileName.Substring(0, fileName.LastIndexOf('.'));
                Presets.Add(new Preset() { PresetName = fileName, PresetPath = file });
            }
            //lvPreset.ItemsSource = Presets;
        }
        public void loadRules(string filePath)
        {
            UserRuleList.Clear();
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                List<rulesFromPreset> items = JsonConvert.DeserializeObject<List<rulesFromPreset>>(json);
                foreach (rulesFromPreset rule in items)
                {
                    for (int i = 0; i < RuleList.Count(); i++)
                    {
                        if (RuleList[i].Name == rule.name)
                        {

                            UserRuleList.Add(RuleList[i].Clone(rule._arg1, rule._arg2));
                            break;
                        }
                    }
                }
            }
            checkAllRules.IsChecked = true;
            checkAllRules_Click(null, null);
            Update_Preview();
        }
        private void SavePresetButton_Click(object sender, RoutedEventArgs e)
        {
            InputDialog inputDialog = new InputDialog("Please enter preset name:", "");
            if (inputDialog.ShowDialog() == true)
            {
                string inputPresetName=inputDialog.Answer;
                string nameWindows = "Save preset";
                if (inputPresetName == "")
                {
                    MessageBox.Show("Please enter preset name!", nameWindows);
                    return;
                }
                if (UserRuleList.Count() == 0)
                {
                    MessageBox.Show("Nothing to save!", nameWindows);
                    return;
                }
                string newPresetName = inputPresetName + ".json";
                string newPath = "Preset/" + newPresetName;
                string notification = "Preset name exists! Would you like to replace it?";
                if (System.IO.File.Exists("Preset/" + newPresetName))
                {
                    var replace = MessageBox.Show(notification, nameWindows, MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (replace == MessageBoxResult.Yes)
                    {
                        var json = JsonConvert.SerializeObject(UserRuleList, Formatting.Indented);
                        System.IO.File.WriteAllText(newPath, json);
                        //reload preset
                        for (int i = 0; i < Presets.Count(); i++)
                        {
                            if (Presets[i].PresetName == inputPresetName)
                            {
                                loadRules(Presets[i].PresetPath);
                            }
                        }
                        MessageBox.Show("Successful!", nameWindows);
                    }
                    else if (replace == MessageBoxResult.No)
                    {
                        //do nothing
                    }
                }
                else
                {
                    var json = JsonConvert.SerializeObject(UserRuleList, Formatting.Indented);
                    System.IO.File.WriteAllText(newPath, json);
                    Presets.Add(new Preset() { PresetName = inputPresetName, PresetPath = newPath });
                    MessageBox.Show("Successful!", nameWindows);
                    //lvPreset.ItemsSource = Presets;
                }

             
                //lblName.Text = inputDialog.Answer;
            }
        }

        private void loadPresetRules(object sender, RoutedEventArgs e)
        {
            
        }
        private void presetCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index=presetCombobox.SelectedIndex;
            if(index != -1)
            {
                loadRules(Presets[index].PresetPath);
            }
            presetCombobox.SelectedValue = "";
            //var presetName = ((sender as Button).Content as TextBlock).Text;

        }

        private void BrowsePresetButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Json files (*.json)|*.json";

            if (openFileDialog.ShowDialog() == true)
            {
                string[] files = openFileDialog.FileNames;
                try
                {
                    //MessageBox.Show(files[0]);
                    
                    loadRules(files[0]);
                    var fileName= System.IO.Path.GetFileName(files[0]);
                    if (System.IO.File.Exists($@"Preset\{fileName}")){
                        MessageBox.Show("Preset existed", "Load preset");
                        return;
                    }
                    File.Copy($@"{files[0]}", $@"Preset\{fileName}");
                    loadAllPreset();
                    MessageBox.Show("Preset loaded", "Load preset");
                }
                catch
                {
                    MessageBox.Show("Something went wrong!", "Load preset");
                }
            }
        }

        private void ClearRuleButton_Click(object sender, RoutedEventArgs e)
        {
            UserRuleList.Clear();
        }

        private void ImportRuleButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DirectoryInfo folder = new DirectoryInfo(@"Plugins Library\");
            if (!folder.Exists)
            {
                Directory.CreateDirectory(@"Plugins Library\");
            }
            openFileDialog.InitialDirectory = @"Plugins Library\";
            openFileDialog.Filter = "DLL files only (*.dll)|*.dll";

            string exePath = Assembly.GetExecutingAssembly().Location;
            string path = System.IO.Path.GetDirectoryName(exePath);
            path = path + @"\Plugins Library";

            if (Directory.Exists(path))
            {
                //Directory.CreateDirectory(path);
                openFileDialog.InitialDirectory = path;
                openFileDialog.Multiselect = true;

                Dictionary<string, string> state = new Dictionary<string, string>();
                if (openFileDialog.ShowDialog() == true)
                {
                    string[] files = openFileDialog.FileNames;
                    //bool check = true;
                    for (int i = 0; i < files.Length; i++)
                    {
                        FileInfo myfile = new FileInfo(files[i]);
                        string nameRule = myfile.Name;
                        int result = RuleFactory.Instance().addRuleFromDll(files[i]);
                        if (result == 1)
                        {

                            totalRules = RuleFactory.Instance().countRules();

                            state.Add(nameRule, "Successful");



                            RuleList = new List<IRule>();
                            NameRules = new BindingList<string>();
                            for (int j = 0; j < totalRules; j++)
                            {
                                RuleList.Add(RuleFactory.Instance().Create(j));
                                NameRules.Add(RuleList[j].Name);
                            }
                            ruleCombobox.ItemsSource = NameRules;

                        }
                        else if (result == 0)
                        {
                            state.Add(nameRule, "Failed");
                        }
                        else
                        {
                            state.Add(nameRule, "File exists");
                        }

                    }

                    string str = "";
                    foreach (var item in state)
                    {
                        str = str + item.Key + " - " + item.Value + '\n';
                    }
                    MessageBoxResult notify = MessageBox.Show(str, "Notify", MessageBoxButton.OK, MessageBoxImage.Information);


                }
            }
            else
            {
                Dictionary<string, string> state = new Dictionary<string, string>();

                if (openFileDialog.ShowDialog() == true)
                {
                    string[] files = openFileDialog.FileNames;
                    //bool check = true;
                    for (int i = 0; i < files.Length; i++)
                    {
                        FileInfo myfile = new FileInfo(files[i]);
                        string nameRule = myfile.Name;
                        int result = RuleFactory.Instance().addRuleFromDll(files[i]);
                        if (result == 1)
                        {

                            totalRules = RuleFactory.Instance().countRules();

                            state.Add(nameRule, "Successfull");



                            RuleList = new List<IRule>();
                            NameRules = new BindingList<string>();
                            for (int j = 0; j < totalRules; j++)
                            {
                                RuleList.Add(RuleFactory.Instance().Create(j));
                                NameRules.Add(RuleList[j].Name);
                            }
                            ruleCombobox.ItemsSource = NameRules;

                        }
                        else if (result == 0)
                        {
                            state.Add(nameRule, "Failed");
                        }
                        else
                        {
                            state.Add(nameRule, "File existed");
                        }

                    }

                    string str = "";
                    foreach (var item in state)
                    {
                        str = str + item.Key + " - " + item.Value + '\n';
                    }
                    MessageBoxResult notify = MessageBox.Show(str, "Notification", MessageBoxButton.OK, MessageBoxImage.Information);

                }
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
    //public class FileItem
    //{
    //    public string Name;
    //    public string Image;
    //    public string fileExtention;
    //    public string Path;
    //    public string newName;
    //    public string status;
    //}
    //public class FolderItem
    //{
    //    public string Name;
    //    public string Image;
    //    public string Path;
    //    public string newName;
    //    public string status;
    //}
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
