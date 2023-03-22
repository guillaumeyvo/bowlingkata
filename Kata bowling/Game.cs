using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata_bowling
{
    internal class Game
    {
        private List<Frame> frames = new List<Frame>();
        private Frame currentFrame;
        private Frame previousFrame;

        public void Roll(int pinsStroked) 
        {
            if (frames.Count() == 0 || frames[frames.Count() - 1].IsFrameCompleted())
            {
                currentFrame = new Frame();
                if (frames.Count() > 0)
                {
                    previousFrame = frames[frames.Count() - 1];
                }

                frames.Add(currentFrame);

            }


            currentFrame.Roll(pinsStroked);

            if (previousFrame != null && previousFrame.HasSpare() && currentFrame.IsFirstRoll())
            {
                previousFrame.AddBonus(pinsStroked);
            }

        }
        public int Score()
        {
            return frames.Sum(f => f.GetFrameScore());
        }
    }

    internal class Frame
    {
        private int pinsStroked = 0;
        private int score = 0;
        private int numberOfRoll = 0;
        private bool hasSpare = false;
        private bool hasStrike = false;
        private bool isFrameCompleted = false;
        public void Roll(int pinsStroked)
        {
            score += pinsStroked;
            numberOfRoll++;

            if (score == 10 && numberOfRoll == 1) 
            {
                isFrameCompleted = true;
                hasStrike = true;
                return;
            }
             
            if (score == 10)
            {
                isFrameCompleted = true;
                hasSpare = true;
            }
        }

        public void AddBonus(int bonus)
        {
            score += bonus;
        }

        public bool IsFrameCompleted ()
        {
            return isFrameCompleted;
        }

        public bool HasSpare()
        {
            return hasSpare;
        }

        public bool IsFirstRoll()
        {
            return numberOfRoll == 1;
        }

        public int GetFrameScore()
        {
            return score;
        }
    }
}
