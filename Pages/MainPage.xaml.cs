using AppointmentSimulator.Models;
using System.Threading.Tasks;

namespace AppointmentSimulator.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            AppointmentsCollectionView.ItemsSource = GlobalData.Appointments;
        }


        private async void OnAddNewAppointment(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"/{nameof(AddNewAppointmentPage)}");
        }
    }
}
