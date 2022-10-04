namespace ArithmeticExpressionParser.Exceptions
{
    public class MissingOperationException : ParsingException
    {
        public MissingOperationException(string expression, int ind) :
            base("Missing operation: ", expression, ind)
        {
        }

        public override string ErrorDescription()
        {
            var res = "";
            res += Expression.Substring(0, ErrorIndex);
            res += "{$}";
            res += Expression.Substring(ErrorIndex);
            return res;
        }
    }
}