using System;
using CocosSharp;
using StrangerRun.Geometry;

namespace StrangerRun.Entities
{
    public class VanEntity : CCNode
    {
        float rotation = 360;
        CCSprite sprite;
        const float width = 116;
        const float height = 73;

        public VanEntity()
        {
            CreateSprite();
        }

        public bool Thrown { get; set; }

        private void CreateSprite()
        {
            sprite = new CCSprite("van.png");
            AddChild(sprite);
        }

        internal void Activity(float frameTimeInSeconds)
        {
            PositionX = PositionX - 2;
            if (Thrown && rotation > 180)
            {
                PositionX = PositionX - 1;
                rotation = rotation - 2f;
                float x = rotation >= 270 ? 1f : -1f;
                Rotation = rotation;
                PositionY = PositionY + 2.5f * x;
            }
        }
    }
}
