using Plugin.Media;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace FitnessApp.ViewModels.Profile
{
    public class EditProfileViewModel : BaseViewModel
    { 
        private string profileImageSource = "";
        public string ProfileImageSource
        {
            get { return this.profileImageSource; }
            set { this.SetProperty<string>(ref this.profileImageSource, value); }
        }

        private string nameText = "";
        public string NameText
        {
            get { return this.nameText; }
            set { this.SetProperty<string>(ref this.nameText, value); }
        }

        private DateTime birthday;
        public DateTime Birthday
        {
            get { return this.birthday; }
            set { this.SetProperty<DateTime>(ref this.birthday, value); }
        }

        private string birthdayText = "";
        public string BirthdayText
        {
            get { return this.birthdayText; }
            set { this.SetProperty<string>(ref this.birthdayText, value); }
        }

        private string emailText = "";
        public string EmailText
        {
            get { return this.emailText; }
            set { this.SetProperty<string>(ref this.emailText, value); }
        }

        private string heightText = "";
        public string HeightText
        {
            get { return this.heightText; }
            set { this.SetProperty<string>(ref this.heightText, value); }
        }

        private string weightText = "";
        public string WeightText
        {
            get { return this.weightText; }
            set { this.SetProperty<string>(ref this.weightText, value); }
        }

        private bool isUpdated = false;
        public bool IsUpdated
        {
            get { return this.isUpdated; }
            set { this.SetProperty<bool>(ref this.isUpdated, value); }
        }

        public ICommand DoneCommand { get; private set; }

        public ICommand ChangeProfilePhotoCommand { get; private set; }

        public EditProfileViewModel()
        {
            this.DoneCommand = new Command(this.UpdateProfile);
            this.ChangeProfilePhotoCommand = new Command(this.ChangeProfilePhoto);

            this.ProfileImageSource = this.User.ImageSource;
            this.NameText = this.User.Name;
            this.EmailText = this.User.Email;
            this.Birthday = this.User.Birthdate;
            this.BirthdayText = this.Birthday.ToString("dd'/'MM'/'yyyy");
            this.HeightText = this.User.Height.ToString();
            this.WeightText = this.User.Weight.ToString();
        }

        private void UpdateProfile()
        {
            if (this.IsUpdated)
            {
                this.User.Name = this.NameText;
                this.User.Birthdate = this.Birthday;
                this.User.Email = this.EmailText;
                this.User.Height = double.Parse(this.HeightText);
                this.User.Weight = double.Parse(this.WeightText);

                this.User.SaveUserDetails();
            }

            this.NavigationService.GoBack();
        }

        private async void ChangeProfilePhoto()
        {
            if (this.IsBusy)
                return;
            this.IsBusy = true;

            var media = CrossMedia.Current;
            var file = await media.PickPhotoAsync();

            if (file == null)
            {
                this.IsBusy = false;
                return;
            }

            this.IsUpdated = true;
            this.ProfileImageSource = file.Path;

            this.IsBusy = false;
        }
    }
}
