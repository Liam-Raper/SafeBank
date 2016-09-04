using System.Collections.Generic;
using System.Linq;
using Data.Standard.Interfaces;
using Security.Interfaces.SecurityQuestions;

namespace Security.Classes.SecurityQuestions
{
    public class SecurityQuestions : ISecurityQuestions
    {

        private readonly IUnitOfWork _unitOfWork;

        public SecurityQuestions(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<string> GetAllSystemQuestions()
        {
            return _unitOfWork.SecurityQuestion.GetAll().Where(x => x.SystemDefault).Select(x => x.Text).ToArray();
        }
    }
}