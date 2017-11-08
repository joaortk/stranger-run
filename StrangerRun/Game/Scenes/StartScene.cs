using System;
using System.Collections.Generic;
using CocosSharp;
using StrangerRun.Game;

namespace StrangerRun.Scenes
{
    public class StartScene : CCScene
    {
        CCLayer layer;

        public StartScene(CCGameView gameView) : base(gameView)
        {
            layer = new CCLayer();

            AddLayer(layer);

            CreateText();

            CreateTouchListener();
        }

        private void CreateText()
        {
            var label = new CCLabel("Toque para iniciar", "Arial", 18, CCLabelFormat.SystemFont);
            label.Position = layer.ContentSize.Center;
            label.Color = CCColor3B.White;

            layer.AddChild(label);
        }

        private void CreateTouchListener()
        {
            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesBegan = HandleTouchesBegan;
            layer.AddEventListener(touchListener);
        }

        private void HandleTouchesBegan(List<CCTouch> arg1, CCEvent arg2)
        {
            var newScene = new StreetScene(StrangerController.GameView);
            StrangerController.GoToScene(newScene);
        }
    }
}

