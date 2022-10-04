namespace ArithmeticExpressionParser.Model
{
    public class Negate : IExpression
    {
        private readonly IExpression _arg;

        public Negate(IExpression arg)
        {
            _arg = arg;
        }

        public double Evaluate()
        {
            return -_arg.Evaluate();
        }

        public override string ToString()
        {
            return "-" + _arg.ToString();
        }
    }
}
