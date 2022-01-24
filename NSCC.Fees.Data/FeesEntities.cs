using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace NSCC.Fees.Data
{
    public partial class FeesEntities : DbContext
    {
        public FeesEntities(String connectionString)
            : base(connectionString)
        {

        }
    }
}
