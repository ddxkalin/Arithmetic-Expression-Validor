namespace ArithmeticExpressionParser.Model
{
    using System.Globalization;
    
    public class Number : IExpression
    {
        private readonly double _value;

        public Number(double val)
        {
            _value = val;
        }

        public double Evaluate()
        {
            return _value;
        }

        public override string ToString()
        {
            return _value.ToString(CultureInfo.InvariantCulture);
        }
    }
}