namespace ArithmeticExpressionParser.Model
{
   public class Add : IExpression
    {
        private readonly IExpression _leftSide;
        private readonly IExpression _rightSide;

        public Add(IExpression leftSide, IExpression ightSide)
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