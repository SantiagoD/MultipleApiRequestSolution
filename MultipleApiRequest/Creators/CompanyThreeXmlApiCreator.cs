using MultipleApiRequest.Core;

namespace MultipleApiRequest.Creators
{
    /// <summary>
    /// Factory for the third company API class
    /// </summary>
    public class CompanyThreeXmlApiCreator : CompanyApiCreator
    {
        public override ICompanyApi CreateCompanyApi()
        {
            return new Company3XmlApi();
        }
    }
}
