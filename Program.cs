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

        if (File.Exists(filePath))
        {
            string jsonContent = File.ReadAllText(filePath);
            jtasks = JsonSerializer.Deserialize<List<Task>>(jsonContent) ?? new List<Task>();
        }
        else
        {
            jtasks = new List<Task>();
        }


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
                    case "delete":
                        DeleteTask(inputs[1]);
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
            try
            {
                string jtask = File.ReadAllText(filePath);
                Task[] datalist = JsonSerializer.Deserialize<Task[]>(jtask);
                foreach (var data in datalist)
                {
                    Console.WriteLine(data.ID + " " + data.description);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("No tasks yet");
               
            }
        
        }
        /*void DeleteTask(string id)
        {   int Id = int.Parse(id);
            jtasks.RemoveAt(Id-1);
            if (Id == 1) 
            {
                for (int i = 0; i < jtasks.Count; i++)
                {
                    jtasks[i].ID -=1;
                }
            }
            else if (Id > 1 && Id != jtasks.Count) 
            {
                for (int i = Id-1; i < jtasks.Count; i++)
                {
                    jtasks[i].ID -= 1;
                }
            }
            string jsonContent = JsonSerializer.Serialize(jtasks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonContent);
        }*/
        void DeleteTask(string id)
        {
            if (!int.TryParse(id, out int Id) || Id <= 0 || Id > jtasks.Count)
            {
                Console.WriteLine("Invalid task ID");
                return;
            }

            // Удаляем задачу по ID (Id-1, так как индексация с 0)
            jtasks.RemoveAt(Id - 1);

            // Перенумерация оставшихся задач
            for (int i = 0; i < jtasks.Count; i++)
            {
                jtasks[i].ID = i + 1;
            }

            try
            {
                // Запись обновленных данных в файл
                string jsonContent = JsonSerializer.Serialize(jtasks, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, jsonContent);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error writing to file: " + ex.Message);
            }
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