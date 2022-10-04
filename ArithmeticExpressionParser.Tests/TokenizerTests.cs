namespace ArithmeticExpressionParser.Tests
{
    public class TokenizerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TokenRecognitionTest()
        {
            var tokenizer = new Tokenizer("-($)" +
                                          "  228 \t + -1 +       " +
                                          "11 / (2) *  " +
                                          " (2 - " +
                                          "1)  \t  " +
                                          " & ^ + 2..3   /  2.1337");
            var expectedTokens = new List<Token>
            {
                Token.Begin, Token.Negate, Token.OpenParenthesis, Token.Error, Token.CloseParenthesis,
                Token.Error, Token.Number, Token.Add, Token.Number,
                Token.Add, Token.Number, Token.Divide, Token.OpenParenthesis, Token.Number,
                Token.CloseParenthesis, Token.Multiply, Token.OpenParenthesis, Token.Number, Token.Subtract,
                Token.Number, Token.CloseParenthesis,
                Token.Error, Token.Error,
                Token.Add, Token.Number,
                Token.Divide, Token.Number,
                Token.End
            };

            foreach (var expected in expectedTokens)
            {
                var curToken = tokenizer.GetCurrentToken();

                Assert.AreEqual(expected, curToken);
                tokenizer.GetNextToken();
            }
        }


        [Test]
        public void ErrorsRecognitionTest()
        {
            var tokenizer1 = new Tokenizer("   & \t ");
            Assert.AreEqual(Token.Begin, tokenizer1.GetCurrentToken());
            tokenizer1.GetNextToken();
            Assert.AreEqual(Token.Error, tokenizer1.GetCurrentToken());

            var tokenizer2 = new Tokenizer("  ) ");
            Assert.AreEqual(Token.Begin, tokenizer2.GetCurrentToken());
            tokenizer2.GetNextToken();
            Assert.AreEqual(Token.Error, tokenizer2.GetCurrentToken());

            var tokenizer3 = new Tokenizer("2..3");
            Assert.AreEqual(Token.Begin, tokenizer3.GetCurrentToken());
            tokenizer3.GetNextToken();
            Assert.AreEqual(Token.Number, tokenizer3.GetCurrentToken());

            var tokenizer4 = new Tokenizer("-");
            Assert.AreEqual(Token.Begin, tokenizer4.GetCurrentToken());
            tokenizer4.GetNextToken();
            Assert.AreEqual(Token.Error, tokenizer4.GetCurrentToken());
        }
    }
}