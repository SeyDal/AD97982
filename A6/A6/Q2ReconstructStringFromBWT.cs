using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class Q2ReconstructStringFromBWT : Processor
    {
        public Q2ReconstructStringFromBWT(string testDataName) : base(testDataName)
        {
            ExcludeTestCaseRangeInclusive(30, 40);
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, String>)Solve);
        public int findNumber (List <char> text , int index)
        {
            int counter = 0;
            char character = text[index];
            for(int i=0; i<= index; i++)
            {
                if (text[i] == character)
                    counter++;
            }
            return counter;
        }
        public int findIndex(List<char> text, int index , char character)
        {
            int counter = 0;
            int result=0;
            for (int i=0; i < text.Count; i++)
            {
                if (text[i] == character)
                    counter++;
                if (counter == index)
                {
                    result = i;
                    break;
                }
            }
            return result;
        }
        public string Solve(string bwt)
        {
            
            List<char> bwtTmpList = bwt.ToList();
            List<char> bwtList = bwt.ToList();
            List<char> result = new List<char>();
            bwtList.Sort();
            int selectedIndex = 0;
            result.Add(bwt[selectedIndex]);
            for (int i=0; i < bwt.Length-1; i++)
            {
                int selectedIndexNumber = findNumber(bwtTmpList, selectedIndex);
                selectedIndex = findIndex(bwtList, selectedIndexNumber, bwtTmpList[selectedIndex]);
                result.Add(bwt[selectedIndex]);
            }
            result.Reverse();
            result .Add( '$');
            result.RemoveAt(0);
            return string.Join("", result);
        }
    }
}
