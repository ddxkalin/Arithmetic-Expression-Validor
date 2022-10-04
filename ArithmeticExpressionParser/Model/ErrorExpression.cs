namespace ArithmeticExpressionParser.Model
{
    public class ErrorExpression : IErrorExpression
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

