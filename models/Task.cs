using System.Text.Json.Serialization;

namespace Api_TodoList.Models;

public partial class Task
{
    public int Id { get; set; }

    public string Task1 { get; set; } = null!;

    public DateTime DateTask { get; set; } = DateTime.UtcNow;

    public int IdUser { get; set; }

    [JsonIgnore]
    public virtual User IdUserNavigation { get; set; } = null!;

    public Task()
    {
        // DateTask = DateTime.Now;
    }

    public Task(TaskDTO taskDTO)
    {
        Task1 = taskDTO.Task;
        IdUser = taskDTO.IdUser;
    }

}
