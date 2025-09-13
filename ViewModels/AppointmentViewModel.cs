﻿using AppointmentSimulator.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AppointmentSimulator.ViewModels
{
    public partial class AppointmentViewModel : ObservableObject 
    {
        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private string _subject = string.Empty;

        [ObservableProperty]
        private DateOnly _appointmentDate;

        [ObservableProperty]
        private TimeSpan _startingTime;

        [ObservableProperty]
        private TimeSpan _endingTime;

        [RelayCommand]
        public async Task AddNewAppointment()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Subject) )
            {
                await Shell.Current.DisplayAlert("Error", "Name and Subject cannot be empty.", "OK");
                return;
            }

            if (GlobalData.Appointments.FirstOrDefault(a => a.AppointmentDate.Equals(AppointmentDate)) != null && GlobalData.Appointments.FirstOrDefault(a => a.StartingTime.Equals(StartingTime)) != null )
            {
                await Shell.Current.DisplayAlert("Error", "An appointment already exists for this schedule.", "OK");
                return;
            }

            Appointment appointment = new()
            {
                Name = Name.Trim(),
                Subject = Subject,
                AppointmentDate = AppointmentDate,
                StartingTime = StartingTime,
                EndingTime = EndingTime
            };

            GlobalData.Appointments.Add(appointment);
            await Shell.Current.DisplayAlert("Success", "Appointment added successfully.", "OK");
            await Shell.Current.GoToAsync("..");
        }


        [RelayCommand]
        public async Task DeleteAppointment()
        {
            var appointmentToDelete = GlobalData.Appointments.FirstOrDefault(a => a.AppointmentDate.Equals(AppointmentDate) && a.StartingTime.Equals(StartingTime));
            if ( appointmentToDelete != null )
            {
                bool confirm = await Shell.Current.DisplayAlert("Confirm Delete", "Are you sure you want to delete this appointment?", "Yes", "No");
                if ( confirm )
                {
                    GlobalData.Appointments.Remove(appointmentToDelete);
                    await Shell.Current.DisplayAlert("Success", "Appointment deleted successfully.", "OK");
                    await Shell.Current.GoToAsync("..");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "No appointment found for the specified date and time.", "OK");
            }
        }
    }
}
