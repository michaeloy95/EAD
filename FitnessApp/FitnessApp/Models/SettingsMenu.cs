using System;

namespace FitnessApp.Models
{
    public class SettingsMenu
    {
        public string Title { get; set; }

        public string IconSource { get; set; }

        public Action Action { get; set; }
    }
}
