using RoverTheExplorer.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RoverTheExplorer
{
    class Program
    {
        private static Robot CurrentRobot { get; set; }

        private static Coordinate UpperRightCoordinates { get; set; }

        private static List<Direction> Directions
        {
            get
            {
                return new List<Direction>
                {
                    new Direction{ Key = 'N', Value = 0, MovementVector = new Coordinate{ X = 0, Y = 1 } },
                    new Direction{ Key = 'E', Value = 1, MovementVector = new Coordinate{ X = 1, Y = 0 } },
                    new Direction{ Key = 'S', Value = 2, MovementVector = new Coordinate{ X = 0, Y = -1 } },
                    new Direction{ Key = 'W', Value = 3, MovementVector = new Coordinate{ X = -1, Y = 0 } },
                };
            }
        }

        static void Main(string[] args)
        {

            bool endSession = false;

            while (!endSession)
            {
                //Check upper-right coordinates
                if (UpperRightCoordinates == null)
                {
                    var upperRightInput = Console.ReadLine().Split(" ");
                    int worldX, worldY;

                    //Input validation
                    if (upperRightInput.Length == 2
                        && int.TryParse(upperRightInput[0], out worldX)
                        && int.TryParse(upperRightInput[1], out worldY)
                        && worldX > 0 && worldY > 0)
                    {
                        UpperRightCoordinates = new Coordinate { X = worldX, Y = worldY };
                    }
                    else
                    {
                        Console.WriteLine("Enter valid upper-right coordinates");
                    }
                }

                //Check robot exist
                if (UpperRightCoordinates != null && CurrentRobot == null)
                {
                    var robotInput = Console.ReadLine().Split(" ");
                    int robotX, robotY;
                    char robotFaceDirection;

                    //Input validation
                    if (robotInput.Length == 3
                        && int.TryParse(robotInput[0], out robotX)
                        && int.TryParse(robotInput[1], out robotY)
                        && char.TryParse(robotInput[2], out robotFaceDirection))
                    {
                        Direction robotDirection = Directions.FirstOrDefault(w => w.Key == robotFaceDirection);

                        if (robotDirection != null)
                        {
                            CurrentRobot = new Robot { FaceDirection = robotDirection, X = robotX, Y = robotY };
                        }
                        else
                        {
                            Console.WriteLine("Enter valid robot face direction");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Enter valid robot coordinates");
                    }
                }
                else
                {
                    var movementInput = Console.ReadLine();

                    for (int i = 0; i < movementInput.Length; i++)
                    {
                        switch (movementInput[i])
                        {
                            case 'M':
                                MoveRobot();
                                break;
                            case 'R':
                                RotateRobot('R');
                                break;
                            case 'L':
                                RotateRobot('L');
                                break;
                            default:
                                break;
                        }
                    }

                    Console.WriteLine($"{CurrentRobot.X} {CurrentRobot.Y} {CurrentRobot.FaceDirection.Key}");

                    CurrentRobot = null;
                }
            }

            return;
        }

        static void RotateRobot(char direction)
        {
            Direction currentDirection = Directions.FirstOrDefault(w => w.Key == CurrentRobot.FaceDirection.Key);

            if (currentDirection == null)
                return;

            if (direction == 'R')
            {
                currentDirection.Value++;
            }
            else if (direction == 'L')
            {
                currentDirection.Value--;
            }

            CurrentRobot.FaceDirection = Directions.FirstOrDefault(w => w.Value == ((Directions.Count + currentDirection.Value) % Directions.Count));
        }

        static void MoveRobot()
        {
            Coordinate destinationCoordinate = new Coordinate
            {
                X = CurrentRobot.X + CurrentRobot.FaceDirection.MovementVector.X,
                Y = CurrentRobot.Y + CurrentRobot.FaceDirection.MovementVector.Y,
            };

            //Check world boundries
            if (destinationCoordinate.X >= 0 && destinationCoordinate.Y >= 0
                && destinationCoordinate.X <= UpperRightCoordinates.X && destinationCoordinate.Y <= UpperRightCoordinates.Y)
            {
                CurrentRobot.X = destinationCoordinate.X;
                CurrentRobot.Y = destinationCoordinate.Y;
            }
        }
    }
}
