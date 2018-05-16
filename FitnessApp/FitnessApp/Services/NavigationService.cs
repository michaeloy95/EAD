using FitnessApp.Interfaces;
using System;
using Xamarin.Forms;

namespace FitnessApp.Services
{
    public class NavigationService : INavigationService
    {
        public Page CurrentPage
        {
            get
            {
                return (Application.Current.MainPage as NavigationPage).CurrentPage;
            }
        }

        public Page CurrentMainPage
        {
            get
            {
                return App.Current.MainPage as NavigationPage;
            }
        }

        public NavigationService()
        {
        }

        public void GoBack()
        {
            this.CurrentPage.Navigation.PopAsync();
        }

        public void GoBack(int pages)
        {
            var stack = this.CurrentMainPage.Navigation.NavigationStack;
            for (int i = 0; i < pages - 1; i++)
            {
                this.CurrentMainPage.Navigation.RemovePage(stack[stack.Count - 2]);
            }
            this.GoBack();
        }

        public void NavigateTo(Type type)
        {
            this.NavigateTo(type, null);
        }

        public void NavigateTo(Type type, object[] parameters)
        {
            Page page = (Page)Activator.CreateInstance(type, parameters);

            var stack = this.CurrentMainPage.Navigation.NavigationStack;
            if (stack[stack.Count - 1].GetType() != type)
            {
                this.CurrentMainPage.Navigation.PushAsync(page, true);
            }
        }

        public void SwitchTo(Type type)
        {
            this.SwitchTo(type, null);
        }

        public void SwitchTo(Type type, object[] parameters)
        {
            Page page = (Page)Activator.CreateInstance(type, parameters);

            var stack = this.CurrentMainPage.Navigation.NavigationStack;
            if (stack[stack.Count - 1].GetType() != type)
            {
                this.CurrentMainPage.Navigation.PushAsync(page, true);
            }

            this.CurrentMainPage.Navigation.RemovePage(stack[stack.Count - 2]);
        }
    }
}
