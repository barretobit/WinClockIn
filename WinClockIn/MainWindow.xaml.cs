using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WinClockIn.Models;
using WinClockIn.Services;

namespace WinClockIn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateStateLabel();
        }

        private void UpdateStateLabel()
        {
            bool isLoggedIn = StateService.IsLoggedIn(DateTime.Now);

            if (isLoggedIn)
            {
                Label_State.Content = "Logged In";
            }
            else
            {
                Label_State.Content = "Logged Off";
            }
        }

        private void Btn_Start_Click(object sender, RoutedEventArgs e)
        {
            DayRegistry? todayRegistry = DataService.GetTodayRegistry();

            if (todayRegistry == null)
            {
                // Create Today Registry
                todayRegistry = new DayRegistry() { LogIn = DateTime.Now };

                DataService.SaveOrUpdateRegistry(todayRegistry);
            }
            else
            {
                ClockService.ClockIn(todayRegistry);
            }
        }
    }
}