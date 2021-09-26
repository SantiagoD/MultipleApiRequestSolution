using MultipleApiRequest.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleApiRequest.Creators
{
    /// <summary>
    /// Factory for the first company API class
    /// </summary>
    public class CompanyJsonApiCreator : CompanyApiCreator
    {
        public override ICompanyApi CreateCompanyApi()
        {
            return new Company1JsonApi();
        }
    }
}
