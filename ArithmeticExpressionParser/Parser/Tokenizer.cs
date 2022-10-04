namespace ArithmeticExpressionParser.Parser
{
    using ArithmeticExpressionParser.Exceptions;

    public class Tokenizer
    {
        private readonly List<ParsingException> _errors;
        private readonly string _expression;
        private Token _currentToken;
        private int _ind;
        private int _parenthesisBalance;

        private double _value;

        public Tokenizer(string expression)
        {
            _errors = new List<ParsingException>();
            _expression = expression;
            _ind = 0;
            _parenthesisBalance = 0;
            _currentToken = Token.Begin;
        }

        public int GetInd()
        {
            return _ind;
        }

        public string GetExpression()
        {
            return _expression;
        }

        public List<ParsingException> GetErrors()
        {
            return _errors;
        }

        public void AddException(ParsingException e)
        {
            _errors.Add(e);
        }

        public Token GetCurrentToken()
        {
            return _currentToken;
        }

        public Token GetNextToken()
        {
            NextToken();
            return _currentToken;
        }

        public double GetValue()
        {
            return _value;
        }

        private void SkipWhiteSpaces()
        {
            while (_ind < _expression.Length && char.IsWhiteSpace(_expression[_ind]))
            {
                _ind++;
            }
        }

        private string GetNumber()
        {
            var l = _ind;
            while (_ind < _expression.Length &&
                   (_expression[_ind] == '.' || char.IsDigit(_expression[_ind])))
            {
                _ind++;
            }

            var r = _ind;
            _ind--;
            return _expression.Substring(l, r - l);
        }

        private void CheckForArgument()
        {
            if (_currentToken != Token.OpenParenthesis && _currentToken != Token.Begin && !_currentToken.IsOperation())
            {
                return;
            }

            _errors.Add(new MissingArgumentException(_expression, _ind));
        }

        private bool CheckForOperation()
        {
            if (_currentToken != Token.CloseParenthesis && _currentToken != Token.Number)
            {
                return true;
            }

            _errors.Add(new MissingOperationException(_expression, _ind));
            return false;
        }

        private void NextToken()
        {
            if (_currentToken == Token.End)
            {
                return;
            }

            SkipWhiteSpaces();
            if (_ind >= _expression.Length)
            {
                CheckForArgument();
                _currentToken = Token.End;
                return;
            }

            var c = _expression[_ind];
            switch (c)
            {
                case '-':
                    if (_currentToken == Token.Number || _currentToken == Token.CloseParenthesis || _currentToken == Token.Error)
                    {
                        _currentToken = Token.Subtract;
                    }
                    else
                    {
                        if (_ind + 1 >= _expression.Length)
                        {
                            _errors.Add(new MissingArgumentException(_expression, _ind + 1));
                            _currentToken = Token.Error;
                        }
                        else if (char.IsDigit(_expression[_ind + 1]))
                        {
                            _ind++;
                            var begin = _ind;
                            var firstDigit = _expression[_ind];
                            var tmp = GetNumber();

                            try
                            {
                                _value = double.Parse("-" + tmp);
                            }
                            catch (FormatException)
                            {
                                _errors.Add(new NumberParsingException(_expression, begin, _ind + 1));
                                _value = firstDigit;
                            }

                            _currentToken = Token.Number;
                        }
                        else
                        {
                            _currentToken = Token.Negate;
                        }
                    }
                    break;

                case '+':
                    CheckForArgument();
                    _currentToken = Token.Add;
                    break;

                case '*':
                    CheckForArgument();
                    _currentToken = Token.Multiply;
                    break;

                case '/':
                    CheckForArgument();
                    _currentToken = Token.Divide;
                    break;

                case '(':
                    if (_currentToken == Token.CloseParenthesis || _currentToken == Token.Number)
                    {
                        _errors.Add(new UnexpectedOpenParenthesisException(_expression, _ind));
                    }
                    else
                    {
                        _parenthesisBalance++;
                        _currentToken = Token.OpenParenthesis;
                    }

                    break;

                case ')':
                    if (_currentToken == Token.OpenParenthesis || _currentToken.IsOperation())
                    {
                        _errors.Add(new MissingArgumentException(_expression, _ind));
                    }

                    if (_parenthesisBalance - 1 < 0)
                    {
                        _errors.Add(new UnexpectedCloseParenthesisException(_expression, _ind));
                        _currentToken = Token.Error;
                    }
                    else
                    {
                        _parenthesisBalance--;
                        _currentToken = Token.CloseParenthesis;
                    }

                    break;

                default:
                    if (char.IsDigit(c))
                    {
                        if (CheckForOperation())
                        {
                            var begin = _ind;
                            var tmp = GetNumber();
                            try
                            {
                                _value = double.Parse(tmp);
                            }
                            catch (FormatException)
                            {
                                _errors.Add(new NumberParsingException(_expression, begin, _ind + 1));
                                _value = int.Parse(c.ToString());
                            }

                            _currentToken = Token.Number;
                        }
                        else
                        {
                            _currentToken = Token.Error;
                        }
                    }
                    else
                    {
                        _errors.Add(new UnexpectedTokenException(_expression, _ind));
                        _currentToken = Token.Error;
                    }

                    break;
            }

            _ind++;
        }
    }
}
