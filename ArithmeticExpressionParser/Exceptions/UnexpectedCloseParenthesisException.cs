namespace ArithmeticExpressionParser.Exceptions
{
    public class UnexpectedCloseParenthesisException : ParsingException
    {
        public UnexpectedCloseParenthesisException(string expression, int ind) :
            base("Unexpected close parenthesis: ", expression, ind)
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