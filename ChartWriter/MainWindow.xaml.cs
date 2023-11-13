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
using System.Windows.Threading;

namespace ChartWriter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DispatcherTimer SamplingTimer { get; set; }

        public Random MVGenerator { get; set; }

        public const double cMinMV = 0.0;
        public const double cMaxMV = 100.0;

        public List<double> MVList { get; set; }
        public MainWindow()
        {
            InitializeComponent();

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SamplingTimer = new DispatcherTimer { IsEnabled=false, Interval=new TimeSpan(0,0,0,0,500)};
            MVGenerator = new Random();
            MVList = new List<double>();
        //slider value wir zum intervall davon später
        }

        private void msValueChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lbl_Slider_ms.Content =  (int)e.NewValue+"ms";
        }

        private void valueChangeEvent(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lbl_slider_value.Content = (int)e.NewValue;
            ProgressbarValue.Value = (double)e.NewValue;
        }

        private void btn_startStop_Click(object sender, RoutedEventArgs e)
        {
            //button beschriftung noch ändern
            SamplingTimer.IsEnabled = !SamplingTimer.IsEnabled;
            SamplingTimer.Tick += SamplingTimer_Tick;
        }

        private void SamplingTimer_Tick(object? sender, EventArgs e)
        {
            //Checkbox abfragen ob zufallszahl oder value
            double newMV = MVGenerator.NextDouble()*(cMaxMV-cMinMV)+cMinMV;
            MVList.Add(newMV);

            Polyline plChart = new Polyline { Stroke = new SolidColorBrush(Colors.Yellow),
                Points = pc };




            if (MVList.Count == 300)
            {
                MVList.RemoveAt(0);
            }
        }
    }
}
