/*
 * Fewest Guesses
 * Pawelski
 * 2/16/2022
 * This program plays a guessing game with the user. At
 * the end of the game, it checks whether the user has
 * the new "high score" by having the fewest guesses. This
 * is remembered by the computer after the program closes.
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FewestGuesses
{
    class Program
    {
        // Please update this file path!
        private const string FILE_PATH = @"";
        private const char DELIMITER = ',';

        static void Main(string[] args)
        {
            Random randomNumber = new Random();

            // Reads to old high score and splits it into variables.
            string[] fewestGuessesRecord = ReadFewestGuessesRecord();
            string fewestGuessesInitials = fewestGuessesRecord[0];
            int fewestGuesses = Convert.ToInt32(fewestGuessesRecord[1]);

            string repeat;
            do
            {
                int guess, guesses = 0;
                int mysteryNumber = randomNumber.Next(1, 101);
                Console.WriteLine("The current fewest guesses is " + fewestGuesses
                    + " by " + fewestGuessesInitials + ".");
                Console.WriteLine("I am thinking of a number between 1 and 100...");
                do
                {
                    Console.Write("Guess my number >> ");
                    guess = Convert.ToInt32(Console.ReadLine());
                    if (guess > mysteryNumber)
                    {
                        Console.WriteLine("Too high!");
                        guesses += 1;
                    }
                    else if (guess < mysteryNumber)
                    {
                        Console.WriteLine("Too low!");
                        guesses += 1;
                    }
                    else if (guess == mysteryNumber)
                    {
                        Console.WriteLine("You got my mystery number!");
                        Console.WriteLine("The number was " + mysteryNumber);
                        guesses += 1;
                    }
                    else
                    {
                        Console.WriteLine("That is outside the range!");
                    }
                } while (guess != mysteryNumber);
                
                // Updates the variables holding the fewest guesses.
                if (guesses < fewestGuesses || fewestGuessesInitials == "AAA")
                {
                    Console.WriteLine("Congratulations, you have the fewest guesses!");
                    Console.Write("Enter your initials >> ");
                    string intials = Console.ReadLine();
                    fewestGuesses = guesses;
                    fewestGuessesInitials = intials;
                }

                Console.Write("Do you want to play again? (Y/N) >> ");
                repeat = Console.ReadLine();
                Console.WriteLine();
            } while (repeat == "Y");

            // Writes the fewest guesses back to the file.
            WriteFewestGuessesRecord(fewestGuessesInitials, fewestGuesses);
        }

        /*
         * Reads the fewest guesses and initials from the file.
         * returns an array with the fields from the file
         */
        private static string[] ReadFewestGuessesRecord()
        {
            string record;
            FileStream file = new FileStream(FILE_PATH, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(file);
            record = reader.ReadLine();
            reader.Close();
            file.Close();
            return record.Split(DELIMITER);
        }

        /*
         * Writes the updated fewest guesses and intials to the file.
         * initials represents the entered initials of the person with the fewest guesses
         * guesses represents the amount of guesses made by the person
         */
        private static void WriteFewestGuessesRecord(string initials, int guesses)
        {
            FileStream file = new FileStream(FILE_PATH, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file);
            writer.WriteLine(initials + DELIMITER + guesses);
            writer.Close();
            file.Close();
        }
    }
}
