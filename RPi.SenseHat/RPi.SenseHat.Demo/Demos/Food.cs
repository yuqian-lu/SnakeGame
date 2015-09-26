using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPi.SenseHat.Demo.Demos
{
    class Food
    {
        int foodX, foodY;

        public Food()
        {
            Random randomNumber = new Random();
            foodX = randomNumber.Next(0, 8);
            foodY = randomNumber.Next(0, 8);
        }
        public int getFoodX()
        {
            return foodX;
        }
        public int getFoodY()
        {
            return foodY;
        }
    }
}
