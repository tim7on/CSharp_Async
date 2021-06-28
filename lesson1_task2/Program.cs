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
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key");
            Console.ReadLine();
            
            ThreadJar threadJar = new ThreadJar(new Action<object>(WriteChar));
            threadJar.Start('*');

            ThreadJar threadJar1 = new ThreadJar(new Action<object>(WriteChar));
            threadJar1.Start('!');

/*            for (int i = 0; i < 80; i++)
            {
                Console.Write('▌');
                Thread.Sleep(150);
            }*/

            threadJar.Wait();
            threadJar1.Wait();
            Console.WriteLine("\nFinished");
        }

        private static void WriteChar(object arg)
        {
            char item = (char)arg;

            for (int i = 0; i < 160; i++)
            {
                Console.Write(item);
                Thread.Sleep(100);
            }
        }

    }
}
