using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIClassification
{
    public class EduNumComparer : IComparer<Case>
    {
        int IComparer<Case>.Compare(Case c1, Case c2)
        {
            if (c1.EduNum > c2.EduNum)
                return 1;
            else if (c1.EduNum < c2.EduNum)
                return -1;
            return 0;
        }
    }
}
