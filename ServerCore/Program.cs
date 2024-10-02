using System;

class ServerCore
{
    static void MainThread()
    {
        while (true)
            Console.WriteLine("Hello Thread!");
    }

    static void Main(string[] args)
    {
        Thread t = new Thread(MainThread);
        t.IsBackground = true;
        t.Start();
        Console.WriteLine("Hello World!");
    }
}
