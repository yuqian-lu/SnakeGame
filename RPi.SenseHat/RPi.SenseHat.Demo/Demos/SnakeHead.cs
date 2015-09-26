using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPi.SenseHat.Demo.Demos
{
    class SnakeHead
    {
        int snakeHeadX;
        int snakeHeadY;
        internal SnakeHead(int x, int y)
        {
            snakeHeadX = x;
            snakeHeadY = y;
        }
        public int GetSnakeHeadX()
        {
            return snakeHeadX;
        }
        public void SetSnakeHeadX(int x)
        {
            snakeHeadX = x;
        }
        public int GetSnakeHeadY()
        {
            return snakeHeadY;
        }
        public void SetSnakeHeadY(int y)
        {
            snakeHeadY = y;
        }
        internal SnakeHead returnHead()
        {
            return this;
        }
    }
}
