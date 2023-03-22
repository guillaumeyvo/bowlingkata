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
        private bool isLastFrame = false;

        public void Roll(int pinsStroked)
        {

            if (frames.Count == 0 || frames[frames.Count - 1].IsFrameCompleted())
            {
                if (frames.Count == 9)
                {
                    currentFrame = new Frame(true);
                    isLastFrame = true;
                }
                else
                {
                    currentFrame = new Frame();
                }

                if (frames.Count > 0)
                {
                    previousFrame = frames[frames.Count - 1];
                }

                frames.Add(currentFrame);

                if (frames.Count > 10)
                {
                    throw new Exception("Number of rolls exceeded");
                }

            }

            currentFrame.Roll(pinsStroked);

            if (previousFrame == null)
            {
                return;
            }

            if (previousFrame.HasSpare() && currentFrame.IsFirstRoll())
            {
                previousFrame.AddSpareBonus(pinsStroked);
            }

            if (previousFrame.HasStrike())
            {
                if (isLastFrame && currentFrame.IsSecondRoll())
                {
                    previousFrame.AddStrikeBonus(currentFrame);

                } else if (!isLastFrame)
                {
                    previousFrame.AddStrikeBonus(currentFrame);
                }
            }

        }

        public int Score()
        {
            foreach (var frame in frames)
            {
                Console.WriteLine("{0} {1}", frames.IndexOf(frame) + 1, frame.GetFrameScore());
            }

            return frames.Sum(f => f.GetFrameScore());
        }
    }

    internal class Frame
    {
        private int score = 0;
        private int numberOfRoll = 0;
        private bool hasSpare = false;
        private bool hasStrike = false;
        private bool isFrameCompleted = false;
        private int strikeBonus = 0;
        private bool isTenthFrame = false;

        public Frame(bool isTenthFrame = false)
        {
            this.isTenthFrame = isTenthFrame;
        }
        public void Roll(int pinsStroked)
        {
            score += pinsStroked;
            numberOfRoll++;

            if (score == 10 && numberOfRoll == 1 && !isTenthFrame)
            {
                isFrameCompleted = true;
                hasStrike = true;
                return;
            }

            if (score == 10 && !isTenthFrame)
            {
                isFrameCompleted = true;
                hasSpare = true;
            }

            if (!isFrameCompleted && numberOfRoll == 2 && !isTenthFrame)
            {
                isFrameCompleted = true;
                return;
            }

            if (isTenthFrame && numberOfRoll == 2 && score < 10)
            {
                isFrameCompleted = true;
                return;
            }


            if (isTenthFrame && numberOfRoll == 3)
            {
                isFrameCompleted = true;
                return;
            }
        }

        public void AddSpareBonus(int bonus)
        {
            score += bonus;
        }

        public void AddStrikeBonus(Frame frame)
        {
            strikeBonus = frame.GetFrameScore();
        }

        public bool IsFrameCompleted()
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
            return score + strikeBonus;
        }

        public bool HasStrike()
        {
            return hasStrike;
        }

        public bool IsSecondRoll()
        {
            return numberOfRoll == 2;
        }
    }
}
