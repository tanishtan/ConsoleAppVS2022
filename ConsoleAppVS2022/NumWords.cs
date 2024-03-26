using System;
using System.Collections.Generic;

public class NumWords
{
    private static readonly Dictionary<int, string> numbersBelow20 = new Dictionary<int, string>()
    {
        { 0, "zero" },
        { 1, "one" },
        { 2, "two" },
        { 3, "three" },
        { 4, "four" },
        { 5, "five" },
        { 6, "six" },
        { 7, "seven" },
        { 8, "eight" },
        { 9, "nine" },
        { 10, "ten" },
        { 11, "eleven" },
        { 12, "twelve" },
        { 13, "thirteen" },
        { 14, "fourteen" },
        { 15, "fifteen" },
        { 16, "sixteen" },
        { 17, "seventeen" },
        { 18, "eighteen" },
        { 19, "nineteen" }
    };

    private static readonly Dictionary<int, string> tens = new Dictionary<int, string>()
    {
        { 20, "twenty" },
        { 30, "thirty" },
        { 40, "forty" },
        { 50, "fifty" },
        { 60, "sixty" },
        { 70, "seventy" },
        { 80, "eighty" },
        { 90, "ninety" }
    };

    private static readonly string[] placeValues =
    {
        "", "thousand", "million"
    };

    public static string ConvertToWords(int number)
    {
        if (number == 0)
        {
            return "zero";
        }

        if (number < 0)
        {
            return "negative " + ConvertToWords(Math.Abs(number));
        }

        string words = "";
        int place = 0;

        while (number > 0)
        {
            // Extract the three digits for the current place
            int threeDigit = number % 1000;

            // Convert the three digits to words
            string threeDigitWords = ConvertThreeDigits(threeDigit);

            // Append the current place value if applicable
            if (threeDigitWords != "" && threeDigitWords != "zero")
            {
                words = (threeDigitWords + (placeValues[place] != "" ? " " + placeValues[place] : "")) + (words == "" ? "" : " " + words);
            }

            // Move to the next place value
            number /= 1000;
            place++;
        }

        return words.Trim();
    }

    private static string ConvertThreeDigits(int threeDigit)
    {
        if (threeDigit == 0)
        {
            return "";
        }

        string words = "";

        // Handle hundreds place
        if (threeDigit >= 100)
        {
            words += numbersBelow20[threeDigit / 100] + " hundred";
            threeDigit %= 100;

            // Add space after hundred if tens and ones are non-zero
            if (threeDigit > 0)
            {
                words += " ";
            }
        }

        // Handle tens and ones
        if (threeDigit >= 20)
        {
            words += tens[threeDigit / 10 * 10];
            threeDigit %= 10;

            if (threeDigit > 0)
            {
                words += "-" + numbersBelow20[threeDigit];
            }
        }
        else if (threeDigit > 0)
        {
            words += numbersBelow20[threeDigit];
        }

        return words;
    }
}


