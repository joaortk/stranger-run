using System;
using CocosSharp;
using StrangerRun.Game;
using Xamarin.Forms;

namespace StrangerRun
{
    public partial class StrangerRunPage : ContentPage
    {
        private BackgroundLayer bikeRunLayer;

        public StrangerRunPage()
        {
            InitializeComponent();
            mapView.ViewCreated = LoadGame;

        }

        void LoadGame(object sender, EventArgs e)
        {
            var nativeGameView = sender as CCGameView;

            if (nativeGameView != null)
            {
                StrangerController.Initialize(nativeGameView);

            }

        }
    }
}
