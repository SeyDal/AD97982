using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class Q3MatchingAgainCompressedString : Processor
    {
        public Q3MatchingAgainCompressedString(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) => 
        TestTools.Process(inStr, (Func<String, long, String[], long[]>)Solve);
        public bool charExist (char character , string text , int top , int bottom)
        {
            for (int i = top; i < bottom; i++)
            {
                if (text[i] == character)
                    return true;
            }
            return false;
        }
        public int countSymbol (int n , string text , char symbol)
        {
            int counter = 0;
            for (int i= 0; i<n ;i++)
            {
                if (symbol == text[i])
                    counter++;
            }
            return counter;
        }
        public int firstOccurrence(char chracter , List<char> text)
        {
            for (int i= 0; i< text.Count; i++)
            {
                if (text[i] == chracter)
                    return i;
            }
            return 0;
        }
        public long find(List<char> pattern, string bwt)
        {
            List<char> bwtSortedList = bwt.ToList();
            bwtSortedList.Sort();
            int top = 0;
            int bottom = bwt.Length - 1;
            int patternCounter = 0;
            while (top <= bottom)
            {
                
                if (pattern.Count != patternCounter)
                {
                    int lastIndex = pattern.Count - 1;
                    char symbol = pattern[lastIndex-patternCounter];
                    patternCounter++;
                    if (charExist(symbol, bwt, top, bottom))
                    {
                        top = firstOccurrence(symbol, bwtSortedList) + countSymbol(top, bwt,symbol);
                        bottom = firstOccurrence(symbol, bwtSortedList) + countSymbol(bottom + 1, bwt,symbol) - 1;
                    }
                    else
                        return 0;

                }
                else
                {
                    return bottom - top + 1;
                }
            }
            return 0;
        }

        public long[] Solve(string text, long n, String[] patterns)
        {
            long[] result = new long[n];
            for (int i = 0; i< n; i++)
            {
                result[i]=(find(patterns[i].ToList(),text));
            }
            return result;
        }
    }
}
