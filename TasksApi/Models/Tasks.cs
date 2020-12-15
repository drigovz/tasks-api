using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasksApi.Models
{
    [Table("Tasks")]
    public class Tasks
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome da tarefa!")]
        [StringLength(100)]
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
    }
}