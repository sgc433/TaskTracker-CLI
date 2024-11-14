using System.Text.Json;
using System;
using System.Threading.Tasks;
using System.Net.Http.Json;
//using Newtonsoft.Json;

internal class Program
{
    private static void Main(string[] args)
    {
        string filePath = "Y:\\Просто скрипты\\void c# project\\Tasks.json";
        List<Task> jtasks = new List<Task>();
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
                jtasks.Add(task);
                string jsonstr = JsonSerializer.Serialize(jtasks, new JsonSerializerOptions { WriteIndented=true});

            if (jsonstr != null) { File.WriteAllText(filePath, jsonstr); }

               
        }
        void ListTask()
        {
            string jtask = File.ReadAllText(filePath);
            Task[] datalist = JsonSerializer.Deserialize<Task[]>(jtask);
            foreach (var data in datalist)
            {
                Console.WriteLine(data.ID + " " + data.description);
            }
            
            
        }
        void DeleteTask(int id)
        {

        }
    }
}
// Наладить работу всех функций с json файлом 
public class Task
{
    public int ID { get; set; }
    public string? description { get; set; }
    public string? status { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }

}