namespace ArithmeticExpressionParser.Tests
{
    public class EvaluateTests
    {
        private readonly List<Tuple<double, double>> _args = new List<Tuple<double, double>>
        {
            new Tuple<double, double>(0, 0), new Tuple<double, double>(-1, 0),
            new Tuple<double, double>(1, 0),
            new Tuple<double, double>(double.NegativeInfinity, double.PositiveInfinity),
            new Tuple<double, double>(double.NegativeInfinity, 0),
            new Tuple<double, double>(double.NaN, 2), new Tuple<double, double>(double.Epsilon, 0),
            new Tuple<double, double>(double.Epsilon, 31)
        };

        private Random _random;

        [SetUp]
        public void Setup()
        {
            _random = new Random();
            for (var i = 0; i < 10; ++i)
            {
                var a = _random.NextDouble() + _random.Next();
                var b = _random.NextDouble() + _random.Next();
                _args.Add(new Tuple<double, double>(a, b));
            }
        }

        [Test]
        public void AddEvaluateTest()
        {
            foreach (var (a, b) in _args) Assert.AreEqual(a + b, new Add(new Number(a), new Number(b)).Evaluate());
        }

        [Test]
        public void SubtractEvaluateTest()
        {
            foreach (var (a, b) in _args) Assert.AreEqual(a - b, new Subtract(new Number(a), new Number(b)).Evaluate());
        }

        [Test]
        public void MultiplyEvaluateTest()
        {
            foreach (var (a, b) in _args) Assert.AreEqual(a * b, new Multiply(new Number(a), new Number(b)).Evaluate());
        }

        [Test]
        public void DivideEvaluateTest()
        {
            foreach (var (a, b) in _args) Assert.AreEqual(a / b, new Divide(new Number(a), new Number(b)).Evaluate());
        }

        [Test]
        public void NegateEvaluateTest()
        {
            foreach (var (a, b) in _args)
            {
                Assert.AreEqual(-a, new Negate(new Number(a)).Evaluate());
                Assert.AreEqual(-b, new Negate(new Number(b)).Evaluate());
            }
        }
    }
}