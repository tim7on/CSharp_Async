using System;
using System.Threading;
/*Задание 2
Создайте проект по шаблону "Console Application". Создайте класс-обертку для работы с классом 
Thread. Класс-обертка, должен позволить выполнить экземпляр класса-делегата Action<object> 
в контексте потока, созданного классом Thread. Он должен быть наделен свойствами bool 
IsCompleted (для проверки на завершенность выполнения метода), bool IsSuccess (для проверки 
на успешность выполнения), bool IsFaulted (для проверки на провал выполнения) и Exception 
типа Exception (для получения исключения, которое произошло в контексте вторичного потока).
Реализуйте также методы Start и Wait. Метод Start будет запускать экземпляр класса-делегата 
Action<object> на выполнение в контексте потока Thread. Метод Wait будет усыплять поток,
который его вызвал, пока класс-делегат Action<object> не завершит свою работу. 
По завершению выполнения нужно присвоить свойству IsCompleted - true. Если выполнение
произошло без ошибок, свойству IsSuccess присвоить true, в противном случае - свойству 
IsFaulted присвоить true и в свойство Exception записать исключение. После создания класса
обертки повторить первое задание только с использованием своего класса-обертки.*/
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
