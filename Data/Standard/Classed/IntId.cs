using Data.Standard.Interfaces;

namespace Data.Standard.Classed
{
    public class IntId : IIdentify<int>
    {

        public IntId(int value)
        {
            _value = value;
        }

        private int _value;

        public int Value()
        {
            return _value;
        }

        public void Set(int value)
        {
            _value = value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is int)) return false;
            var objInt = (int) obj;
            return objInt == Value();
        }

        public bool Equals(IntId other)
        {
            return _value == other._value;
        }

        public override int GetHashCode()
        {
            return _value;
        }

    }
}