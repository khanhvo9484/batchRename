using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AddPrefix
{
    /// <summary>
    /// Interaction logic for AddPrefixWindow.xaml
    /// </summary>
    public partial class AddPrefixWindow : UserControl
    {
        AddPrefixRule rule;
        public string _Prefix { get; set; } = "";
        public AddPrefixWindow(AddPrefixRule rule)
        {
            this.rule = rule;
            _Prefix = this.rule._Prefix;
            this.rule._arg1 = _Prefix;
            this.rule._arg2 = "";

            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this;

        }
        
        private void prefixInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void Apply_button_click(object sender, RoutedEventArgs e)
        {
            if (prefixInput.Text != "")
            {
                Dictionary<string, string> DictSetup = new Dictionary<string, string>();
                List<string> ListSetup = new List<string>();
                this.rule._Prefix = prefixInput.Text;
                DictSetup.Add("Prefix", this.rule._Prefix);
                this.rule.Setup(DictSetup, null);
            }
           

        }
    }
}
