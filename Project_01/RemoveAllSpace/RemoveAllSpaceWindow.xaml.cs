using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace RemoveAllSpace
{
    /// <summary>
    /// Interaction logic for RemoveAllSpaceWindow.xaml
    /// </summary>
    public partial class RemoveAllSpaceWindow : UserControl
    {
        RemoveAllSpaceRule rule;
        public RemoveAllSpaceWindow(RemoveAllSpaceRule rule)
        {
            this.rule = rule;
            this.rule.Setup(null, null);
            this.rule._arg1 = "";
            this.rule._arg2 = "";
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
