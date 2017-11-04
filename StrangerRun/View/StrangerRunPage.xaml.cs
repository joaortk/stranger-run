using System;
using CocosSharp;
using Xamarin.Forms;

namespace StrangerRun
{
    public partial class StrangerRunPage : ContentPage
    {
        public StrangerRunPage()
        {
            InitializeComponent();
            mapView.DesignResolution = new Size(800, 600);
            mapView.ViewCreated = LoadGame;

        }

        void LoadGame(object sender, EventArgs e)
        {
            var nativeGameView = sender as CCGameView;
            if (nativeGameView != null)
            {
#if __IOS__
                nativeGameView.MultipleTouchEnabled = true;
#endif
                var mapScene = new CCScene(nativeGameView);
                //var mapLayer = new MapLayer(selectedBooth);
                //mapScene.AddLayer(mapLayer);
                nativeGameView.RunWithScene(mapScene);

                nativeGameView.Stats.Enabled = true;

            }

        }
    }
}
