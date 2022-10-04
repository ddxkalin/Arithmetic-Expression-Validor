namespace ArithmeticExpressionParser.Exceptions
{
    public abstract class ParsingException : Exception
    {
        protected readonly int ErrorIndex;
        protected readonly string Expression;

        protected ParsingException(string message, string expression, int ind) : base(message)
        {
            ErrorIndex = ind;
            Expression = expression;
        }

        public abstract string ErrorDescription();
    }
}