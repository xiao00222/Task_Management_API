using System.ComponentModel.DataAnnotations;
namespace Task_Management_API.Models
{
    public class TaskTemplate
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }=string.Empty;
      public bool IsCompleted { get; set; }
    }
}
