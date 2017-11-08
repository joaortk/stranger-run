using System;
using System.Linq;
using CocosSharp;
using StrangerRun.Geometry;

namespace StrangerRun.Entities
{
    public class BikerEntity : CCNode
    {
        const float width = 32;
        const float height = 32;

        CCSprite bikerSprite;
        CCSprite forceSprite;
        CCPoint desiredLocation;
        const int forceMaxBlinks = 3;
        int forceBlinkCount;
        float lowestY;
        float highestY = 5000;

        public CCPoint Delta;
        CCRepeatForever walkRepeat;


        public BikerEntity()
        {
            CreateSprite();

            StartAnimation();
        }

        public bool ForceActivated { get; set; }

        public void LimitMovement(float lowestY, float highestY)
        {
            this.lowestY = lowestY;
            this.highestY = highestY;

            desiredLocation = new CCPoint(32, (highestY - lowestY) / 2);

        }

        private void StartAnimation()
        {
            bikerSprite.RunAction(walkRepeat);
        }

        private void CreateSprite()
        {
            var spriteSheet = new CCSpriteSheet("mike11.plist");
            var animationFrames = spriteSheet.Frames;

            var walkAnim = new CCAnimation(animationFrames, 0.15f);
            walkRepeat = new CCRepeatForever(new CCAnimate(walkAnim));
            bikerSprite = new CCSprite(animationFrames.First()) { Name = "Mike11" };
            bikerSprite.AnchorPoint = CCPoint.AnchorMiddleBottom;
            AddChild(bikerSprite);

            forceSprite = new CCSprite("force_0");
            forceSprite.PositionX = 56;
            forceSprite.PositionY = 16;
            forceSprite.Opacity = 0;
            AddChild(forceSprite);
        }

        public void HandleInput(CCPoint touchPoint)
        {
            if (touchPoint.Y < lowestY)
            {
                touchPoint.Y = lowestY;
            }
            if (touchPoint.Y > highestY)
            {
                touchPoint.Y = highestY;
            }

            desiredLocation = touchPoint;
        }

        public void Activity(float frameTimeInSeconds)
        {
            Delta = (desiredLocation - this.Position);
            this.Position += Delta * frameTimeInSeconds;
        }

        internal void SetDesiredPositionToCurrentPosition()
        {
            desiredLocation.X = this.PositionX;
            desiredLocation.Y = this.PositionY;
        }

    }

}
