using System;
using System.Collections.Generic;
using System.Linq;
//using static RobotRover.DirectionHelper;

namespace RobotRover
{
    class Program
    {
        //initial location as mentioned
        static List<Rover> Rovers = new List<Rover>();
        //static DirectionHelper _directionHelper = new DirectionHelper();
        static void Main(string[] args)
        {
            InitialSetup();       
            bool exitFlag = false;
            var rover = Rovers.FirstOrDefault();
            while (!exitFlag)
            {                
                if(rover.CurrentPosition ==null)
                    rover.SetPosition(10, 10, "N");

                Console.WriteLine("Rover at postion : " + rover.CurrentPosition);
                Console.WriteLine("Please select one of the following options :\n1. SetPosition\n2. MoveCommand\nPress Any other Key to Exit");
                var input = Console.ReadLine();
                try
                {
                    switch (input)
                    {
                        case "1":
                            Console.WriteLine("Please enter position as [X<space>Y<space>DirectionCode] example [10 10 N] : ");
                            var (x,y,directionString) = ParsePositionString(Console.ReadLine());
                            rover.SetPosition(x, y, directionString);
                            Console.WriteLine("Location set to : " + rover.CurrentPosition);
                            Console.WriteLine("==================================================\n");
                            break;
                        case "2":
                            Console.WriteLine("Please enter commands as a single string:");
                            rover.Move(Console.ReadLine());
                            Console.WriteLine(rover.CurrentPosition!=null?rover.CurrentPosition.ToString():"Oops! The rover has fallen out of bounds...");
                            Console.WriteLine("==================================================\n");
                            break;
                        default:
                            exitFlag = true;
                            Console.WriteLine("Exiting...");
                            Console.WriteLine("==================================================\n");
                            break;
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("Exception occured : " + e.Message);
                }
            }
        }

        private static void InitialSetup()
        {
            var rover = new Rover(new Plateau { Width = 30, Height = 40 });
            Rovers.Add(rover);
        }
        private static (int,int,string) ParsePositionString(string v)
        {
            v = v.Trim().ToUpperInvariant();
            if (v.StartsWith("["))
                v = v.Substring(1);
            if (v.EndsWith("]"))
                v = v.Substring(0, v.Length - 1);
            string[] values = v.Split(' ');
            if (values != null & values.Length == 3)
            {
                int x = int.Parse(values[0]);
                int y = int.Parse(values[1]);
                return (x,y,values[2]);
            }
            throw new Exception("Invalid Position String!");
        }
    }
}
