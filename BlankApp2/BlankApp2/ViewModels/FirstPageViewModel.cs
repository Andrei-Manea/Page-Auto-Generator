using BlankApp2.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace BlankApp2.ViewModels
{
	public class FirstPageViewModel : ViewModelBase
	{
        public ICommand NextPage { get; set; }

        private string _favColor;
        public string FavColor
        {
            get { return _favColor; }
            set { SetProperty(ref _favColor, value); }
        }

        private string _favBand;
        public string FavBand
        {
            get { return _favBand; }
            set { SetProperty(ref _favBand, value); }
        }

        private bool _likeXamarin;
        public bool LikeXamarin
        {
            get { return _likeXamarin; }
            set { SetProperty(ref _likeXamarin, value); }
        }

        public FirstPageViewModel(INavigationService navigationMethod, IPageDialogService dialogService, IEventAggregator eventAggregator) : base(navigationMethod, dialogService, eventAggregator)
        {
            NextPage = new Command(Next_Page);
        }

        private async void Next_Page(object obj)
        {
            string[] preferences = new string[4];

            if (LikeXamarin == true)
            {
                preferences[0] = FavColor;
                preferences[0] = FavBand;
                preferences[0] = "true";
            } else
            {
                preferences[0] = FavColor;
                preferences[0] = FavBand;
                preferences[0] = "false";
            }

            await NavigationMethod.NavigateAsync(nameof(MainPage));
            EventAggregator.GetEvent<SendMessageEvent>().Publish(preferences);
        }
    }
}
