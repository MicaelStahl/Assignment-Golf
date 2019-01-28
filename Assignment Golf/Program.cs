using System;
using System.Collections.Generic;

namespace Assignment_Golf
{
    class Program
    {
        static void Main(string[] args)
        {
            // Something fun I wanted to add as the homescreen to the game.

            Console.WriteLine("\t\t\t\t" + @"__      _____   _    ____ ___  _ __ ___   ___  ");
            Console.WriteLine("\t\t\t\t" + @"\ \ /\ / / _ \ | |  / ___/ _ \| '_ ` _ \ / _ \ ");
            Console.WriteLine("\t\t\t\t" + @" \ V  V /  __/ | |_| (__| (_) | | | | | |  __/ ");
            Console.WriteLine("\t\t\t\t" + @"  \_/\_/ \___| |___|\____\___/|_| |_| |_|\___| ");

            Console.WriteLine("\n");
            Console.WriteLine("\t\t\t" + @"  '\                    .    .                        |>18>> ");
            Console.WriteLine("\t\t\t" + @"    \              .            '  .                  | ");
            Console.WriteLine("\t\t\t" + @"   O>>         .                     'o               | ");
            Console.WriteLine("\t\t\t" + @"    \       .                                         | ");
            Console.WriteLine("\t\t\t" + @"    /\    .                                           | ");
            Console.WriteLine("\t\t\t" + @"   / /  .'                                            | ");
            Console.WriteLine("\t\t\t" + @"^^^^^^^`^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
            Console.Write("\t\t\t\t Press any key to continue to the golfing!");

            Console.ReadKey();

            SwingLoop();

        }

        static void SwingLoop()
        {
            // <Summary> //
            // This method handles the majority of the code
            // it gathers data from various methods to 
            // make the golfgame a possibility
            bool stayAlive = true;

            int swings = 0;
            double distanceBetween = 0;
            double distanceBetweenNegative = 0;
            double distanceToHole = 0;

            distanceToHole = DistanceToHole();

            List<double> swingDistance = new List<double>();
            List<double> distanceRemaining = new List<double>();

            // This calls for a method that will either let the
            // user pick the distance to the hole, or it'll random it

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
                    distanceRemaining.Add(distanceBetween); //Adds the current distance remaining to a list.

                    // Here we check for 4 things that will either let you win, lose or continue shooting.
                    // if = Checks if the user shot further away than he begun - Results in loss if he does.
                    // else if = checks if the user has overshot and tells the user about it.
                    // else if = checks if the user has successfully gotten the ball in the cup - result in win.
                    // else = makes the loop continue until the user has either succeeded with previous statements or shot too many times.

                    if (distanceBetween > distanceToHole)
                    {
                        swings = swings + 1;
                        DisplayMessage("You're out! You shot " + Math.Round(Math.Abs(distanceBetween), 1) + " meters too far in " + swings + " swings.", ConsoleColor.Red);
                        Console.ReadKey();

                        stayAlive = false;
                    }
                    else if (distanceBetweenNegative < -1 && distanceBetweenNegative > -100)
                    {
                        Console.Clear();

                        swings = swings + 1;
                        DisplayMessage("You shot " + Math.Round(Math.Abs(distanceBetween), 1) + " meters to far with your " + swings + " swing.", ConsoleColor.Yellow);
                    }
                    else if (Math.Abs(distanceBetween) < 0.1)
                    {
                        Console.Clear();

                        swings = swings + 1;

                        if (swings == 1)
                        {
                            DisplayMessage("Congratulations! You shot " + Math.Round(ballDistance, 1) + " which resulted in a hole in one!", ConsoleColor.Green);

                            stayAlive = false;
                        }
                        DisplayMessage("Congratulations! you won in " + swings + " swings!", ConsoleColor.Green);

                        for (int i = 0; i < swingDistance.Count; i++)
                        {
                            // This creates the value "Swing X = Y. where X = Swing number and Y = distance on that swing with the distance remaining.
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"Swing {i + 1} = {Math.Round(swingDistance[i], 1)} meter swing.\t {Math.Round(distanceRemaining[i], 1)} meters remains", ConsoleColor.Yellow);
                        }

                        DisplayMessage("Press any key exit the game like the champion you truly are!", ConsoleColor.Green);
                        Console.ReadKey();
                        stayAlive = false;
                    }
                    else
                    {
                        swings = swings + 1;

                        if (swings >= 10)
                        {
                            DisplayMessage("You had " + Math.Round(Math.Abs(distanceBetween), 1) + " meters to the hole. And you're on " + swings + " swings.", ConsoleColor.Yellow);

                            Console.ForegroundColor = ConsoleColor.Yellow;

                            for (int i = 0; i < swingDistance.Count; i++)
                            {
                                Console.WriteLine($"Swing {i + 1} = {Math.Round(swingDistance[i], 1)} meter swing.\t {Math.Round(distanceRemaining[i], 1)} meters remains");
                            }

                            DisplayMessage("\nYou reached the max amount of attempts! The game will now self-destruct in 3.. 2.. 1..", ConsoleColor.Red);
                            Console.ReadKey();

                            stayAlive = false;
                        }
                        else
                        {
                            Console.Clear();

                            DisplayMessage("You have " + Math.Round(Math.Abs(distanceBetween), 1) + " meters to the hole. And you're on " + swings + " swings.", ConsoleColor.Yellow);
                        }
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    DisplayMessage("How'd you manage this? This value is way too big!", ConsoleColor.Red);
                }
            }
        }

        static double DistanceToHole()
        {
            bool stayAlive = true;
            int lackOfCreativeNames = 0;

            Console.Clear();

            do
            {
                try
                {

                    Console.Write("\nDo you want the computer to set the distance to the hole? y/n ");
                    char choice = Console.ReadKey(true).KeyChar;
                    if (choice == 'y')
                    {
                        Random random = new Random();
                        int randomNumber = random.Next(200, 2000);

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
                        }
                        else if (distanceToHole > 2000 && distanceToHole < 10000)
                        {
                            DisplayMessage("That's some absolute distance! Too big, try again!", ConsoleColor.Red);
                        }
                        else if (distanceToHole > 10000)
                        {
                            DisplayMessage("Are we trying to play golf, or fly to Jupiter?", ConsoleColor.Red);
                        }
                        else
                        {
                            return distanceToHole;
                        }
                    }
                    else
                    {
                        DisplayMessage("\nPlease try again...", ConsoleColor.Red);
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
            } while (stayAlive);
            return lackOfCreativeNames;
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
                    // is between 0 and 89 degrees. if it isn't, it'll repeat
                    // itself until the user gives out a valid amount

                    Console.Write("Please insert the angle you wish to shoot in: ");
                    double angle = double.Parse(Console.ReadLine());

                    if (angle <= 0)
                    {

                        DisplayMessage("We're playing golf. Not a digging simulator!", ConsoleColor.Red);
                    }
                    else if (angle > 90)
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
            while (stayAlive);
            return loopNumber;
        }

        static double AllowVelocityAmount()
        {
            // <Summary> //
            // This method asks the user for a velocity that is 
            // allowed inside the given parameters. if it isn't,
            // then the user will have to try again.
            bool stayalive = true;
            int creativenumber = 0;

            do
            {
                try
                {


                    Console.Write("Please insert the velocity you want to shoot in (m/s): ");
                    double velocity = double.Parse(Console.ReadLine());

                    if (velocity < 0)
                    {
                        DisplayMessage("You can't shoot in negative speeds, silly you!", ConsoleColor.Red);
                    }
                    else if (velocity > 100 && velocity < 500)
                    {
                        DisplayMessage("Did you use a rocket to shoot or something? Try again!", ConsoleColor.Red);
                    }
                    else if (velocity > 500)
                    {
                        DisplayMessage("Hehe, you're funny. try again!", ConsoleColor.Red);
                    }
                    else
                    {
                        return velocity;
                    }
                }
                catch (FormatException)
                {
                    DisplayMessage("Please enter a NUMBER!", ConsoleColor.Red);
                }
                catch (ArgumentNullException)
                {
                    DisplayMessage("Don't know how you managed this, but try again.", ConsoleColor.Red);
                }
                catch (OverflowException)
                {
                    DisplayMessage("Please type a smaller number", ConsoleColor.Red);
                }
            } while (stayalive);
            return creativenumber;
        }

        static double CalculateBallDistance(double angle, double velocity)
        {
            // This method creates the equation to calculate the current ballDistance by bringing 
            // in the angle and velocity from previous methods.
            double gravity = 9.8;

            double radianValue = (Math.PI / 180) * angle;
            double ballDistance = Math.Pow(velocity, 2) / gravity * Math.Sin(2 * radianValue);

            return ballDistance;
        }

        static void DisplayMessage(string note, ConsoleColor color = ConsoleColor.White)
        {
            // This simple method works a bit like a Console.WriteLine and also saves unnecessary lines of code
            // for Console.ForeGroundColor, Console.ResetColor etc.
            Console.ForegroundColor = color;
            Console.WriteLine("\n" + note + "\n");
            Console.ResetColor();
        }
    }
}
