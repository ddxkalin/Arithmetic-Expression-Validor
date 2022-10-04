namespace ArithmeticExpressionParser.Exceptions
{
    public class MissingArgumentException : ParsingException
    {
        public MissingArgumentException(string expression, int ind) :
            base("Missing argument: ", expression, ind)
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