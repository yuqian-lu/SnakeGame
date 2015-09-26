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
using SnakeLogic;

namespace RPi.SenseHat.Demo.Demos
{
    public sealed class snake : SenseHatDemo
    {
        public Arena arena = new SnakeLogic.Arena(8, 8);

        public snake(ISenseHat senseHat)
            : base(senseHat)
        {
        }

        private Direction OutputMoveDirection()
        {
            //double MoveDirection;
            //Determine the dominant direction 
            Vector3 Acceleration = SenseHat.Sensors.Acceleration.Value;
            double XAcceleration = System.Math.Abs(Acceleration.X);
            double YAcceleration = System.Math.Abs(Acceleration.Y);
            if (XAcceleration >= YAcceleration)
            {
                return Acceleration.X > 0 ? Direction.Right : Direction.Left;
            }

            return Acceleration.Y > 0 ? Direction.Down : Direction.Up;
        }

        public override void Run()
        {
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

                Direction d = this.OutputMoveDirection();
                arena.ChangeDirection(d);
                arena.Update();
                var tuple = arena.GetData();
                //int[,] gameScreen = new int[8, 8] { { 0, 1, 0, 1, 0, 0, 0, 0 }, { 0, 1, 0, 0, 0, 0, 0, 0 }, { 0, 1, 0, 0, 0, 0, 0, 0 }, { 0, 1, 0, 0, 0, 0, 0, 0 }, { 0, 1, 0, 0, 0, 0, 0, 0 }, { 0, 1, 0, 0, 0, 0, 0, 0 }, { 0, 1, 0, 0, 0, 0, 0, 0 }, { 0, 1, 0, 0, 0, 0, 0, 0 } };
                UpdateScreen(tuple.Item1, tuple.Item2);

                Sleep(TimeSpan.FromMilliseconds(500));
            }
        }

        private void UpdateScreen(int[,] gameScreen, Point food)
        {
            SenseHat.Display.Clear(); // Clear the screen.

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (gameScreen[x, y] > 0)
                    {
                        SenseHat.Display.Screen[x, y] = Colors.Green; // Draw the pixel.
                    }

                }
            }
            if(food!=null)
            {
                SenseHat.Display.Screen[food.X, food.Y] = Colors.Yellow; // Draw the pixel.
            }

            SenseHat.Display.Update(); // Update the physical display.
        }


        private static Color[,] CreateGravityBlobScreen(Vector3 vector)
        {
            double x0 = (vector.X + 1) * 5.5 - 2;
            double y0 = (vector.Y + 1) * 5.5 - 2;

            double distScale = 4;

            var colors = new Color[8, 8];

            bool isUpsideDown = vector.Z < 0;

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    double dx = x0 - x;
                    double dy = y0 - y;

                    double dist = Math.Sqrt(dx * dx + dy * dy) / distScale;
                    if (dist > 1)
                    {
                        dist = 1;
                    }

                    int colorIntensity = (int)Math.Round(255 * (1 - dist));
                    if (colorIntensity > 255)
                    {
                        colorIntensity = 255;
                    }

                    colors[x, y] = isUpsideDown
                        ? Color.FromArgb(255, (byte)colorIntensity, 0, 0)
                        : Color.FromArgb(255, 0, (byte)colorIntensity, 0);
                }
            }

            return colors;
        }
    }
}