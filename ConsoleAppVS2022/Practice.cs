using System;
using System.Collections.Generic;

class Practice
{
    public static void Test()
    {
        var theater = new Theaters();
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

class Theaters
{
    Dictionary<char, List<char>> seats = new Dictionary<char, List<char>>();

    public Theaters()
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

    public void BlockSeats(string input)
    {
        var seatRanges = input.Split(',');
        foreach (var seatRange in seatRanges)
        {
            var seatsToBlock = seatRange.Split("..");
            char startRow = seatsToBlock[0][0];
            int startSeat = int.Parse(seatsToBlock[0].Substring(1)) - 1;
            if (seatsToBlock.Length == 1)
            {
                if (startSeat < seats[startRow].Count)
                {
                    seats[startRow][startSeat] = 'B';
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
                        seats[row][seat] = 'B';
                    }
                }
            }
        }
    }
}
