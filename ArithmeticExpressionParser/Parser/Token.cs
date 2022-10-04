namespace ArithmeticExpressionParser.Parser
{
    public enum Token
    {
        Begin,
        End,
        Number,
        Negate,
        Add,
        Subtract,
        Multiply,
        Divide,
        OpenParenthesis,
        CloseParenthesis,
        Error
    }
}