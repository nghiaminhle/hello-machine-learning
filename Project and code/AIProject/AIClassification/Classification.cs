using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIClassification
{
    public class Classification
    {
        private IClassification _classificationEngine;
        private Data _dataset;

        public Classification(IClassification classificationObject, Data dataset)
        {
            this._classificationEngine = classificationObject;
            this._dataset = dataset;
        }
    }
}
