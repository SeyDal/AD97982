using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class Q1FindAllOccur : Processor
    {
        public Q1FindAllOccur(string testDataName) : base(testDataName)
        {
			this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, String, long[]>)Solve,"\n");
        public long[] FindAllOccurrences(string p,string t)
        {
            string S = p + "$" + t ;
            var s = ComputePrefixFunction(S);
            List<long> result = new List<long>();
            for (int i = p.Length ; i < S.Length; i++)
            {
                if (s[i] == p.Length)
                {
                    result.Add(i - (2 * p.Length));
                }
            }
            return result.ToArray();
        }
        public int[] ComputePrefixFunction(string P)
        {
            int[] s = new int[P.Length];
            int border = 0;
            for(int i=1 ; i < P.Length ; i++)
            {
                while ((border > 0 ) && P[i] != P[border])
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

        public long[] Solve(string text, string pattern)
        {
            List<long> result = new List<long>();
            long [] patterns=FindAllOccurrences(pattern, text);
            if (patterns.Length == 0)
                return new long[] { -1 };
            
            return patterns;

        }
    }
}
