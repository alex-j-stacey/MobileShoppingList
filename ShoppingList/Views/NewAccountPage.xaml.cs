using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;
using Newtonsoft.Json;
using ShoppingList.Models;
using System.Text;

namespace ShoppingList.Views
{
    public partial class NewAccountPage : ContentPage
    {
        public NewAccountPage()
        {
            InitializeComponent();
            Title = "Create Account";
        }

        async void CreateUser_Clicked(System.Object sender, System.EventArgs e)
        {
            var data = JsonConvert.SerializeObject(new UserAccount(txtUser.Text, txtPass.Text, txtEmail.Text));
            var client = new HttpClient();
            var response = await client.PostAsync(new Uri("http://joewetzel.com/fvtc/account/createuser"), new StringContent(data, Encoding.UTF8, "application/json"));
            var accountStatus = response.Content.ReadAsStringAsync().Result;

            //user exists
            //email exists
            //complete

            //form validation
            if (accountStatus == "user exists") { await DisplayAlert("Error", "This username already exists", "Ok"); return; }
            if (accountStatus == "email exists") { await DisplayAlert("Error", "This email already exists", "Ok"); return; }
            if (!txtPass.Text.Equals(txtPass2.Text)) { await DisplayAlert("Error", "Passwords do not match", "Ok"); return; }
            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains(".")) { await DisplayAlert("Error", "Not a valid email", "Ok"); return; }

            if (accountStatus == "complete")
            {
                response = await client.PostAsync(new Uri("http://joewetzel.com/fvtc/account/createuser"), new StringContent(data, Encoding.UTF8, "application/json"));
                var SKey = response.Content.ReadAsStringAsync().Result;

                if (!string.IsNullOrEmpty(SKey))
                {
                    App.SessionKey = SKey;
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Sorry, there was an error logging in", "Ok");
                    return;
                }
            }
            else
            {
                await DisplayAlert("Error", "Sorry, there was an error creating account", "Ok");
                return;
            }
        }
    }
}
