using System;
using CocosSharp;

namespace StrangerRun.Game
{
    public class BackgroundLayer : CCLayer
    {
        private CCTileMap streetMap;
        private float delta = -8f;
        private int frameCount;
        private int frameLimit = 10;


        protected override void AddedToScene()
        {
            base.AddedToScene();
            streetMap = new CCTileMap("street.tmx");
            streetMap.Antialiased = false;
            streetMap.ScaleX = 1.2f;
            AddChild(streetMap);
        }

        public void Activity(float frameTimeInSeconds)
        {
            if (frameCount == frameLimit)
            {
                streetMap.PositionX += delta;
                delta = delta * -1;
                frameCount = 0;
            }
            frameCount++;
        }
    }
}
