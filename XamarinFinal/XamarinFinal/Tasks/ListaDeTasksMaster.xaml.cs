using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinFinal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaDeTasksMaster : ContentPage
    {
        public ListView ListView;

        public ListaDeTasksMaster()
        {
            InitializeComponent();

            ListView = MenuItemsListView;
        }

    }
}