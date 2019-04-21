using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class Q2CunstructSuffixArray : Processor
    {
        public Q2CunstructSuffixArray(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long[]>)Solve);
        public long [] SortCharacters(string s)
        {
            Dictionary<char, int> S = new Dictionary<char, int>();
            S['$'] = 0;
            S['A'] = 1;
            S['C'] = 2;
            S['G'] = 3;
            S['T'] = 4;
            long[] order = new long[s.Length];
            long[] count = new long[5];


            for (int i=0; i < s.Length ; i++)
            {
                count[S[s[i]]]++;
            }
            for (int j=1; j < 5; j++)
            {
                count[j] += count[j - 1];

            }
            for (int i = s.Length - 1; i > -1; i--)
            {
                count[S[s[i]]]--;
                order[count[S[s[i]]]] = i;
            }
            return order;
        }
        public long[] ComputeCharClasses(string s,long[] order)
        {
            long[] classV = new long[s.Length];
            for (int i=1; i< s.Length; i++)
            {
                if (s[(int)order[i]] != s[(int)order[i - 1]])
                {
                    classV[order[i]] = classV[order[i-1]] + 1;
                }
                else
                {
                    classV[order[i]] = classV[order[i - 1]] ;
                }
            }
            return classV;
        }


        public long[] SortDoubled(string s,long l,long[] order,long[] classV)
        {
            long[] count = new long[s.Length];
            long[] newOrder = new long[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                count[i]=0;
            }
            for (int i = 0; i < s.Length; i++)
            {
                count[classV[i]]++;
            }
            for (int i = 1; i < s.Length; i++)
            {
                count[i] += count[i - 1];
            }
            for (int i = s.Length-1; i>-1; i--)
            {
                long start = (order[i] - l + s.Length) % s.Length;
                long cl = classV[start];
                count[cl]--;
                newOrder[count[cl]] = start;
            }
            return newOrder;
        }
        public long[] UpdateClasses(long[] newOrder ,long[] classV , long l)
        {
            int n = newOrder.Length;
            long[] newClass = new long[n];
            newClass[newOrder[0]] = 0;
            for (int i=1; i < n; i++)
            {
                long cur = newOrder[i];
                long prev = newOrder[i - 1];
                long mid = cur + l;
                long midPrev = (prev + l) % n;
                if(classV[cur]!=classV[prev] || classV[mid]!=classV[midPrev])
                {
                    newClass[cur] = newClass[prev] + 1;

                }
                else
                {
                    newClass[cur] = newClass[prev] ;

                }
            }
            return newClass;
        }
        

        private long[] Solve(string text)
        {
            long[] order = SortCharacters(text);
            long[] classV = ComputeCharClasses(text, order);
            long l = 1;
            while (l < text.Length)
            {
                order = SortDoubled(text, l, order, classV);
                classV = UpdateClasses(order, classV, l);
                l = l * 2;

            }
            return order;
        }
    }
}
