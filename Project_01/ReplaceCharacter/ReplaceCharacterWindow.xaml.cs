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

namespace ReplaceCharacter
{
    /// <summary>
    /// Interaction logic for ReplaceCharacterWindow.xaml
    /// </summary>
    public partial class ReplaceCharacterWindow : UserControl
    {
        ReplaceCharacterRule rule;
        public string _source { get; set; } = "";
        public string _target { get; set; } = "";


        public ReplaceCharacterWindow(ReplaceCharacterRule rule)
        {
            this.rule = rule;
            if (this.rule._ArrChar == null)
            {
                _source = "";

            }
            else
            {
                _source = this.rule._ArrChar[0];
            }
            _target = this.rule._TargetChar;
            this.rule._arg1 = _source;
            this.rule._arg2 = _target;
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this;
            if (source_input.Text == "" || target_input.Text == "")
            {
               

            }
        }
        private void Apply_button_click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> DictSetup = new Dictionary<string, string>();
            List<string> ArrChar = new List<string>();
            if (source_input.Text != "" && target_input.Text != "")
            {
                ArrChar.Add(source_input.Text);
                this.rule._ArrChar = ArrChar;
                this.rule._TargetChar = target_input.Text;
                DictSetup.Add("TargetChar", target_input.Text);
                this.rule.Setup(DictSetup, ArrChar);
               
            }
            NotifyText.Text = "Added";



        }

        private void source_input_changed(object sender, TextChangedEventArgs e)
        {

            NotifyText.Text = "";
        }

        private void target_input_changed(object sender, TextChangedEventArgs e)
        {
            NotifyText.Text = "";
        }

        private void ValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[\/:*?""<>|\\]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
