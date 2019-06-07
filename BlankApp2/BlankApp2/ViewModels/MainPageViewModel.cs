using BlankApp2.Views;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace BlankApp2.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public ICommand SendClicked { get; set; }

        private string _command;
        public string Command
        {
            get { return _command; }
            set { SetProperty(ref _command, value); }
        }
        string ColorPref;
        string BandPref;
        string BoolPref;

        public MainPageViewModel(INavigationService navigationMethod, IPageDialogService dialogService, IEventAggregator eventAggregator) : base(navigationMethod, dialogService, eventAggregator)
        {
            SendClicked = new Command(Send_Clicked);
            eventAggregator.GetEvent<SendMessageEvent>().Subscribe(MessageReceived);
        }

        private void MessageReceived(string[] preferences)
        {
            ColorPref = preferences[0];
            BandPref = preferences[1];
            BoolPref = preferences[2];
        }

        private async void Send_Clicked()
        {
            string[] parsedCmd = Command.Split(',');

            string[] allRequests = new string[parsedCmd.Length];
            int j = 0;
            for (int i = 2; i < parsedCmd.Length; i++)
            {
                allRequests[j] = parsedCmd[i];
                j++;
            }

            string[] greeting = parsedCmd[0].Split(' ');
            string name = greeting[1];

            string[] layoutType = parsedCmd[1].Split(' ');
            string layout = layoutType[1];


            
            List<View> views = new List<View>();
            foreach (string i in allRequests)
            {
                string[] request = new string[3];
                if (i == null)
                    continue;

                request = i.Split(' ');

                if (request[0].Equals("Entry"))
                {
                    if (request[1].Equals("Pref"))
                    {
                        var entry = new Entry { Text = BandPref };
                        views.Add(entry);
                    }
                    else
                    {
                        var entry = new Entry { Text = request[1] };
                        views.Add(entry);
                    }

                }
                else if (request[0].Equals("Label"))
                {
                    if (request[1].Equals("Pref"))
                    {
                        var label = new Label { Text = BandPref };
                        views.Add(label);
                    }
                    else
                    {
                        var label = new Label { Text = request[1] };
                        views.Add(label);
                    }

                }
                else if (request[0].Equals("Box"))
                {
                    if (request[1].Equals("Pref"))
                    {
                        var box = new BoxView { Color = Color.FromHex(ColorPref) };
                        views.Add(box);
                    }
                    else
                    {
                        var box = new BoxView { Color = Color.FromHex(request[1]) };
                        views.Add(box);
                    }

                }
                else if (request[0].Equals("DataPicker"))
                {
                    if (request[1].Equals("Pref"))
                    {
                        var datePicker = new DatePicker
                        {
                            MinimumDate = new DateTime(2018, 1, 1),
                            MaximumDate = new DateTime(2018, 12, 31),
                            Date = new DateTime(2018, 6, 21)
                        };
                        views.Add(datePicker);
                    }
                    else
                    {
                        var datePicker = new DatePicker
                        {
                            MinimumDate = new DateTime(2018, 1, 1),
                            MaximumDate = new DateTime(2018, 12, 31),
                            Date = new DateTime(2018, 6, 21)
                        };
                        views.Add(datePicker);
                    }

                }
                else if (request[0].Equals("Switch"))
                {
                    if (request[1].Equals("Pref"))
                    {
                        if (BoolPref == "true")
                        {
                            var switch1 = new Switch { IsToggled = true };
                            views.Add(switch1);
                        }
                        else
                        {
                            var switch1 = new Switch { IsToggled = false };
                            views.Add(switch1);
                        }
                    }
                    else
                    {
                        if (request[2] == "true")
                        {
                            var switch1 = new Switch { IsToggled = true };
                            views.Add(switch1);
                        }
                        else
                        {
                            var switch1 = new Switch { IsToggled = false };
                            views.Add(switch1);
                        }
                    }

                }
            }

            View pageContent = new Label();
            switch (layout)
            {
                case "Stack":
                     pageContent = new StackLayout();
                    foreach (View v in views) {
                        (pageContent as StackLayout).Children.Add(v);
                    }
                    break;
                default:
                    break;

            }

            await App.Nav.PushAsync(new ContentPage { Content = pageContent });

        }
    }
}
