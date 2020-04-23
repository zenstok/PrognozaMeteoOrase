using System;

using PrognozaMeteoOrase.Models;

namespace PrognozaMeteoOrase.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Oras Item { get; set; }
        public ItemDetailViewModel(Oras item = null)
        {
            Title = item?.Denumire;
            Item = item;
        }
    }
}
