using System;
using System.Collections.Generic;

namespace Assignment_Golf
{
    class Program
    {
        static void Main(string[] args)
        {
            //Math.Abs is a math formula that will always give you back a positive value
            //Math.Round is a math formula that rounds the amount of decimals

            //ballDis = Math.Abs(ballDis);

            double ballDis = DistanceCalculation();
            Console.WriteLine(ballDis);
            Console.ReadKey();
        }
        static double DistanceCalculation()
        {
            // <Summary> //
            // This method calculates the distance flown by the ball after
            // the player has input correct angle and velocity values

            bool stayAlive = true;

            double distanceToHole = DistanceToHole();
            double angle = AllowAngleAmount();
            double velocity = AllowVelocityAmount();
            int swings = 0;

            double gravity = 9.8;

            double radianValue = (Math.PI / 180) * angle;
            double ballDistance = Math.Pow(velocity, 2) / gravity * Math.Sin(2 * radianValue);

            double distanceBetween = distanceToHole - ballDistance;


            while (stayAlive)
            {

            double radianLoopValue = (Math.PI / 180) * angle;
            double ballLoopDistance = Math.Pow(velocity, 2) / gravity * Math.Sin(2 * radianValue);

            double distanceLoopBetween = distanceToHole - ballDistance;
                if (distanceLoopBetween < -500)
                {
                    Console.WriteLine("You shot too far. You lose the game.");
                    Console.ReadKey();
                    stayAlive = false;
                }
                else if (distanceLoopBetween == 0)
                {
                    swings++;
                    Console.WriteLine("Congratulations! you won in " + swings + " swings!");
                }
                else
                {
                    return DistanceToHole();
                }
            }

            return Math.Round(ballDistance, 2);
        }
        static void SwingLoop()
        {
            List<int> swings = new List<int>();
        }
        static double DistanceToHole()
        {
            Console.Write("Please enter your wanted distance (in meters) to the hole!");
            double distanceToHole = double.Parse(Console.ReadLine());

            if (distanceToHole < 0)
            {
                Console.WriteLine("What kind of backwards dimension are we living in?? Try again!");
                return DistanceToHole();
            }
            else if (distanceToHole >= 2500)
            {
                Console.WriteLine("That's some absolute distance! Too big, try again!");
                return DistanceToHole();
            }
            else return distanceToHole;
        }
        static double AllowAngleAmount()
        {
                                      // <Summary> //
                     // This Method asks for the angle and verifies the angle
                     // is between 0 and 89 degrees. if it isn't it'll repeat
                     // itself until the user gives out a valid amount

            Console.Write("Please insert the angle you wish to shoot in: ");
            double angle = double.Parse(Console.ReadLine());

            if (angle < 0 )
            {
                Console.WriteLine("Can't shoot in negative angles!");
                return AllowAngleAmount();
            }
            else if (angle >= 90)
            {
                Console.WriteLine("Can't shoot at an altitude this high");
                return AllowAngleAmount();
            }
            else
            {
                return angle;
            }
        }
        static double AllowVelocityAmount()
        {
            Console.Write("Please insert the velocity you want to shoot in (m/s): ");
            double velocity = double.Parse(Console.ReadLine());

            if (velocity <= 0)
            {
                Console.WriteLine("You can't shoot in negative speeds, silly you!");
                return AllowVelocityAmount();
            }
            else if (velocity > 100)
            {
                Console.WriteLine("Did you use a rocket to shoot or something? Try again!");
                return AllowVelocityAmount();
            }
            else if (velocity > 500)
            {
                Console.WriteLine("Hehe, you're funny. try again!");
                return AllowVelocityAmount();
            }
            else
            {
                return velocity;
            }

        }
    }
}
