using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NSCC.Fees.Data;

namespace NSCC.Fees.Business.Classes
{
    public class AlphaCount
    {
        public string Alphabet { get; set; }
        public List<Program> Programs { get; set; }
    }
}