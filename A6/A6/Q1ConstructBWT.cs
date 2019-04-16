using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class Q1ConstructBWT : Processor
    {
        public Q1ConstructBWT(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, String>)Solve);

        public string Solve(string text)
        {
            List<string> mText = new List<string>();
            int TL = text.Length;
            for (int i=0; i < TL; i++)
            {
                List<char> str = new List<char>();
                for (int j = 0; j < TL; j++)
                {

                    if (i+j < text.Length)
                    {
                        str .Add( text[i + j]);
                    }
                    else
                    {
                        str .Add( text[i + j - TL]);
                    }
                }
                mText.Add(string.Join("", str));
            }
            mText.Sort();
            List<char> result = new List<char>();
            for (int i = 0; i < TL; i++)
            {
                result .Add( mText[i][TL - 1]);
            }
            return string.Join("", result);
        }
    }

    
}
