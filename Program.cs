using System.Text.Json;
using System;
using System.Threading.Tasks;


internal class Program
{
    private static void Main(string[] args)
    {
        List<string> tasks = new List<string>();
        while (true)
        {
            
            if (args.Length >= 0)
            {
                args.Append(Console.ReadLine());
            }

            try
            {
                if (args[0] == "add")
                {
                    AddTask(args[1]);
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }

            Console.WriteLine(tasks.Count);
            ListTask();

            /*if (args[0].ToLower() == "exit")
            {
                break;
            }*/
        }
        

        void AddTask(string descrip)
            {

                var task = new Task
                {
                    ID = tasks.Count+1,
                    description = descrip,
                    status = "todo",
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now
                };
                string jsonstr = JsonSerializer.Serialize(task);
                if (jsonstr != null) {

                tasks.Add(jsonstr);

                }
                
            }
        void ListTask()
            {
                try
                {
                    for (int i = 0; i < tasks.Count; i++) { Console.WriteLine(tasks[i]); }

                }
                catch (Exception e)
                {
                    Console.WriteLine("No tasks yet" + e);
                    
                }

        }
        
    }
}
// Сделать пару функций и подключить гит
public class Task
{
    public int ID { get; set; }
    public string? description { get; set; }
    public string? status { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }

}