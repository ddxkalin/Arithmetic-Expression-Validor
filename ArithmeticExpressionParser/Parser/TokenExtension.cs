namespace ArithmeticExpressionParser.Parser
{
    public static class TokenExtension
    {
        public static bool IsOperation(this Token token)
        {
            return token == Token.Add || token == Token.Subtract ||
                   token == Token.Multiply || token == Token.Divide || token == Token.Negate;
        }
    }
}