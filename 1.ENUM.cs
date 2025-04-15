using System;
namespace TrafficLightSimulation
{
    enum TrafficLight
    {
        Red = 1,
        Yellow = 2,
        Green = 3
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Traffic Light Simulation");
                Console.WriteLine("Choose a light (1-Red, 2-Yellow, 3-Green): ");

                string input = Console.ReadLine();

                if (!int.TryParse(input, out int choice))
                {
                    throw new FormatException("Input must be a number (1, 2, or 3).");
                }


                if (!Enum.IsDefined(typeof(TrafficLight), choice))
                {
                    throw new ArgumentOutOfRangeException("choice", "Invalid selection. Please choose 1, 2, or 3.");
                }


                TrafficLight selectedLight = (TrafficLight)choice;

                switch (selectedLight)
                {
                    case TrafficLight.Red:
                        Console.WriteLine("Action: STOP");
                        break;

                    case TrafficLight.Yellow:
                        Console.WriteLine("Action: GET READY");
                        break;

                    case TrafficLight.Green:
                        Console.WriteLine("Action: GO");
                        break;
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }
    }
}