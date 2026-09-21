using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        Thread thread1 = new Thread(() =>
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("---------LAST 20 PROCESSES---------");

                var processList = new List<Process>();

                foreach (var p in Process.GetProcesses())
                {
                    try
                    {
                        var time = p.StartTime;
                        processList.Add(p);
                    }
                    catch
                    {
                    }
                }

                var processes = processList
                    .OrderByDescending(p => p.StartTime)
                    .Take(20);

                foreach (var p in processes)
                {
                    Console.WriteLine($"{p.ProcessName} - {p.Id} - {p.StartTime}");
                }

                Thread.Sleep(500);
            }
        });

        thread1.Start();

        Thread thread2 = new Thread(() =>
        {
            while (true)
            {
                Console.WriteLine("Enter command: ");

                string command = Console.ReadLine();

                string[] parts = command.Split(' ');

                if (parts[0] == "Start")
                {
                    Process.Start(parts[1]);
                }
                else if (parts[0] == "Kill")
                {
                    foreach (var p in Process.GetProcessesByName(
                        Path.GetFileNameWithoutExtension(parts[1])))
                    {
                        p.Kill();
                    }
                }
            }
        });

        thread2.Start();
    }
}