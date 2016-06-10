using System.Security.Cryptography.X509Certificates;

namespace Data.Standard.Interfaces
{
    public interface IValidate<in TDataSet>
    {
        bool Validate(TDataSet validate);
    }
}