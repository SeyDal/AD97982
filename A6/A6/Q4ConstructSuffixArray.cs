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
            List<char> textList = text.ToList();
            for (int i = 0; i < TL; i++)
            {
                mText.Add(string.Join("", textList));
                textList.RemoveAt(0);
            }
            mText.Sort();
            List<long> result = new List<long>();
            for (int i=0; i < TL; i++)
            {
                result.Add(TL- mText[i].Length);
            }
            return result.ToArray();
        }
    }
}
