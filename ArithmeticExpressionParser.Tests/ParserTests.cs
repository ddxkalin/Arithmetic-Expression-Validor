namespace ArithmeticExpressionParser.Tests
{
    using System.Globalization;
    using MathArithmeticalTestsPair = System.Tuple<string, System.Func<double, double, double, double>>;
    using WrongExpressionOrArgumentTests = System.Tuple<string, string>;

    public class ParserTests
    {
        private List<MathArithmeticalTestsPair> _mathArithmeticalTestsPair;
        private List<Tuple<string, string>> _wrongExpressionOrArgumentTests;

        private ExpressionParser _parser;
        private Random _random;

        [SetUp]
        public void Setup()
        {
            _random = new Random();
            _parser = new ExpressionParser();
            _mathArithmeticalTestsPair = new List<MathArithmeticalTestsPair>
            { 
                new MathArithmeticalTestsPair("228", (x, y, z) => 228),
                new MathArithmeticalTestsPair("x+y", (x, y, z) => x + y),
                new MathArithmeticalTestsPair("x-y", (x, y, z) => x - y),
                new MathArithmeticalTestsPair("x*y", (x, y, z) => x * y),
                new MathArithmeticalTestsPair("x/y", (x, y, z) => x / y),
                new MathArithmeticalTestsPair("0/x", (x, y, z) => 0 / x),
                new MathArithmeticalTestsPair("x/0", (x, y, z) => x / 0),
                new MathArithmeticalTestsPair("x /    -    y", (x, y, z) => x / -y),
                new MathArithmeticalTestsPair("x* \t \n  y", (x, y, z) => x * y),
                new MathArithmeticalTestsPair("--x--y--z", (x, y, z) => x + y + z),
                new MathArithmeticalTestsPair("x*z  +(-4-y   )/z", (x, y, z) => x * z + (-4 - y) / z),
                new MathArithmeticalTestsPair("-(-(-\t\t-5 + 23   *x*y) + 1 * z) -(((-11)))",
                    (x, y, z) => -(-(5 + 23 * x * y) + z) + 11),
                new MathArithmeticalTestsPair("x--y--z", (x, y, z) => x + y + z),
                new MathArithmeticalTestsPair("x/y/z", (x, y, z) => x / y / z),
                new MathArithmeticalTestsPair("(((((x + y + (-10*-z))))))", (x, y, z) => x + y + -10 * -z)
            };

            _wrongExpressionOrArgumentTests = new List<WrongExpressionOrArgumentTests>
            {
                new WrongExpressionOrArgumentTests("No first argument", " + 2"),
                new WrongExpressionOrArgumentTests("Missing argument", "- * 2"),
                new WrongExpressionOrArgumentTests("No second argument", " 2 + "),
                new WrongExpressionOrArgumentTests("No middle argument", " 2 +  + 3"),
                new WrongExpressionOrArgumentTests("No middle argument", " 2 + (2 +  + 4) + 3"),
                new WrongExpressionOrArgumentTests("No first argument", " 1 + (* 2 * 4) + 2"),
                new WrongExpressionOrArgumentTests("No last argument", " 1 + (5 * 2 * ) + 2"),
                new WrongExpressionOrArgumentTests("No last argument", " 1 + (5 * 2 * ) + 2"),
                new WrongExpressionOrArgumentTests("No open parenthesis", "2*2)"),
                new WrongExpressionOrArgumentTests("No close parenthesis", "(2*2"),
                new WrongExpressionOrArgumentTests("Unexpected symbol in the beginning", "&2*2"),
                new WrongExpressionOrArgumentTests("Unexpected symbol in the end", "2*2&"),
                new WrongExpressionOrArgumentTests("Unexpected symbol in the middle", "2*^2"),
                new WrongExpressionOrArgumentTests("Single plus", "+"),
                new WrongExpressionOrArgumentTests("Single minus", "-"),
                new WrongExpressionOrArgumentTests("Single unexpected symbol", "g"),
                new WrongExpressionOrArgumentTests("Empty expression", "(())"),
                new WrongExpressionOrArgumentTests("Wrong number", "2.,.,+4")
            };
        }

        [Test]
        public void CorrectMathArithmeticalTests()
        {
            foreach (var test in _mathArithmeticalTestsPair)
            {
                var x = _random.NextDouble() + _random.Next();
                var y = _random.NextDouble() + _random.Next();
                var z = _random.NextDouble() + _random.Next();

                var testString = new string(test.Item1);
                testString = testString.Replace("x", x.ToString(CultureInfo.InvariantCulture));
                testString = testString.Replace("y", y.ToString(CultureInfo.InvariantCulture));
                testString = testString.Replace("z", z.ToString(CultureInfo.InvariantCulture));

                var (item1, item2) = _parser.Parse(testString);
                Assert.IsTrue(item2.Count == 0);            //TODO: something is wrong here with the Assert
                Assert.AreEqual(test.Item2(x, y, z), item1.Evaluate());
            }
        }

        [Test]
        public void FailedWrongExpressionOrArgumentTests()
        {
            foreach (var (item1, item2) in _wrongExpressionOrArgumentTests)
            {
                var (_, exceptions) = _parser.Parse(item2);
                Assert.IsTrue(exceptions.Count != 0);
            }
        }
    }
}