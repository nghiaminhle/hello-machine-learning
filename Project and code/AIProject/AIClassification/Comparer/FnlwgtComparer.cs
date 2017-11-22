using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIClassification
{
    public class FnlwgtComparer: IComparer<Case>
    {
        int IComparer<Case>.Compare(Case c1, Case c2)
        {
            if (c1.Fnlwgt > c2.Fnlwgt)
                return 1;
            else if (c1.Fnlwgt < c2.Fnlwgt)
                return -1;
            return 0;
        }
    }
}
