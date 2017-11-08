using System;
using System.Collections.Generic;
using CocosSharp;
using StrangerRun.Entities;
using StrangerRun.Game;

namespace StrangerRun.Scenes
{
    public class StreetScene : CCScene
    {
        BikerEntity biker;
        List<VanEntity> vanList = new List<VanEntity>();
        BackgroundLayer backgroundLayer;
        CCLayer gameplayLayer;
        bool hasGameEnded;
        CCLayer hudLayer;


        public StreetScene(CCGameView gameView) : base(gameView)
        {
            CreateLayers();

            CreateBiker();

            CreateVan();

            CreateTouchListener();

            Schedule(Activity);

        }

        private void CreateVan()
        {
            var van = new VanEntity();
            van.PositionX = gameplayLayer.ContentSize.Width + 100;
            van.PositionY = gameplayLayer.ContentSize.Height / 2;
            gameplayLayer.AddChild(van);
            vanList.Add(van);
        }

        private void CreateBiker()
        {
            biker = new BikerEntity();
            biker.PositionY = gameplayLayer.ContentSize.Height / 2;
            biker.PositionX = gameplayLayer.ContentSize.Width / 2;
            biker.AnchorPoint = CCPoint.AnchorMiddleBottom;
            biker.LimitMovement(gameplayLayer.ContentSize.Height / 4, (gameplayLayer.ContentSize.Height / 5) * 3);
            gameplayLayer.AddChild(biker);
        }

        private void CreateLayers()
        {
            backgroundLayer = new BackgroundLayer();
            AddLayer(backgroundLayer);

            gameplayLayer = new CCLayer();
            AddLayer(gameplayLayer);

            hudLayer = new CCLayer();
            AddLayer(hudLayer);

        }

        private void CreateTouchListener()
        {
            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesBegan = HandleTouchesBegan;
            gameplayLayer.AddEventListener(touchListener);
        }

        private void HandleTouchesBegan(List<CCTouch> arg1, CCEvent arg2)
        {
            if (!hasGameEnded)
            {
                foreach (var van in vanList)
                {
                    if (van.Children[0].BoundingBoxTransformedToWorld.ContainsPoint(arg1[0].StartLocation))
                    {
                        van.Thrown = true;
                        return;
                    }
                }
                var locationOnScreen = arg1[0].Location;
                biker.HandleInput(locationOnScreen);
            }
            else
            {
                var newScene = new StartScene(StrangerController.GameView);
                StrangerController.GoToScene(newScene);
            }
        }

        private void Activity(float frameTimeInSeconds)
        {
            if (!hasGameEnded)
            {
                backgroundLayer.Activity(frameTimeInSeconds);

                biker.Activity(frameTimeInSeconds);

                foreach (var van in vanList)
                {
                    van.Activity(frameTimeInSeconds);
                }

                PerformCollision();
            }
        }


        private void PerformCollision()
        {
            // reverse for loop since fruit may be destroyed:
            for (int i = vanList.Count - 1; i > -1; i--)
            {
                var van = vanList[i];

                bool didCollide = VanVsBiker(van);

                if (didCollide)
                {
                    EndGame();
                }

                VanLeft(van);
            }
        }

        private void VanLeft(VanEntity van)
        {
            if (van.PositionX < -100)
            {
                RemoveChild(van);
                vanList.Remove(van);
                CreateVan();
            }
        }

        private bool VanVsBiker(VanEntity van)
        {
            return !van.Thrown && biker.Children[0].BoundingBoxTransformedToWorld.IntersectsRect(van.Children[0].BoundingBoxTransformedToWorld);
        }

        private void EndGame()
        {
            hasGameEnded = true;

            var drawNode = new CCDrawNode();
            drawNode.DrawRect(
                new CCRect(0, 0, 2000, 2000),
                new CCColor4B(0, 0, 0, 160));
            hudLayer.AddChild(drawNode);

            var endGameLabel = new CCLabel("Game Over", "Arial", 40, CCLabelFormat.SystemFont);
            endGameLabel.HorizontalAlignment = CCTextAlignment.Center;
            endGameLabel.Color = CCColor3B.White;
            endGameLabel.VerticalAlignment = CCVerticalTextAlignment.Center;
            endGameLabel.PositionX = hudLayer.ContentSize.Width / 2.0f;
            endGameLabel.PositionY = hudLayer.ContentSize.Height / 2.0f;
            hudLayer.AddChild(endGameLabel);

        }

    }
}
