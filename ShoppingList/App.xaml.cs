using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ShoppingList.Views;

namespace ShoppingList
{
    public partial class App : Application
    {
        public static string SessionKey = "";
        

        public App()
        {
            InitializeComponent();

            MainPage = new ListPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
