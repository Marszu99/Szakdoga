using System;
using TimeSheet.DataAccess;
using TimeSheet.Model;

namespace TimeSheet.Logic
{
    public class CompanyRepository
    {
        private ICompanyLogic _companyLogic;

        public CompanyRepository(ICompanyLogic companyLogic)
        {
            _companyLogic = companyLogic;
        }

        public Company GetCompany()
        {
            return _companyLogic.GetCompany();
        }
    }
}
