using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class Q3PatternMatchingSuffixArray : Processor
    {
        public Q3PatternMatchingSuffixArray(string testDataName) : base(testDataName)
        {
            this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long, string[], long[]>)Solve,"\n");

        public List<long> FindAllOccurrences(string p, string t)
        {
            string S = p + "$" + t;
            int [] s = ComputePrefixFunction(S);
            List<long> result = new List<long>();
            for (int i = p.Length+1; i < S.Length; i++)
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

        private long[] Solve(string text, long n, string[] patterns)
        {
            List<long> result = new List<long>();
            List<long> resultT = new List<long>();


            for (int i=0; i<n; i++)
            {
                List<long> a =FindAllOccurrences(patterns[i], text).ToList();
                
                result.AddRange(a);
                
            }
           
            if (result.Count == 0)
                result.Add(-1);
            for(int i=0; i< result.Count; i++)
            {
                if (!resultT.Contains(result[i]))
                {
                    resultT.Add(result[i]);
                }
               
            }

            
            
             return resultT.ToArray() ;
        }
    }
}
