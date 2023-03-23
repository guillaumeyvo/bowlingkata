namespace Kata_bowling
{
    internal class Game
    {
        private List<Frame> frames = new List<Frame>();

        public void Roll(int pinsStroked)
        {
            (Frame previousFrame, Frame currentFrame) = GetLastTwoFrames(frames);
            currentFrame.AddRoll(pinsStroked);

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
                if (currentFrame.IsLastFrame() && currentFrame.IsSecondRoll())
                {
                    previousFrame.AddStrikeBonus(currentFrame);

                } else if (!currentFrame.IsLastFrame())
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

        private (Frame previousFrame, Frame currentFrame) GetLastTwoFrames(List<Frame> frames)
        {
            Frame currentFrame = null;
            Frame previousFrame = null;

            if (frames.Count == 0)
            {
                currentFrame = new Frame();
                frames.Add(currentFrame);

                return (previousFrame, currentFrame);
            }

            if (frames.Count == 1)
            {
                if (frames[0].IsFrameCompleted())
                {
                    previousFrame = frames[0];
                    currentFrame = new Frame();

                    frames.Add(currentFrame);

                }
                else
                {
                    previousFrame = null;
                    currentFrame = frames[0];
                }

                return (previousFrame, currentFrame);

            }

            if (!frames[frames.Count - 1].IsFrameCompleted())
            {
                previousFrame = frames[frames.Count - 2];
                currentFrame = frames.Last();
            }
            else
            {
                previousFrame = frames.Last();
                currentFrame = frames.Count == 9 ? new Frame(true) : new Frame();

                frames.Add(currentFrame);


            }

            if (frames.Count > 10)
            {
                throw new Exception("Number of rolls exceeded");
            }

            return (previousFrame, currentFrame);
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
        public void AddRoll(int pinsStroked)
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
                return;
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

        public bool IsLastFrame()
        {
            return isTenthFrame;
        }
    }
}
