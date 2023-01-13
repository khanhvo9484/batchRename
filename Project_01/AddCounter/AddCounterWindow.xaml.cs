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
using System.ComponentModel;

namespace AddCounter
{
    /// <summary>
    /// Interaction logic for AddCounterWindow.xaml
    /// </summary>
    public partial class AddCounterWindow : UserControl, INotifyPropertyChanged
    {
        AddCounterRule rule;
        public bool _pressApply { get; set; } = false;
        public string _start { get; set; } = "";
        public string _step { get; set; } = "";
        //public string _ka { get; set; } = "hahahka";


        public static readonly RoutedEvent ClickApplyEvent = EventManager.RegisterRoutedEvent(
        "ClickAplly", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(AddCounterWindow));

        public event RoutedEventHandler ClickAplly;

        public event PropertyChangedEventHandler PropertyChanged;
        public AddCounterWindow(AddCounterRule rule)
        {
            this.rule = rule;
            //this._pressApply = false;
            //this.DataContext = this;
            var builder1 = new StringBuilder();
            _start = builder1.Append(this.rule._start).ToString();

            var builder2 = new StringBuilder();
            _step = builder2.Append(this.rule._step).ToString();
            this.rule._arg1 = _start;
            this.rule._arg2 = _step;
            InitializeComponent();
        }
        private void Apply_button_click(object sender, RoutedEventArgs e)
        {
            if (ClickAplly != null)
            {
                ClickAplly(this, e);
            }
            Dictionary<string, string> DictSetup = new Dictionary<string, string>();
            List<string> ListSetup = new List<string>();


            if (Start_input.Text != "" && Step_input.Text != "")
            {
                this.rule._start = int.Parse(Start_input.Text);
                DictSetup.Add("start", Start_input.Text);
                this.rule._step = int.Parse(Step_input.Text);
                DictSetup.Add("step", Step_input.Text);
                this.rule.Setup(DictSetup, null);

            }
            this._pressApply = true;

            status.Source = new BitmapImage(new Uri(@"./Icons/Tick.png", UriKind.Relative));
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //this.DataContext = rule;
            this.DataContext = this;
            if (Step_input.Text == "0")
            {
                status.Source = new BitmapImage(new Uri(@"./Icons/Cross.png", UriKind.Relative));

            }
        }
        private void ValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        //this.rule.setup(DictSetup, null);

        private void start_text_change(object sender, TextChangedEventArgs e)
        {
            if (Start_input.Text != "")
            {
                if (int.Parse(Start_input.Text) != this.rule._start)
                {
                    status.Source = new BitmapImage(new Uri(@"./Icons/Cross.png", UriKind.Relative));

                }
                else
                {
                    status.Source = new BitmapImage(new Uri(@"./Icons/Tick.png", UriKind.Relative));

                }
            }

        }
        private void step_text_change(object sender, TextChangedEventArgs e)
        {

            if (Step_input.Text != "" && Step_input.Text != "0")
            {
                if (int.Parse(Step_input.Text) != this.rule._step)
                {
                    status.Source = new BitmapImage(new Uri(@"./Icons/Cross.png", UriKind.Relative));

                }
                else
                {
                    status.Source = new BitmapImage(new Uri(@"./Icons/Tick.png", UriKind.Relative));

                }
            }

        }
    }
}
