using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinFinal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaDeTasks : MasterDetailPage
    {
        public TasksVM vm;
        public ListaDeTasks()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {            
            base.OnAppearing();
            vm = new TasksVM((App)Application.Current);
            vm.ShowDetail(new TaskModel());

            MasterPage.BindingContext = vm;
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;

            vm._atualizaLista();

            IsPresented = true;
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as TaskModel;
            if (item == null)
                return;
            vm.ShowDetail(item);
            MasterPage.ListView.SelectedItem = null;
        }
    }
}