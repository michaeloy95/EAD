using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitnessApp.ViewCells
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FoodViewCell : ViewCell
	{
		public FoodViewCell ()
		{
			InitializeComponent ();
		}
	}
}