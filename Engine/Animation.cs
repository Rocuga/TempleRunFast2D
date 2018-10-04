using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace templeRun
{
    class Animation : IActivable,IUpdatable,ICloneable
    {
        public int FrameWidth { get; private set; }
        public int FrameHeight { get; private set; }
        public int OffsetX { get; private set; }
        public int OffsetY { get; private set; }

        float frameDelay;
        float counter;

        int currentFrameIndex;
        int currentFrame
        {
            get { return currentFrameIndex; }
            set
            {
                currentFrameIndex = value;
                OffsetX = startX + (currentFrameIndex % cols) * FrameWidth;
                OffsetY = startY + (currentFrameIndex / cols) * FrameHeight;
            }
        }

        int startX;
        int startY;

        int rows;
        int cols;

        int numFrames;

        bool loop;

        public bool IsActive { get; set; }
        public Animation(int fwidth, int fheight, int cols = 1, int rows = 1, float fps = 1f, bool loop = true, int startX = 0, int startY = 0)
        {
            FrameWidth = fwidth;
            FrameHeight = fheight;
            this.loop = loop;
            this.startX = startX;
            this.startY = startY;
            this.rows = rows;
            this.cols = cols;
            frameDelay = 1 / fps;
            numFrames = rows * cols;
            IsActive = true;
            currentFrame = 0;
        }
        protected virtual void OnAnimationEnds()
        {
            if (loop)
            {
                currentFrame = 0;
            }
            else
            {
                IsActive = false;
            }
        }
        public void Update()
        {
            if (IsActive)
            {
                counter += Game.DeltaTime;
                if (counter >= frameDelay)
                {
                    counter = 0;
                    if (++currentFrame == numFrames)
                    {
                        OnAnimationEnds();
                    }
                }
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
