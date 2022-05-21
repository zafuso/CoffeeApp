using CoffeeApp.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CoffeeApp.ViewModels
{
    public class CoffeeEquipmentViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Models.Coffee> Coffee { get; set; }
        public ObservableRangeCollection<Grouping<string, Models.Coffee>> CoffeeGroups { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<Coffee> FavoriteCommand { get; }
        public CoffeeEquipmentViewModel()
        {
            Title = "Coffee Equipment";
            Coffee = new ObservableRangeCollection<Models.Coffee>();
            CoffeeGroups = new ObservableRangeCollection<Grouping<string, Models.Coffee>>();

            var image = "https://www.yesplz.coffee/app/uploads/2020/11/emptybag-min.png";

            Coffee.Add(new Models.Coffee { Roaster = "Douwe Egberts", Name = "Pilske", Image = image });
            Coffee.Add(new Models.Coffee { Roaster = "Douwe Egberts", Name = "Black Gold", Image = image });
            Coffee.Add(new Models.Coffee { Roaster = "Yes Plz", Name = "Kenya Kiambu", Image = image });
            Coffee.Add(new Models.Coffee { Roaster = "Yes Plz", Name = "Kenya Kiambu", Image = image });
            Coffee.Add(new Models.Coffee { Roaster = "Yes Plz", Name = "Kenya Kiambu", Image = image });
            Coffee.Add(new Models.Coffee { Roaster = "Yes Plz", Name = "Kenya Kiambu", Image = image });
            Coffee.Add(new Models.Coffee { Roaster = "Yes Plz", Name = "Kenya Kiambu", Image = image });
            Coffee.Add(new Models.Coffee { Roaster = "Yes Plz", Name = "Kenya Kiambu", Image = image });

            CoffeeGroups.Add(new Grouping<string, Coffee>("Yes Plz", new[] { Coffee[2] }));
            CoffeeGroups.Add(new Grouping<string, Coffee>("Douwe Egberts", Coffee.Take(2)));

            RefreshCommand = new AsyncCommand(Refresh);
            FavoriteCommand = new AsyncCommand<Coffee>(Favorite);
        }

        async Task Favorite(Coffee coffee)
        {
            if (coffee == null)
                return;
            await Application.Current.MainPage.DisplayAlert("Favorite", coffee.Name, "OK");
        }

        Coffee previouslySelected;
        Coffee selectedCoffee;
        public Coffee SelectedCoffee
        {
            get => selectedCoffee;
            set
            {
                if (value != null)
                {
                    Application.Current.MainPage.DisplayAlert("Selected", value.Name, "OK");
                    previouslySelected = value;
                    value = null;
                }

                selectedCoffee = value;
                OnPropertyChanged();
            }
        }
        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            IsBusy = false;
        }
    }
}
