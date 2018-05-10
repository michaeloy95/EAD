using FitnessApp.Data;
using FitnessApp.Interfaces;
using FitnessApp.Models;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FitnessApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public User User => App.User;

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public ILocalDatabase<Models.Meal> LocalDatabaseMeal
        {
            get
            {
                return App.LocalDatabaseMeal;
            }
        }

        public ILocalDatabase<Models.Food> LocalDatabaseFood
        {
            get
            {
                return App.LocalDatabaseFood;
            }
        }

        public INavigationService NavigationService
        {
            get
            {
                return App.NavigationService;
            }
        }

        public INotificationManager NotificationManager
        {
            get
            {
                return ServiceLocator.Current.GetInstance<INotificationManager>();
            }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
