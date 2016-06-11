using System.Collections.Generic;

namespace Security.Interfaces.SecurityQuestions
{
    public interface ISecurityQuestions
    {
        IEnumerable<string> GetAllSystemQuestions();
    }
}