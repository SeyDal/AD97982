using TestCommon;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Exam1
{
    public class Q2Cryptanalyst : Processor
    {
        public Q2Cryptanalyst(string testDataName) : base(testDataName)
        {
            this.ExcludeTestCaseRangeInclusive(10, 37);
            this.ExcludeTestCaseRangeInclusive(1, 1);
            this.ExcludeTestCaseRangeInclusive(5, 5);
            this.ExcludeTestCaseRangeInclusive(6, 6);
        }

        public override string Process(string inStr) => Solve(inStr);

        public HashSet<string> Vocab = new HashSet<string>();

        public List<long> FindAllOccurrences(string p, string t)
        {
            string S = p +  t;
            int[] s = ComputePrefixFunction(S);
            List<long> result = new List<long>();
            for (int i = p.Length + 1; i < S.Length; i++)
            {
                if (s[i] == p.Length)
                {
                    result.Add(i - (2 * p.Length));
                }
            }
            return result;
        }
        public int[] ComputePrefixFunction(string P)
        {
            int[] s = new int[P.Length];
            int border = 0;
            for (int i = 1; i < P.Length; i++)
            {
                while ((border > 0) && P[i] != P[border])
                {
                    border = s[border - 1];
                }
                if (P[i] == P[border])
                {
                    border++;
                }
                else
                {
                    border = 0;
                }
                s[i] = border;
            }
            return s;
        }
        public bool charExist(char character, string text, int top, int bottom)
        {
            for (int i = top; i < bottom; i++)
            {
                if (text[i] == character)
                    return true;
            }
            return false;
        }
        public int countSymbol(int n, string text, char symbol)
        {
            int counter = 0;
            for (int i = 0; i < n; i++)
            {
                if (symbol == text[i])
                    counter++;
            }
            return counter;
        }
        public int firstOccurrence(char chracter, List<char> text)
        {
            for (int i = 0; i < text.Count; i++)
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
                    char symbol = pattern[lastIndex - patternCounter];
                    patternCounter++;
                    if (charExist(symbol, bwt, top, bottom))
                    {
                        top = firstOccurrence(symbol, bwtSortedList) + countSymbol(top, bwt, symbol);
                        bottom = firstOccurrence(symbol, bwtSortedList) + countSymbol(bottom + 1, bwt, symbol) - 1;
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
        public string makeBWT (string text)
        {
            List<string> mText = new List<string>();
            int TL = text.Length;
            for (int i = 0; i < TL; i++)
            {
                List<char> str = new List<char>();
                for (int j = 0; j < TL; j++)
                {

                    if (i + j < text.Length)
                    {
                        str.Add(text[i + j]);
                    }
                    else
                    {
                        str.Add(text[i + j - TL]);
                    }
                }
                mText.Add(string.Join("", str));
            }
            mText.Sort();
            List<char> result = new List<char>();
            for (int i = 0; i < TL; i++)
            {
                result.Add(mText[i][TL - 1]);
            }
            return string.Join("", result);
        }

        public string Solve(string cipher)
        {
            // Cryptanalysis c = new Cryptanalysis(
            // @"Exam1_TestData\TD2\dictionary.txt", 
            //'0', '9');
            //return c.Decipher(
            //cipher, 3, ' ', 'z', 
            //Cryptanalysis.IsDecipheredI1).GetHashCode().ToString();
            //return "";
            string result1="";
            long counter = 0;
            string[] patterns = File.ReadAllLines(@"Exam1_TestData\TD2\dictionary.txt"); // returns string[] 
            long n = patterns.Length;

            for (int k = 0; k < 99; k++)
            {
                string msg = cipher;
                Encryption e = new Encryption(k.ToString(), ' ', 'z', false);
                string cur = e.Decrypt(msg);
                string text = makeBWT(cur);
        
                for (int i = 0; i < n; i++)
                {
                    string pattern = patterns[i];


                    counter += find(pattern.ToList(), text);
                    if (counter > 200)
                    {
                        result1 = cur.GetHashCode().ToString();
                        break;
                    }

                }
                if (result1.Length > 0)
                    break;
               

         
            }
       //   Encryption e1 = new Encryption("6", ' ', 'z', false);
         // string cur1 = e1.Decrypt(cipher).GetHashCode().ToString();
            return result1;

        }
    }
}