﻿using System;
using System.Collections.Generic;
using CocosSharp;
using StrangerRun.Scenes;

namespace StrangerRun.Game
{
    public class StrangerController
    {
        public static CCGameView GameView
        {
            get;
            private set;
        }

        public static void Initialize(CCGameView gameView)
        {
            GameView = gameView;

            var contentSearchPaths = new List<string>() { "Fonts", "Sounds" };

#if __IOS__
            contentSearchPaths.Add("Sounds/iOS/");

#else // android
            contentSearchPaths.Add("Sounds/Android/");


#endif

            contentSearchPaths.Add("Images");
            GameView.ContentManager.SearchPaths = contentSearchPaths;


            // We use a lower-resolution display to get a pixellated appearance
            int width = 384;
            int height = 208;
            GameView.DesignResolution = new CCSizeI(width, height);
            GameView.ResolutionPolicy = CCViewResolutionPolicy.ShowAll;

#if DEBUG
            GameView.Stats.Enabled = true;
#endif

            InitializeAudio();

            var scene = new StartScene(GameView);
            GameView.RunWithScene(scene);
        }

        private static void InitializeAudio()
        {
            //CCAudioEngine.SharedEngine.PlayBackgroundMusic("FruityFallsSong");
        }

        public static void GoToScene(CCScene scene)
        {
            GameView.Director.ReplaceScene(scene);
        }
    }
}
