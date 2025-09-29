using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exizent.CaseManagement.Client.Models
{
    public class EntitlementResourceRepresentation
    {
        public decimal? PecuniaryAmount { get; init; }
        public ResiduaryEntitlementType? ResiduaryType { get; init; }
        public decimal? ResiduaryAmount { get; init; }


    }
}
