namespace ArithmeticExpressionParser.Model
{
    public class Multiply : IExpression
    {
        private readonly IExpression _leftSide;
        private readonly IExpression _rightSide;

        public Multiply(IExpression leftSide, IExpression rightSide)
        {
            _leftSide = leftSide;
            _rightSide = rightSide;
        }

        public double Evaluate()
        {
            return _leftSide.Evaluate() * _rightSide.Evaluate();
        }

        public override string ToString()
        {
            return "(" + _leftSide.ToString() + "*" + _rightSide.ToString() + ")";
        }
    }
}
