namespace Data.Standard.Interfaces
{
    public interface IIdentify<T>
    {
        T Value();
        void Set(T value);
    }
}