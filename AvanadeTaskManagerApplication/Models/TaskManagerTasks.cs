using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace AvanadeTaskManagerApplication.Models
{
    public class TaskManagerTasks
    {
     
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string title { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(400)")]
        public string description { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(20)")]
        public string category { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(20)")]
        public string priority { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(20)")]
        public string status { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(20)")]
        public string dueDate { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(20)")]
        public string createdAt { get; set; } = string.Empty;
    }
}
