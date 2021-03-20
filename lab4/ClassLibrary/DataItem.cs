using System.Numerics;
namespace ClassLibrary
{
    public struct DataItem
    {
        public Vector2 vect { get; set; }
        public Complex compl { get; set; }

        public DataItem(Vector2 v, Complex c)
        {
            vect = v;
            compl = c;
        }
        public override string ToString()
        {
            return $"Vector2 = {vect}; Complex = {compl}";
        }
        public string ToString(string format)
        {
            return $"Vector2 = {vect.ToString(format)}; Complex = {compl.ToString(format)}; Absolute Value = {compl.Magnitude.ToString(format)}";
        }
    }
}
