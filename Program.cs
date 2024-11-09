using System.Text.Json;
using System;
using System.Threading.Tasks;


internal class Program
{
    private static void Main(string[] args)
    {
        List<Task> tasks = new List<Task>();
        List<string> jtasks = new List<string>();
        string? input = Console.ReadLine();
        string[] inputs = input.Split(" ");
        while (true) 
        {

            try
            {
                switch (inputs[0])
                {
                    case "add":
                        AddTask(inputs[1]);
                        break;
                    case "list":
                        ListTask();
                        break;
                    default:
                        break;

                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }

             input = Console.ReadLine();
             inputs = input?.Split(" ");

            if (inputs[0].ToLower() == "exit")
            {
                break;
            }
        }

        
        void AddTask(string descrip)
            {

                var task = new Task
                {
                    ID = jtasks.Count+1,
                    description = descrip,
                    status = "todo",
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now
                };
                string jsonstr = JsonSerializer.Serialize(task, new JsonSerializerOptions { WriteIndented=true});
                
                if (jsonstr != null) {

                jtasks.Add(jsonstr);
                tasks.Add(task);
                }
                
            }
        void ListTask()
            {
              if (jtasks.Count == 0) { Console.WriteLine("No tasks yet"); }
              else { for (int i = 0; i < tasks.Count; i++) { Console.WriteLine(tasks[i].ID + " " + tasks[i].description); } }
        }
        void DeleteTask(int id)
        {

        }
    }
}
// Сделать функции update + delete
public class Task
{
    public int ID { get; set; }
    public string? description { get; set; }
    public string? status { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }

}