using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPi.SenseHat.Demo.Demos
{
    class SnakeBody
    {
        public Queue<int> snakeBodyX = new Queue<int>();
        public Queue<int> snakeBodyY = new Queue<int>();
        public void AddBody(int x, int y)
        {
            snakeBodyX.Enqueue(x);
            snakeBodyY.Enqueue(y);
        }
        public void IncreaseBody(SnakeHead head)
        {
            snakeBodyX.Enqueue(head.GetSnakeHeadX());
            snakeBodyY.Enqueue(head.GetSnakeHeadX());
        }
        public void MoveBody(SnakeHead head)
        {
            snakeBodyX.Dequeue();
            snakeBodyX.Enqueue(head.GetSnakeHeadX());
            snakeBodyY.Dequeue();
            snakeBodyY.Enqueue(head.GetSnakeHeadY());
        }
        public int GetBodyFrontX()
        {
            return snakeBodyX.LastOrDefault();
        }
        public int GetBodyFrontY()
        {
            return snakeBodyY.LastOrDefault();
        }
        public Queue<int> getBodyX()
        {
            return snakeBodyX;
        }
        public Queue<int> getBodyY()
        {
            return snakeBodyY;
        }
    }
}
