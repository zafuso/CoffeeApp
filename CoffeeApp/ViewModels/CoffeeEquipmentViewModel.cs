using CoffeeApp.Models;
using CoffeeApp.Services;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace CoffeeApp.ViewModels
{
    public class CoffeeEquipmentViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Models.Coffee> Coffee { get; set; }
        public ObservableRangeCollection<Grouping<string, Models.Coffee>> CoffeeGroups { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<Coffee> FavoriteCommand { get; }

        public AsyncCommand<object> SelectedCommand { get; }
        public Command LoadMoreCommand { get; }
        public Command DelayLoadMoreCommand { get; }
        public Command ClearCommand { get; }
        public CoffeeEquipmentViewModel()
        {
            Title = "Coffee Equipment";
            Coffee = new ObservableRangeCollection<Models.Coffee>();
            CoffeeGroups = new ObservableRangeCollection<Grouping<string, Models.Coffee>>();

            LoadMore();

            RefreshCommand = new AsyncCommand(Refresh);
            SelectedCommand = new AsyncCommand<object>(Selected);
            FavoriteCommand = new AsyncCommand<Coffee>(Favorite);
            LoadMoreCommand = new Command(LoadMore);
            ClearCommand = new Command(Clear);
            DelayLoadMoreCommand = new Command(DelayLoadMore);
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
            set => SetProperty(ref selectedCoffee, value);
        }

        async Task Selected(object args)
        {
            var coffee = args as Coffee;
            if (coffee == null)
                return;

            SelectedCoffee = null;
            await Application.Current.MainPage.DisplayAlert("Selected", coffee.Name, "OK");
        }
        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            IsBusy = false;
        }
        void LoadMore()
        {
            if (Coffee.Count >= 20)
                return;

            var image = "https://www.yesplz.coffee/app/uploads/2020/11/emptybag-min.png";
            Coffee.Add(new Coffee { Roaster = "Yes Plz", Name = "Sip of Sunshine", Image = image });
            Coffee.Add(new Coffee { Roaster = "Yes Plz", Name = "Potent Potable", Image = image });
            Coffee.Add(new Coffee { Roaster = "Yes Plz", Name = "Potent Potable", Image = image });
            Coffee.Add(new Coffee { Roaster = "Blue Bottle", Name = "Kenya Kiambu Handege", Image = image });
            Coffee.Add(new Coffee { Roaster = "Blue Bottle", Name = "Kenya Kiambu Handege", Image = image });

            CoffeeGroups.Clear();

            CoffeeGroups.Add(new Grouping<string, Coffee>("Blue Bottle", Coffee.Where(c => c.Roaster == "Blue Bottle")));
            CoffeeGroups.Add(new Grouping<string, Coffee>("Yes Plz", Coffee.Where(c => c.Roaster == "Yes Plz")));
        }

        void DelayLoadMore()
        {
            if (Coffee.Count <= 10)
                return;

            LoadMore();
        }


        void Clear()
        {
            Coffee.Clear();
            CoffeeGroups.Clear();
        }

    }
}
