using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PrognozaMeteoOrase.Models;
using PrognozaMeteoOrase.Services;
using Newtonsoft.Json;
using PrognozaMeteoOrase.ViewModels;

namespace PrognozaMeteoOrase.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VremeOrasFavorit : ContentPage
    {
        ItemDetailViewModel viewModel;

        public VremeOrasFavorit(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
            PreiaDateVreme(viewModel.Item.Denumire);
        }

        private async void PreiaDateVreme(string location)
        {
            var url = $"http://api.openweathermap.org/data/2.5/weather?q={location}&appid={ApiCaller.API_KEY}&units=metric";

            var result = await ApiCaller.Get(url);

            if (result.Successful)
            {
                try
                {
                    var weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(result.Response);
                    descriptionTxt.Text = weatherInfo.weather[0].description.ToUpper();
                    iconImg.Source = $"w{weatherInfo.weather[0].icon}";
                    cityTxt.Text = weatherInfo.name.ToUpper();
                    temperatureTxt.Text = weatherInfo.main.temp.ToString("0");
                    humidityTxt.Text = $"{weatherInfo.main.humidity}%";
                    pressureTxt.Text = $"{weatherInfo.main.pressure} hpa";
                    windTxt.Text = $"{weatherInfo.wind.speed} m/s";
                    cloudinessTxt.Text = $"{weatherInfo.clouds.all}%";

                    var dt = new DateTime().ToUniversalTime().AddSeconds(weatherInfo.dt);
                    dateTxt.Text = dt.ToString("dddd, MMM dd").ToUpper();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Weather Info", ex.Message, "OK");
                }
            }
            else
            {
                await DisplayAlert("Weather Info", "No weather information found", "OK");
            }
        }

        public VremeOrasFavorit()
        {
            InitializeComponent();

            var item = new Oras
            {
                Denumire = "Item 1",
                Descriere = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }

        private void Prognoza_Clicked(object sender, EventArgs e)
        {

        }

        private async void Stergere_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Stergere oras", "Esti sigur ca vrei sa stergi acest oras?", "M-am razgandit", "Da");
        }
    }
}