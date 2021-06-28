using System;
using System.Threading;


namespace lesson1_task2
{
    internal class ThreadJar
    {
        private readonly Action<object> action;

        public bool IsSuccess { get; private set; } = false;
        public bool IsCompleted { get; private set; } = false;
        public bool IsFaulted { get; private set; } = false;
        public Exception Exception { get; private set; } = null;

        public ThreadJar(Action<object> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Start(object state)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadExe), state);
        }
        public void Wait()
        {
            while (!IsCompleted)
            {
                Thread.Sleep(150);
            }

            if (Exception != null)
            {
                throw Exception;
            }
        }
        private void ThreadExe(object state)
        {
            try
            {
                action.Invoke(state);
                IsSuccess = true;
            }
            catch (Exception ex)
            {
                Exception = ex;
                IsFaulted = true;
            }
            finally
            {
                IsCompleted = true;
            }

        }

    }
}
