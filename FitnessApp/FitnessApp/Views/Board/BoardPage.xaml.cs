using FitnessApp.ViewModels.Board;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitnessApp.Views.Board
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BoardPage : ContentPage
    {
        public BoardViewModel ViewModel;

        public BoardPage()
        {
            InitializeComponent();

            this.ViewModel = new BoardViewModel()
            {
                Title = this.Title
            };
            this.BindingContext = this.ViewModel;
        }
    }
}