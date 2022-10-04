namespace ArithmeticExpressionParser.Exceptions
{
    public class NumberParsingException : ParsingException
    {
        private readonly int _start;
        private readonly int _end;

        public NumberParsingException(string expression, int start, int end) :
            base("Couldn't parse number: ", expression, start)
        {
            _start = start;
            _end = end;
        }

        public override string ErrorDescription()
        {
            var res = "";
            res += Expression.Substring(0, _start);
            res += "{$}";
            res += Expression.Substring(_end);
            return res;
        }
    }
}