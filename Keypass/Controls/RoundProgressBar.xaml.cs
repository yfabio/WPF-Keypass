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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Keypass.Controls
{
    /// <summary>
    /// Interaction logic for RoundProgressBar.xaml
    /// </summary>
    public partial class RoundProgressBar : UserControl
    {
        public RoundProgressBar()
        {
            InitializeComponent();

            //Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline),
            //    new FrameworkPropertyMetadata { DefaultValue = 10 });
        }
    }
}
