using System.Text.Json;
using System;
using System.Threading.Tasks;


internal class Program
{
    private static void Main(string[] args)
    {
        string filePath = "Y:\\Просто скрипты\\void c# project\\Tasks.json";
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

                Task task = new Task
                {
                    ID = jtasks.Count+1,
                    description = descrip,
                    status = "todo",
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now
                };
                string jsonstr = JsonSerializer.Serialize(task, new JsonSerializerOptions { WriteIndented=true});
                
                if (jsonstr != null) { jtasks.Add(jsonstr); File.WriteAllText(filePath, JsonSerializer.Serialize(jsonstr, new JsonSerializerOptions { WriteIndented = true })); }

        }
        void ListTask()
        {
            List<Task> list = new List<Task>();
            Task t = new Task();
                for (int i = 0; i < jtasks.Count; i++)
                {
                t = JsonSerializer.Deserialize<Task>(jtasks[i]);
                if (t != null) { list.Add(t); }
                }
              if (jtasks.Count == 0) { Console.WriteLine("No tasks yet"); }
              else { for (int i = 0; i < list.Count; i++) { Console.WriteLine(list[i].ID + " " + list[i].description); } }
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