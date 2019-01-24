using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment_Golf
{
    class Program
    {
        static void Main(string[] args)
        {

            SwingLoop();

        }

        static void SwingLoop()
        {
            // <Summary> //
            // This method handles the majority of the code
            // it gathers data from various methods to 
            // make the golfgame a possibility

            bool stayAlive = true;

            double swings = 0;
            double gravity = 9.8;
            double distanceBetween = 0;
            //double distanceBetweenNegative = 0;
            double finalBallDistance = 0;
            double eachSwingDistanceCount = 0;
            double distanceToHole = 0;

            List<double> eachSwingDistance = new List<double>();

            // This calls for a method that will either let the
            // user pick the distance to the hole, or it'll random it
            distanceToHole = DistanceToHole();

            while (stayAlive)
            {
                // This While loop makes the golfgame a reality, and it starts
                // by fetching the values for the angle and velocity to create
                // the correct ballDistance result.

                double angle = AllowAngleAmount();
                double velocity = AllowVelocityAmount();


                double radianValue = (Math.PI / 180) * angle;
                double ballDistance = Math.Pow(velocity, 2) / gravity * Math.Sin(2 * radianValue);

                eachSwingDistance.Add(ballDistance); // Adds every new swing to the list.
                eachSwingDistanceCount = eachSwingDistance.Count;

                foreach (double distance in eachSwingDistance)
                {
                    finalBallDistance = finalBallDistance + distance;

                    distanceBetween = distanceToHole - finalBallDistance;

                }

                finalBallDistance = 0;

                //distanceBetween = Math.Abs(distanceBetweenNegative);

                // Here we check for 3 things that will either let you win, lose or continue shooting.
                // if = Checks if the user shot more than 500 meters too far - Results in loss
                // else if = checks if the user has successfully gotten the ball in the cup - result in win
                // else = makes the loop continue until the user has either succeeded with previous statements or shot too many times.

                if (distanceBetween < -100)
                {
                    Console.Clear();
                    Console.WriteLine(distanceBetween);
                    swings = swings + 1;

                    Console.WriteLine("You shot too far. You shot " + Math.Round(Math.Abs(distanceBetween), 1) + " meters too far in " + swings + " swings.");
                    Console.ReadKey();

                    stayAlive = false;
                }
                else if (distanceBetween < -1 && distanceBetween > -100)
                {
                    Console.WriteLine(distanceBetween);
                    //distanceBetween = Math.Abs(distanceBetween);
                    //Console.WriteLine(distanceBetween);
                }
                else if (Math.Abs(distanceBetween) < 0.1)
                {
                    Console.Clear();

                    swings = swings + 1;
                    Console.WriteLine("Congratulations! you won in " + swings + " swings!");

                    for (int i = 0; i < eachSwingDistance.Count; i++)
                    {
                        // This creates the value "Swing X = Y. where X = Swing number and Y = distance on that swing.
                        Console.WriteLine($"Swing {i + 1} = {Math.Round(eachSwingDistance[i], 1)}");
                    }
                    Console.Write("Press any key to continue, you champion!");
                    Console.ReadKey();
                    stayAlive = false;
                }
                else
                {

                    swings = swings + 1;

                    if (swings >= 10)
                    {
                        for (int i = 0; i < eachSwingDistance.Count; i++)
                        {
                            Console.WriteLine($"Swing {i + 1} = {Math.Round(eachSwingDistance[i], 1)}");
                        }

                        Console.WriteLine("\nYou reached the max amount of attempts! You suck!");
                        Console.ReadKey();

                        stayAlive = false;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine(distanceBetween);

                        Console.WriteLine("You have " + Math.Round(Math.Abs(distanceBetween), 1) + " meters to the hole. and you're on " + swings + " swings.");

                    }
                }
            }
        }
        static double DistanceToHole()
        {
            Console.Clear();

            Console.Write("Do you wish for the computer to set the distance to the hole? y/n ");
            string choice = Console.ReadLine();
            choice = choice.ToLower();
            if (choice == "y")
            {
                Random random = new Random();
                int randomNumber = random.Next(1000, 2000);
                Console.Clear();
                Console.WriteLine("The distance to the hole was determined to be: " + randomNumber + " meters. Good luck!");
                double distanceToHole = Convert.ToDouble(randomNumber);

                return distanceToHole;

            }
            else if (choice == "n")
            {
                Console.Clear();

                Console.Write("Please enter your wanted distance (in meters) to the hole! ");
                double distanceToHole = double.Parse(Console.ReadLine());

                if (distanceToHole < 0)
                {
                    Console.WriteLine("What kind of backwards dimension are we living in?? Try again!");
                    return DistanceToHole();
                }
                else if (distanceToHole > 2000)
                {
                    Console.WriteLine("That's some absolute distance! Too big, try again!");
                    return DistanceToHole();
                }
                else
                {
                    return distanceToHole;
                }
            }
            else
            {
                Console.WriteLine("Please try again");
                return DistanceToHole();
            }
        }
        static double AllowAngleAmount()
        {
            // <Summary> //
            // This Method asks for the angle and verifies the angle
            // is between 0 and 89 degrees. if it isn't it'll repeat
            // itself until the user gives out a valid amount

            Console.Write("Please insert the angle you wish to shoot in: ");
            double angle = double.Parse(Console.ReadLine());

            if (angle < -1)
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

            if (velocity < 0)
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
