using CoffeeApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace CoffeeApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}