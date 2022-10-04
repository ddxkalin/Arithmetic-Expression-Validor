namespace ArithmeticExpressionParser.Model
{
   public class Add : IErrorExpression
    {
        private readonly IErrorExpression _leftSide;
        private readonly IErrorExpression _rightSide;

        public Add(IErrorExpression leftSide, IErrorExpression ightSide)
        {
            _leftSide = leftSide;
            _rightSide = ightSide;
        }

        public double Evaluate()
        {
            return _leftSide.Evaluate() + _rightSide.Evaluate();
        }

        public override string ToString()
        {
            return "(" + _leftSide.ToString() + "+" + _rightSide.ToString() + ")";
        }
    }
}