using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment_Golf
{
    class Program
    {
        static void Main(string[] args)
        {

            bool stayAlive = true;
            SwingLoop(stayAlive);

        }

        static void SwingLoop(bool stayAlive)
        {
            // <Summary> //
            // This method handles the majority of the code
            // it gathers data from various methods to 
            // make the golfgame a possibility



            double swings = 0;
            double distanceBetween = 0;
            double distanceBetweenNegative = 0;
            double distanceToHole = 0;

            List<double> swingDistance = new List<double>();
            List<double> distanceRemaining = new List<double>();

            // This calls for a method that will either let the
            // user pick the distance to the hole, or it'll random it
            distanceToHole = DistanceToHole();

            distanceBetween = distanceToHole;


            while (stayAlive)
            {
                try
                {
                    // This While loop makes the golfgame a reality, and it starts
                    // by fetching the values for the angle and velocity to create
                    // the correct ballDistance result which then also - with a simple
                    // equation - gives you the distance remaining after every shot.

                    double angle = AllowAngleAmount();
                    double velocity = AllowVelocityAmount();
                    double ballDistance = CalculateBallDistance(angle, velocity);

                    swingDistance.Add(ballDistance); // Adds every new swing to the list.

                    distanceBetween = ballDistance - distanceBetween; //This equation gives you the remaining distance to the hole after every hit.

                    distanceBetweenNegative = distanceBetween;
                    distanceBetween = Math.Abs(distanceBetween);
                    distanceRemaining.Add(distanceBetween);

                    // Here we check for 4 things that will either let you win, lose or continue shooting.
                    // if = Checks if the user shot further away than he begun - Results in loss if he does.
                    // else if = checks if the user has overshot and tells the user about it.
                    // else if = checks if the user has successfully gotten the ball in the cup - result in win.
                    // else = makes the loop continue until the user has either succeeded with previous statements or shot too many times.

                    if (distanceBetween > distanceToHole)
                    {
                        //Console.Clear();
                        Console.WriteLine(distanceBetween);
                        swings = swings + 1;

                        Console.WriteLine("You shot too far. You shot " + Math.Round(Math.Abs(distanceBetween), 1) + " meters too far in " + swings + " swings.");
                        Console.ReadKey();

                        stayAlive = false;
                    }
                    else if (distanceBetweenNegative < -1 && distanceBetweenNegative > -100)
                    {
                        swings = swings + 1;
                        Console.WriteLine("You shot " + Math.Round(Math.Abs(distanceBetween), 1) + " meters to far with your " + swings + " swing.");
                        //distanceBetween = Math.Abs(distanceBetween);
                        //Console.WriteLine(distanceBetween);
                    }
                    else if (Math.Abs(distanceBetween) < 0.1)
                    {
                        Console.Clear();

                        swings = swings + 1;
                        Console.WriteLine("Congratulations! you won in " + swings + " swings!");

                        for (int i = 0; i < swingDistance.Count; i++)
                        {
                            // This creates the value "Swing X = Y. where X = Swing number and Y = distance on that swing.
                            Console.WriteLine($"Swing {i + 1} = {Math.Round(swingDistance[i], 1)}", ConsoleColor.Yellow);
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
                            for (int i = 0; i < swingDistance.Count; i++)
                            {
                                Console.WriteLine($"Swing {i + 1} = {Math.Round(swingDistance[i], 1)}", ConsoleColor.Yellow);
                            }

                            Console.WriteLine("\nYou reached the max amount of attempts! You suck!", ConsoleColor.Red);
                            Console.ReadKey();

                            stayAlive = false;
                        }
                        else
                        {
                            Console.Clear();

                            Console.WriteLine("You have " + Math.Round(Math.Abs(distanceBetween), 1) + " meters to the hole. and you're on " + swings + " swings.", ConsoleColor.Yellow);
                        }
                    }
                }
                catch
                {

                }
            }
        }

        static double DistanceToHole()
        {
            Console.Clear();

            Console.Write("Do you wish for the computer to set the distance to the hole? y/n ");
            char choice = Console.ReadKey(true).KeyChar;
            if (choice == 'y')
            {
                Random random = new Random();
                int randomNumber = random.Next(1000, 2000);
                Console.Clear();
                DisplayMessage("The distance to the hole was determined to be: " + randomNumber + " meters. Good luck!", ConsoleColor.Green);
                double distanceToHole = Convert.ToDouble(randomNumber);

                return distanceToHole;

            }
            else if (choice == 'n')
            {
                Console.Clear();

                Console.Write("Please enter your wanted distance (in meters) to the hole! ");
                double distanceToHole = double.Parse(Console.ReadLine());

                if (distanceToHole < 0)
                {
                    DisplayMessage("What kind of backwards dimension are we living in?? Try again!", ConsoleColor.Red);
                    return DistanceToHole();
                }
                else if (distanceToHole > 2000)
                {
                    DisplayMessage("That's some absolute distance! Too big, try again!", ConsoleColor.Red);
                    return DistanceToHole();
                }
                else
                {
                    return distanceToHole;
                }
            }
            else
            {

                DisplayMessage("\nPlease try again...", ConsoleColor.Red);

                return DistanceToHole();
            }
        }

        static double AllowAngleAmount()
        {
            bool stayAlive = true;
            int loopNumber = 0;
            do
            {

                try
                {
                    // <Summary> //
                    // This Method asks for the angle and verifies the angle
                    // is between 0 and 89 degrees. if it isn't it'll repeat
                    // itself until the user gives out a valid amount

                    Console.Write("Please insert the angle you wish to shoot in: ");
                    double angle = double.Parse(Console.ReadLine());

                    if (angle <= -1)
                    {

                        DisplayMessage("We're playing golf. Not a digging simulator!", ConsoleColor.Red);
                    }
                    else if (angle >= 90)
                    {

                        DisplayMessage("Can't shoot at an altitude this high", ConsoleColor.Red);
                    }
                    else
                    {
                        return angle;
                    }
                }
                catch (FormatException)
                {
                    DisplayMessage("Please actually type a correct value.", ConsoleColor.Red);
                }
                catch (OverflowException)
                {
                    DisplayMessage("Do try to pick a reasonable number next time. :) ", ConsoleColor.Red);
                }
                catch (Exception)
                {
                    DisplayMessage("Something went wrong. :(", ConsoleColor.Red);
                }
            }
            while (stayAlive); // Personal reminder = Do-While loops makes the While not require curly brackets.
            return loopNumber;
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

        static double CalculateBallDistance(double angle, double velocity)
        {
            double gravity = 9.8;

            double radianValue = (Math.PI / 180) * angle;
            double ballDistance = Math.Pow(velocity, 2) / gravity * Math.Sin(2 * radianValue);

            return ballDistance;
        }

        static void DisplayMessage(string note, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("\n" + note + "\n");
            Console.ResetColor();
            Console.WriteLine("Press any key to continue. . .");
            Console.ReadKey();

        }
    }
}
