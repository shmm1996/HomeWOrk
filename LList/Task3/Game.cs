using System;
using System.Collections.Generic;

namespace Task3
{
    public class Game
    {

        private LinkedList<Ball> _balls;

        public void Start(int length)
        {
            Random random = new Random();
            while (length > 0)
            {
                _balls.AddLast(new Ball {Color = random.Next(1, 10)});
                length--;
            }
        }

        public void Play()
        {
            LinkedListNode<Ball> currentBallNode = _balls.First;
            int lastColor = currentBallNode.Value.Color;
            int singleColorLength = 0;
            do
            {
                singleColorLength++;
                currentBallNode = currentBallNode.Next;
                if (currentBallNode.Value.Color != lastColor)
                {
                    if (singleColorLength < 3)
                        singleColorLength = 0;
                    else
                    {
                        LinkedListNode<Ball> previousBallNode = currentBallNode;
                        while (singleColorLength > 0)
                        {
                            previousBallNode = previousBallNode.Previous;
                            singleColorLength--;
                        }
                        currentBallNode.Previous = previousBallNode;
                        previousBallNode.Next = currentBallNode;
                    }
                    lastColor = currentBallNode.Value.Color;
                }
            } while (currentBallNode != null);
        }
    }
}
