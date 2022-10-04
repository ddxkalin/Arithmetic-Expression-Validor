namespace ArithmeticExpressionParser.Model
{
    public class ErrorExpression : IExpression
    {
        public double Evaluate()
        {
            return 0;
        }

        public override string ToString()
        {
            return "0";
        }
    }
}

