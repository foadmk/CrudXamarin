using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XamarinFinal
{
	public partial class App : Application
	{
        public MasterDetailPage MasterDetailPage { get; set; }

        private RestAPI myRestAPI;
        
        public RestAPI getAPI()
        {
            if(myRestAPI == null) myRestAPI = new RestAPI();
            return myRestAPI;
        }

        public App ()
		{
			InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
		}

        public void GoToMain()
        {
            if (getAPI().isLogedIn())
            {
                MasterDetailPage = new ListaDeTasks();
                MainPage.Navigation.PushModalAsync(MasterDetailPage);
            }
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
