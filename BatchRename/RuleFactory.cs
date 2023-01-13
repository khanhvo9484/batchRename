using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BatchRename
{
    internal class RuleFactory
    {
        private List<IRule> _prototypes = new List<IRule>();

        private static RuleFactory _instance = null;
        private RuleFactory()
        {
            string exePath = Assembly.GetExecutingAssembly().Location;
            string folder = Path.GetDirectoryName(exePath);
            var fis = new DirectoryInfo(folder + "\\Plugins").GetFiles("*.dll");
            foreach (var f in fis)
            {
                var assembly = Assembly.Load(File.ReadAllBytes(f.FullName));
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.IsClass && typeof(IRule).IsAssignableFrom(type))
                    {
                        IRule c = (IRule)Activator.CreateInstance(type);
                        _prototypes.Add(c);
                    }
                }
            }

        }
        public static RuleFactory Instance()
        {
            if (_instance == null)
            {
                _instance = new RuleFactory();
            }
            return _instance;
        }
        public int countRules()
        {
            return _prototypes.Count;
        }
        public IRule Create(int index)
        {
            return _prototypes[index];
        }
        public int addRuleFromDll(string filePath)
        {
            IRule newRule = null;
            FileInfo file = new FileInfo(filePath);
            int result = 1;
            try
            {
                var assembly = Assembly.Load(File.ReadAllBytes(file.FullName));
                var types = assembly.GetTypes();

                foreach (var t in types)
                {
                    if (t.IsClass && typeof(IRule).IsAssignableFrom(t))
                    {
                        newRule = (IRule)Activator.CreateInstance(t);
                    }
                }


            }
            catch (Exception)
            {
                result = 0;
                newRule = null;
            }
            if (result == 1)
            {
                string exePath = Assembly.GetExecutingAssembly().Location;
                string folder = Path.GetDirectoryName(exePath);
                FileInfo newfile = new FileInfo(filePath);
                FileInfo oldFile = new FileInfo(folder + "\\DLL\\" + newfile.Name);
                string nameFile = newfile.Name;
                string destination = folder + "\\DLL\\" + newfile.Name;
                if (oldFile.Exists == true)
                {
                    MessageBoxResult notify = MessageBox.Show($"File {nameFile} exists. Do you want replace it? ", "Notify", MessageBoxButton.YesNo, MessageBoxImage.Information);

                    if (notify == MessageBoxResult.Yes)
                    {
                        System.GC.Collect();
                        System.GC.WaitForPendingFinalizers();
                        File.Delete(folder + "\\DLL\\" + newfile.Name);
                        File.Copy(filePath, destination, true);
                        var fis = new DirectoryInfo(folder + "\\DLL").GetFiles("*.dll");

                        foreach (var f in fis)
                        {
                            if (f.FullName == destination)
                            {
                                var assembly = Assembly.Load(File.ReadAllBytes(f.FullName));
                                var types = assembly.GetTypes();

                                foreach (var type in types)
                                {
                                    if (type.IsClass && typeof(IRule).IsAssignableFrom(type))
                                    {
                                        IRule c = (IRule)Activator.CreateInstance(type);
                                        for (int i = 0; i < _prototypes.Count; i++)
                                        {
                                            if (_prototypes[i].Name == c.Name)
                                            {
                                                _prototypes.RemoveAt(i);
                                                break;
                                            }
                                        }
                                        _prototypes.Add(c);

                                    }
                                }
                            }
                        }
                        return 1;
                    }
                    else
                    {
                        return 2;
                    }
                }
                else
                {
                    File.Copy(filePath, destination, true);
                    var filesDll = new DirectoryInfo(folder + "\\Plugins").GetFiles("*.dll");
                    foreach (var f in filesDll)
                    {
                        if (f.FullName == destination)
                        {
                            var assembly = Assembly.Load(File.ReadAllBytes(f.FullName));
                            var types = assembly.GetTypes();

                            foreach (var type in types)
                            {
                                if (type.IsClass && typeof(IRule).IsAssignableFrom(type))
                                {
                                    IRule c = (IRule)Activator.CreateInstance(type);
                                    _prototypes.Add(c);
                                }
                            }
                        }
                    }
                }




            }
            return result;
        }
        }
}
