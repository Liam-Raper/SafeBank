using Data.Standard.Interfaces;

namespace Data.Standard.Classed
{
    public class IntId : IIdentify<int>
    {

        private int _value;

        public int Value()
        {
            return _value;
        }

        public void Set(int value)
        {
            _value = value;
        }
    }
}