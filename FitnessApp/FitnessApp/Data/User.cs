using System;
using Xamarin.Forms;

namespace FitnessApp.Data
{
    public class User
    {
        private const string LoggedInKey = "LoggedIn";

        private const string IDKey = "ID";

        private const string UsernameKey = "Username";

        private const string PasswordKey = "Password";

        private const string NameKey = "Name";

        private const string EmailKey = "Email";

        private const string BirthdateKey = "Birthdate";

        private const string HeightKey = "Height";

        private const string WeightKey = "Weight";

        private const string BMIKey = "BMI";

        private const string RDIKey = "RDI";

        private const string ImageSourceKey = "ImageSource";

        public bool HasLoggedIn
        {
            get;
            set;
        }

        public string ID
        {
            get;
            set;
        }

        public string Username
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public DateTime Birthdate
        {
            get;
            set;
        }

        public double Height
        {
            get;
            set;
        }

        public double Weight
        {
            get;
            set;
        }

        public int BMI
        {
            get;
            set;
        }

        public int RDI
        {
            get;
            set;
        }

        public string ImageSource
        {
            get;
            set;
        }

        public bool FoodIsLoaded
        {
            get;
            set;
        }

        public bool MealIsLoaded
        {
            get;
            set;
        }

        public User()
        {
            this.Initialise();
        }

        public User(User user)
        {
            this.HasLoggedIn = true;

            this.ID = user.ID;

            this.Username = user.Username;

            this.Password = user.Password;

            this.Email = user.Email;

            this.Name = user.Name;

            this.Birthdate = user.Birthdate;

            this.Height = user.Height;

            this.Weight = user.Weight;

            this.BMI = user.BMI;

            this.RDI = user.RDI;

            this.ImageSource = user.ImageSource;

            this.FoodIsLoaded = user.FoodIsLoaded;

            this.MealIsLoaded = user.MealIsLoaded;
        }

        public void Initialise()
        {
            this.HasLoggedIn = Application.Current.Properties.ContainsKey(LoggedInKey)
                                    ? (bool)Application.Current.Properties[LoggedInKey]
                                    : false;

            if (!this.HasLoggedIn)
            {
                this.RemoveUser();
                return;
            }

            this.ID = Application.Current.Properties.ContainsKey(IDKey)
                                    ? (string)Application.Current.Properties[IDKey]
                                    : string.Empty;

            this.Username = Application.Current.Properties.ContainsKey(UsernameKey)
                                    ? (string)Application.Current.Properties[UsernameKey]
                                    : string.Empty;

            this.Password = Application.Current.Properties.ContainsKey(PasswordKey)
                                    ? (string)Application.Current.Properties[PasswordKey]
                                    : string.Empty;

            this.Email = Application.Current.Properties.ContainsKey(EmailKey)
                                    ? (string)Application.Current.Properties[EmailKey]
                                    : string.Empty;

            this.Name = Application.Current.Properties.ContainsKey(NameKey)
                                    ? (string)Application.Current.Properties[NameKey]
                                    : string.Empty;

            this.Birthdate = Application.Current.Properties.ContainsKey(BirthdateKey)
                                    ? (DateTime)Application.Current.Properties[BirthdateKey]
                                    : new DateTime();

            this.Height = Application.Current.Properties.ContainsKey(HeightKey)
                                    ? (double)Application.Current.Properties[HeightKey]
                                    : double.NaN;

            this.Weight = Application.Current.Properties.ContainsKey(WeightKey)
                                    ? (double)Application.Current.Properties[WeightKey]
                                    : double.NaN;

            this.BMI = Application.Current.Properties.ContainsKey(BMIKey)
                                    ? (int)Application.Current.Properties[BMIKey]
                                    : 0;

            this.RDI = Application.Current.Properties.ContainsKey(RDIKey)
                                    ? (int)Application.Current.Properties[RDIKey]
                                    : 0;

            this.ImageSource = Application.Current.Properties.ContainsKey(ImageSourceKey)
                                    ? (string)Application.Current.Properties[ImageSourceKey]
                                    : "";

            this.FoodIsLoaded = false;

            this.MealIsLoaded = false;
        }

        public void SetLogin(bool logged)
        {
            this.HasLoggedIn = logged;
            Application.Current.Properties[LoggedInKey] = this.HasLoggedIn;

            if (!logged)
            {
                this.RemoveUser();
            }
        }

        public void SaveUserDetails()
        {
            Application.Current.Properties[IDKey] = this.ID;
            
            Application.Current.Properties[UsernameKey] = this.Username;

            Application.Current.Properties[PasswordKey] = this.Password;

            Application.Current.Properties[EmailKey] = this.Email;

            Application.Current.Properties[NameKey] = this.Name;

            Application.Current.Properties[BirthdateKey] = this.Birthdate;

            Application.Current.Properties[HeightKey] = this.Height;

            Application.Current.Properties[WeightKey] = this.Weight;

            Application.Current.Properties[RDIKey] = this.RDI;
            
            Application.Current.Properties[BMIKey] = this.BMI;

            Application.Current.Properties[ImageSourceKey] = this.ImageSource;
        }

        private void RemoveUser()
        {
            this.ID = string.Empty;
            if (Application.Current.Properties.ContainsKey(IDKey))
            {
                Application.Current.Properties.Remove(IDKey);
            }

            this.Username = string.Empty;
            if (Application.Current.Properties.ContainsKey(UsernameKey))
            {
                Application.Current.Properties.Remove(UsernameKey);
            }

            this.Password = string.Empty;
            if (Application.Current.Properties.ContainsKey(PasswordKey))
            {
                Application.Current.Properties.Remove(PasswordKey);
            }

            this.Email = string.Empty;
            if (Application.Current.Properties.ContainsKey(EmailKey))
            {
                Application.Current.Properties.Remove(EmailKey);
            }

            this.Name = string.Empty;
            if (Application.Current.Properties.ContainsKey(NameKey))
            {
                Application.Current.Properties.Remove(NameKey);
            }

            this.Birthdate = new DateTime();
            if (Application.Current.Properties.ContainsKey(BirthdateKey))
            {
                Application.Current.Properties.Remove(BirthdateKey);
            }

            this.Height = double.NaN;
            if (Application.Current.Properties.ContainsKey(HeightKey))
            {
                Application.Current.Properties.Remove(HeightKey);
            }

            this.Weight = double.NaN;
            if (Application.Current.Properties.ContainsKey(WeightKey))
            {
                Application.Current.Properties.Remove(WeightKey);
            }

            this.RDI = 0;
            if (Application.Current.Properties.ContainsKey(RDIKey))
            {
                Application.Current.Properties.Remove(RDIKey);
            }

            this.BMI = 0;
            if (Application.Current.Properties.ContainsKey(BMIKey))
            {
                Application.Current.Properties.Remove(BMIKey);
            }

            this.ImageSource = "";
            if (Application.Current.Properties.ContainsKey(ImageSourceKey))
            {
                Application.Current.Properties.Remove(ImageSourceKey);
            }

            this.FoodIsLoaded = false;

            this.MealIsLoaded = false;
        }
    }
}
