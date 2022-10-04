using System;
using System.Text;

namespace ArithmeticExpressionValidator
{
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


    }
}

