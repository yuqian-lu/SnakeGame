////////////////////////////////////////////////////////////////////////////
//
//  This file is part of Rpi.SenseHat.Demo
//
//  Copyright (c) 2015, Mattias Larsson
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy of 
//  this software and associated documentation files (the "Software"), to deal in 
//  the Software without restriction, including without limitation the rights to use, 
//  copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the 
//  Software, and to permit persons to whom the Software is furnished to do so, 
//  subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in all 
//  copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
//  INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
//  PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
//  HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION 
//  OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
//  SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using Windows.UI;
using Emmellsoft.IoT.Rpi.SenseHat;
using RichardsTech.Sensors;

namespace RPi.SenseHat.Demo.Demos
{
	public class JoystickPixel : SenseHatDemo
	{
		public JoystickPixel(ISenseHat senseHat)
			: base(senseHat)
		{
		}

		public override void Run()
		{
            /*// The initial position of the pixel.
			int x = 3;
			int y = 3;
            */
            Snake mySnake = new Snake();
            Food theFood = null;
			SenseHat.Display.Clear();
            SenseHat.Display.Screen[mySnake.GetHeadX(), mySnake.GetHeadY()] = Colors.Yellow; // Draw the pixel.
            {
                int x = 0, y = 0;
                foreach (int positionX in mySnake.GetBody().getBodyX())
                {
                    x += 1;
                    foreach (int positionY in mySnake.GetBody().getBodyY())
                    {
                        y += 1;
                        if (y == x)
                        {
                            SenseHat.Display.Screen[positionX, positionY] = Colors.Yellow;
                        }
                    }
                    y = 0;
                }
            }

            while (true)
			{
                if (!SenseHat.Sensors.ImuSensor.Update())
                {
                    return;
                }

                if (!SenseHat.Sensors.Acceleration.HasValue)
                {
                    return;
                }

                if ( theFood == null)
                {
                    theFood = new Food();
                }
                Vector3 gravityDirection = SenseHat.Sensors.Acceleration.Value;

				UpdatePosition(mySnake, gravityDirection, theFood); // Move the pixel.



				SenseHat.Display.Clear(); // Clear the screen.
                
                if (theFood != null)
                {
                    SenseHat.Display.Screen[theFood.getFoodX(), theFood.getFoodY()] = Colors.Green;
                }
				SenseHat.Display.Screen[mySnake.GetHeadX(), mySnake.GetHeadY()] = Colors.Yellow; // Draw the pixel.

                {
                    int x = 0, y = 0;
                    foreach (int positionX in mySnake.GetBody().getBodyX())
                    {
                        x += 1;
                        foreach (int positionY in mySnake.GetBody().getBodyY())
                        {
                            y += 1;
                            if (y == x)
                            {
                                SenseHat.Display.Screen[positionX, positionY] = Colors.Yellow;
                            }
                        }
                        y = 0;
                    }
                }

                SenseHat.Display.Update(); // Update the physical display.
                

				// Take a short nap.
				Sleep(TimeSpan.FromMilliseconds(500));
			}
		}

		private void UpdatePosition(Snake mySnake, Vector3 gravityDirection, Food theFood)
		{
            if (Math.Abs(gravityDirection.X) > Math.Abs(gravityDirection.Y) && gravityDirection.X < 0 && mySnake.GetHeadX() - 1 != mySnake.GetBody().GetBodyFrontX())
			{
                if (mySnake.GetHeadX() - 1 == theFood.getFoodX() && mySnake.GetHeadY() == theFood.getFoodY())
                {
                    mySnake.SnakeEat(mySnake.GetHeadX() - 1, mySnake.GetHeadY());
                    theFood = null;
                }
                else if (mySnake.GetHeadX() > 0)
				{
					mySnake.SnakeMove(mySnake.GetHeadX() - 1, mySnake.GetHeadY());
				}
			}
            else if (Math.Abs(gravityDirection.X) > Math.Abs(gravityDirection.Y) && gravityDirection.X > 0 && mySnake.GetHeadX() + 1 != mySnake.GetBody().GetBodyFrontX())
            {
                if (mySnake.GetHeadX() + 1 == theFood.getFoodX() && mySnake.GetHeadY() == theFood.getFoodY())
                {
                    mySnake.SnakeEat(mySnake.GetHeadX() + 1, mySnake.GetHeadY());
                    theFood = null;
                }
                else if (mySnake.GetHeadX() < 7)
                {
                    mySnake.SnakeMove(mySnake.GetHeadX() + 1, mySnake.GetHeadY());
                }
            }

            if (Math.Abs(gravityDirection.X) < Math.Abs(gravityDirection.Y) && gravityDirection.Y < 0 && mySnake.GetHeadY() - 1 != mySnake.GetBody().GetBodyFrontY())
            {
                if (mySnake.GetHeadX() == theFood.getFoodX() && mySnake.GetHeadY() - 1 == theFood.getFoodY())
                {
                    mySnake.SnakeEat(mySnake.GetHeadX(), mySnake.GetHeadY() - 1);
                    theFood = null;
                }
                else if (mySnake.GetHeadY() > 0)
                {
                    mySnake.SnakeMove(mySnake.GetHeadX(), mySnake.GetHeadY() - 1);
                }
            }
            else if (Math.Abs(gravityDirection.X) < Math.Abs(gravityDirection.Y) && gravityDirection.Y > 0 && mySnake.GetHeadY() + 1 != mySnake.GetBody().GetBodyFrontY())
            {
                if (mySnake.GetHeadX() == theFood.getFoodX() && mySnake.GetHeadY() + 1 == theFood.getFoodY())
                {
                    mySnake.SnakeEat(mySnake.GetHeadX(), mySnake.GetHeadY() + 1);
                    theFood = null;
                }
                else if (mySnake.GetHeadY() < 7)
                {
                    mySnake.SnakeMove(mySnake.GetHeadX(), mySnake.GetHeadY() + 1);
                }
            }
        }
	}
}