using System.Text.Json;
using System;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Runtime.Intrinsics.X86;

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
                        if (inputs.Length == 1) { ListTask(); }
                        else if (inputs.Length != 1) 
                        {
                            if (inputs[1] == "todo") { ListTODOTask(); }
                            if (inputs[1] == "done") { ListDoneTask(); }
                            if (inputs[1] == "in-progress") { ListInProgressTask(); }
                        }
                        break;
                    case "delete":
                        DeleteTask(inputs[1]);
                        break;
                    case "update":
                        UpdateTask(inputs[1], inputs[2]);
                        break;
                    case "mark-done":
                        MarkDone(inputs[1]);
                        break;
                    case "mark-in-progress":
                        MarkInProgress(inputs[1]);
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
        void ListDoneTask()
        {
            try
            {
                int cnt = 0;
                string jtask = File.ReadAllText(filePath);
                Task[] datalist = JsonSerializer.Deserialize<Task[]>(jtask);
                foreach (var data in datalist)
                {
                    if (data.status == "done")
                    {
                        cnt++;
                        Console.WriteLine(data.ID + " " + data.description);
                    }

                }
                if (cnt == 0)
                {
                    Console.WriteLine("No tasks with status 'done'");
                }
                
            }
            catch (Exception)
            {
                Console.WriteLine("No tasks with status 'done'");

            }
         }
        void ListTODOTask()
        {
            try
            {
                int cnt = 0;
                string jtask = File.ReadAllText(filePath);
                Task[] datalist = JsonSerializer.Deserialize<Task[]>(jtask);
                foreach (var data in datalist)
                {
                    if (data.status == "todo")
                    {
                        cnt++;
                        Console.WriteLine(data.ID + " " + data.description);
                    }

                }
                if (cnt == 0)
                {
                    Console.WriteLine("No tasks with status 'to do'");
                }

            }
            catch (Exception)
            {
                Console.WriteLine("No tasks with status 'to do'");

            }
        }
        void ListInProgressTask()
        {
            try
            {
                int cnt = 0;
                string jtask = File.ReadAllText(filePath);
                Task[] datalist = JsonSerializer.Deserialize<Task[]>(jtask);
                foreach (var data in datalist)
                {
                    if (data.status == "in-progress")
                    {
                        cnt++;
                        Console.WriteLine(data.ID + " " + data.description);
                    }

                }
                if (cnt == 0)
                {
                    Console.WriteLine("No tasks with status 'in-progress'");
                }

            }
            catch (Exception)
            {
                Console.WriteLine("No tasks with status 'in-progress'");

            }
        }
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
        void UpdateTask(string id, string newDesc)
        {
            try
            {
                for (int i = 0; i < jtasks.Count; i++)
                {
                    if (jtasks[i].ID == int.Parse(id))
                    {
                        jtasks[i].description = newDesc;
                    }
                }
                string jsonContent = JsonSerializer.Serialize(jtasks, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, jsonContent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        void MarkDone(string id)
        {
            try
            {
                for (int i = 0; i < jtasks.Count; i++)
                {
                    if (jtasks[i].ID == int.Parse(id))
                    {
                        jtasks[i].status = "done";
                    }
                }
                string jsonContent = JsonSerializer.Serialize(jtasks, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, jsonContent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        void MarkInProgress(string id)
        {
            try
            {
                for (int i = 0; i < jtasks.Count; i++)
                {
                    if (jtasks[i].ID == int.Parse(id))
                    {
                        jtasks[i].status = "in-progress";
                    }
                }
                string jsonContent = JsonSerializer.Serialize(jtasks, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, jsonContent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
// Исправить работу 3х новых функций
public class Task
{
    public int ID { get; set; }
    public string? description { get; set; }
    public string? status { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }

}