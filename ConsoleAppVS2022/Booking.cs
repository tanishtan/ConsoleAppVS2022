using System;
using System.Collections.Generic;

class Booking
{
    public static void  Test()
    {
        var theater = new Theater();
        while (true)
        {
            Console.Clear();
            theater.DisplaySeats();
            Console.WriteLine("\nBlock Seat(s) ");
            string input = Console.ReadLine();
            if (input.ToUpper() == "Q") break;
            theater.BlockSeats(input);
        }
    }
}

class Theater
{
    Dictionary<char, List<char>> seats = new Dictionary<char, List<char>>();

    public Theater()
    {
        for (char row = 'A'; row <= 'J'; row++)
        {
            seats[row] = new List<char>();
            int seatCount = row <= 'C' ? 20 : 25;
            for (int i = 0; i < seatCount; i++)
            {
                seats[row].Add('A');
            }
        }
    }

    public void DisplaySeats()
    {
        foreach (var row in seats)
        {
            Console.Write(row.Key + "\t");
            foreach (var seat in row.Value)
            {
                Console.Write(seat + " ");
            }
            Console.WriteLine();
        }
    }

    public List<(char, int)> FindRandomSeats(int numSeats)
    {
        List<(char, int)> availableSeats = new List<(char, int)>(); // Collect available seats

        // Find all available seats
        foreach (var row in seats)
        {
            for (int seat = 0; seat < row.Value.Count; seat++)
            {
                if (row.Value[seat] != 'B')
                {
                    availableSeats.Add((row.Key, seat));
                }
            }
        }

        // Implement random selection logic (e.g., reservoir sampling, shuffling)
        // Assuming you have a function `RandomlySelectKFromN(List<T> list, int k)`

        if (availableSeats.Count < numSeats)
        {
            Console.WriteLine("Not enough consecutive seats available. Assigning random seats...");
            return RandomlySelectKFromN(availableSeats, numSeats);
        }
        else
        {
            Console.WriteLine("Consecutive seats available. No need for random allocation.");
            return new List<(char, int)>(); // Return an empty list to indicate no random allocation
        }
    }

    public static List<T> RandomlySelectKFromN<T>(List<T> list, int k)
    {
        if (k > list.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(k), "k cannot be greater than the number of elements in the list.");
        }

        var result = new List<T>();
        for (int i = 0; i < k; i++)
        {
            result.Add(list[i]);
        }

        Random random = new Random();
        for (int i = k; i < list.Count; i++)
        {
            int j = random.Next(0, i + 1);
            if (j < k)
            {
                result[j] = list[i];
            }
        }

        return result;
    }



    public void BlockSeats(string input)
    {
        var seatRanges = input.Split(',');
        foreach (var seatRange in seatRanges)
        {
            bool anyBlocked = false; // Flag to track if any seats are blocked

            var seatsToBlock = seatRange.Split("..");
            
            char startRow = seatsToBlock[0][0];
            int startSeat = int.Parse(seatsToBlock[0].Substring(1)) - 1;

            if (seatsToBlock.Length == 1)
            {
                if (startSeat < seats[startRow].Count && seats[startRow][startSeat] != 'B')
                {
                    seats[startRow][startSeat] = 'B';
                }
                else
                {
                    anyBlocked = true;
                }
            }
            else
            {
                char endRow = seatsToBlock[1][0];
                int endSeat = int.Parse(seatsToBlock[1].Substring(1)) - 1;

                for (char row = startRow; row <= endRow; row++)
                {
                    for (int seat = startSeat; seat <= endSeat && seat < seats[row].Count; seat++)
                    {
                        if (seats[row][seat] != 'B')
                        {
                            seats[row][seat] = 'B';
                        }
                        else
                        {
                            anyBlocked = true;
                        }
                    }
                }
            }

            if (anyBlocked)
            {
                Console.WriteLine("Some seats in the requested range are already blocked. Continue blocking this range (Y/N)?");
                string choice = Console.ReadLine().ToUpper();
                if (choice != "Y")
                {
                    
                    break; // Exit the loop if user doesn't want to continue blocking
                }
            }
        }
    }
}
