namespace ArithmeticExpressionParser.Exceptions
{
    public class UnexpectedTokenException : ParsingException
    {
        public UnexpectedTokenException(string expression, int ind) :
            base("Unexpected token: ", expression, ind)
        {
        }

        public override string ErrorDescription()
        {
            var res = "";
            res += Expression.Substring(0, ErrorIndex);
            res += "{$}";
            if (ErrorIndex + 1 < Expression.Length)
            {
                res += Expression.Substring(ErrorIndex + 1);
            }

            return res;
        }
    }
}