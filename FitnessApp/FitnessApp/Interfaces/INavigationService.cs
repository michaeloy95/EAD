using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FitnessApp.Interfaces
{
    public interface INavigationService
    {
        Page CurrentPage { get; }

        Page CurrentMainPage { get; }

        void GoBack();

        void NavigateTo(Type type);

        void NavigateTo(Type type, object[] parameters);

        void SwitchTo(Type type);

        void SwitchTo(Type type, object[] parameters);
    }
}
