using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A11
{
    public class Q2FunParty : Processor
    {
        public Q2FunParty(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long,long[],long[][], long>)Solve);

        public virtual long Solve(long n, long [] funFactors, long[][] hierarchy)
        {
            throw new NotImplementedException();
        }
    }
}
