using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class Q4ConstructSuffixArray : Processor
    {
        public Q4ConstructSuffixArray(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) => 
            TestTools.Process(inStr, (Func<String, long[]>)Solve);

        public long[] Solve(string text)
        {
            int TL = text.Length;
            List<string> mText =new List<string>();
            for (int i = 0; i < TL; i++)
            {
                StringBuilder str = new StringBuilder();
                for (int j = i; j < TL; j++)
                {
                    str.Append(text[j]);
                }
                mText.Add(str.ToString());
            }
            mText.Sort();
            long[] result = new long[TL];
            for (int i=0; i < TL; i++)
            {
                result[i]=(TL- mText[i].Length);
            }
            return result;
        }
    }
}
