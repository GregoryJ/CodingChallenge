/* Copyright (c) 2015 Gregory Jerome - All Rights Reserved */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodingChallenge
{
    /// <summary>
    /// Step 1: Determine the total number of permutations of the word.
    /// Step 2: Determine the fraction of permutations occurring before the word
    ///         that has the same first letter as the input word.
    /// Step 3: Multiply the total permutations by the fraction of permutations to
    ///         get the rank of the word occurring before the first word with the
    ///         same first letter as the input word. Add this value to the current
    ///         value of rank.
    /// Step 4: Remove the first letter of the input word.
    /// Step 5: Repeat steps 1-4 until there are no letters left.
    /// Step 6: Add 1 to the value of rank to get the rank of the original input
    ///         word.
    /// </summary>
    class ProgramMyCode
    {
        private static double _rank;

        static void Main(string[] args)
        {
            while (true)
            {
                // Initialize class level variable on each loop.
                _rank = 0D;
                Console.WriteLine();
                Console.Write("Enter a word: ");
                string word = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(word))
                {
                    break;
                }
                // Trim white-space.
                word = word.Trim();
                Console.WriteLine();
                Stopwatch stopWatch = Stopwatch.StartNew();
                double rank = CalculateRank(word);
                // Use format specifier to prevent scientific notation.
                Console.WriteLine("Rank: {0:N}", rank);
                Console.WriteLine("Milliseconds to Execute: {0}", stopWatch.ElapsedMilliseconds);
            }
        }

        private static double CalculateRank(string word)
        {
            // Convert to upper case so the algorithm is case insensitive.
            word = word.ToUpper();

            // Calculate the factorial of the word's length. This represents the total number
            // of permutations of the word, including duplicate words resulting from a letter 
            // appearing more than once.
            Console.WriteLine("wordLength: {0}", word.Length);
            long wordLengthFactorial = Factorial(word.Length);
            Console.WriteLine("wordLengthFactorial: {0}", wordLengthFactorial);

            // To determine the total number of permutations not including duplicate words, 
            // we divide the factorial of the length of the word by the product of the 
            // factorials of the frequencies of each letter:
            //     (word size)! / (L1! * L2! * L3!... Ln!)
            //     (Where L1, L2, L3, etc. are the frequencies of each letter.)

            // First, determine the frequency of each letter in the word, i.e., how many times each
            // unique letter appears in the word. Store the results in a dictionary where
            // the letter is the key and the frequency is the value.
            Dictionary<char, int> charFrequencies = new Dictionary<char, int>();
            foreach (char letter in word)
            {
                // If the letter doesn't exist, add it and give it a frequency of 1.
                if (!charFrequencies.ContainsKey(letter))
                {
                    charFrequencies.Add(letter, 1);
                }
                else
                {
                    // If the letter exists, increment it's frequency by 1.
                    charFrequencies[letter]++;
                }
            }
            foreach (var chr in charFrequencies)
            {
                Console.WriteLine("Frequency of \"{0}\": {1}", chr.Key, chr.Value);
            }

            // Determine the product of the factorials of the frequencies of each letter.
            long charFrequencyFactorialsProduct = 1;
            foreach (char key in charFrequencies.Keys)
            {
                charFrequencyFactorialsProduct *= Factorial(charFrequencies[key]);
            }
            Console.WriteLine("charFactorialsProduct: {0}", charFrequencyFactorialsProduct);

            // To eliminate duplicate words, divide the factorial of the length of the word by the 
            // product of the factorials of the frequencies of each letter.
            long totalNumPermutations = wordLengthFactorial / charFrequencyFactorialsProduct;
            Console.WriteLine("totalNumPermutations: {0}", totalNumPermutations);

            // Calculate the fraction of total permutations ocurring before the first word that
            // has the same first letter as the input word.

            char currentFirstChar = word[0];
            int sublistCharFrequencies = 0;
            foreach (char key in charFrequencies.Keys)
            {
                if (key < currentFirstChar)
                {
                    sublistCharFrequencies += charFrequencies[key];
                }
            }

            // Determine fraction of total permutations by dividing by the word length.
            double fractionOfPermutations = (double)sublistCharFrequencies / word.Length;

            // This is the rank of the word just before the word with the same first
            // letter as the input word.
            _rank += totalNumPermutations * fractionOfPermutations;

            // Now that we have this value we can remove the first letter of the word and
            // compute the permutations of the new word based on its first letter.

            word = word.Substring(1, word.Length - 1);
            Console.WriteLine();
            if (word.Length > 0)
            {
                // If there are letters left, recurse into this method.
                return CalculateRank(word);
            }

            // Add 1 to get the final rank of the original input word.
            return _rank + 1;
        }

        private static long Factorial(long num)
        {
            long result = 0;

            if (num <= 1) { return 1; }

            checked
            {
                result = num * Factorial(num - 1);
            }

            return result;
        }
    }
}
