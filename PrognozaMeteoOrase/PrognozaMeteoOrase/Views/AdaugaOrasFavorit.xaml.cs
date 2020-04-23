using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PrognozaMeteoOrase.Models;
using PrognozaMeteoOrase.Services;

namespace PrognozaMeteoOrase.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AdaugaOrasFavorit : ContentPage
    {
        public Oras Item { get; set; }

        public AdaugaOrasFavorit()
        {
            InitializeComponent();

            Item = new Oras
            {
                Denumire = "Item name",
                Descriere = "This is an item description."
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            var url = $"http://api.openweathermap.org/data/2.5/weather?q={Item.Denumire}&appid={ApiCaller.API_KEY}&units=metric";

            var result = await ApiCaller.Get(url);

            if (result.Successful)
            {
                MessagingCenter.Send(this, "AddItem", Item);
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Eroare inserare", "Nu a fost gasit orasul inserat", "OK");
            }
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}