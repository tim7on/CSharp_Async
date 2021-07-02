using System;
using System.Threading;
using System.Threading.Tasks;
/*Задание 2
Создайте проект по шаблону "Console Application". Создайте метод «private static void
WriteChar(object symbol)». В теле метода создайте цикл for, размерностью 160 итераций, который 
в своем теле с задержкой в пол секунды выводит на экран консоли значение параметра symbol,
приведенного к типу char. Вызовите метод WriteChar из метода Main в контексте задачи, 
передавая в качестве параметра значение "!". Все время, пока метод WriteChar выполняется, из 
метода Main выводите на экран консоли "$". Когда задача закончит свое выполнение выведите 
на экран консоли строку "Метод Main закончил свою работу"*/
namespace lesson2_task2
{
    class Program
    {
        delegate void ThreadOut();
        static void Main(string[] args)
        {
         
            ThreadOut threadOut=() => { WriteChar('!'); };
            Task task = new Task(new Action(threadOut));
            task.Start();

            while (!task.IsCompleted)
            {
                Console.Write('$');
                Thread.Sleep(500);
            }
            Console.WriteLine("\nМетод Main закончил свою работу");
        }

        private static void WriteChar(object arg)
        {
            char item = (char)arg;

            for (int i = 0; i < 160; i++)
            {
                Console.Write(item);
                Thread.Sleep(500);
            }
        }
    }
}
