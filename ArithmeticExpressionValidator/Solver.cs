namespace ArithmeticExpressionValidator
{
    using ArithmeticExpressionParser.Parser;
    using ParserResult =
        System.Tuple<double, ArithmeticExpressionParser.Model.IExpression,
            System.Collections.Generic.List<ArithmeticExpressionParser.Exceptions.ParsingException>>;

    /// <summary>
    /// Here is the place where our Solver is working with the thread and the queue. With the ParserResult
    /// tuple, we are sending the result and after that, we are disposing the process.
    /// </summary>
    internal class Solver : IDisposable
    {
        private readonly ExpressionParser _parser;
        private readonly Queue<Task> _tasks;
        private readonly Thread _worker;

        public Solver()
        {
            _parser = new ExpressionParser();
            _tasks = new Queue<Task>();
            _worker = new Thread(Run);
            _worker.Start();
        }

        public ParserResult SendAndEval(string s)
        {
            var result = new Result();
            lock (_tasks)
            {
                _tasks.Enqueue(new Task((() => { result.Eval(s, _parser); })));
                Monitor.PulseAll(_tasks);
            }

            return result.GetRes();
        }

        private class Result
        {
            private ParserResult _res;

            internal Result()
            {
                _res = null;
            }

            internal void Eval(string s, ExpressionParser parser)
            {
                lock (this)
                {
                    var (expression, errors) = parser.Parse(s);
                    _res = new ParserResult(expression.Evaluate(), expression, errors);
                    Monitor.Pulse(this);
                }
            }

            internal ParserResult GetRes()
            {
                lock (this)
                {
                    while (_res == null)
                    {
                        Monitor.Wait(this);
                    }
                }

                return _res;
            }
        }

        private void Eval()
        {
            Task task;

            lock (_tasks)
            {
                while (_tasks.Count == 0)
                {
                    Monitor.Wait(_tasks);
                }

                task = _tasks.Dequeue();
            }

            task.Start();
        }

        private void Run()
        {
            try
            {
                while ((Thread.CurrentThread.ThreadState & ThreadState.WaitSleepJoin) == 0)
                {
                    Eval();
                }
            }
            catch (ThreadInterruptedException)
            {
            }
            finally
            {
                Thread.CurrentThread.Interrupt();
            }
        }

        public void Dispose()
        {
            _worker.Interrupt();

            try
            {
                _worker.Join();
            }
            catch (ThreadInterruptedException)
            {
            }
        }
    }
}