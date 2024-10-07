using System;

namespace ServerCore
{
    class SpintLock
    {
        volatile int _locked = 0;

        public void Acquire()
        {
            while (true)
            {
                int expected = 0;
                int desired = 1;
                if (Interlocked.CompareExchange(ref _locked, desired, expected) == expected)
                    break;

                // 쉬다 올게~

            }
        }

        public void Release()
        {
            _locked = 0;
        }
    }

    class Program
    {
        static int _num = 0;
        static SpintLock _lock = new SpintLock();

        static void Thread1()
        {
            for(int i = 0; i < 100000; i++)
            {
                _lock.Acquire();
                _num++;
                _lock.Release();
            }
        }

        static void Thread2()
        {
            for (int i = 0; i < 100000; i++)
            {
                _lock.Acquire();
                _num--;
                _lock.Release();
            }
        }

        static void Main(string[] args)
        {
            Task t1 = new Task(Thread1);
            Task t2 = new Task(Thread2);
            t1.Start();
            t2.Start();

            Task.WaitAll(t1, t2);

            Console.WriteLine(_num);
        }
    }
}
