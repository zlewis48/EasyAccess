using System;
using System.ComponentModel.DataAnnotations;

namespace EasyAccess.Data
{
    public class TaskModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Details { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}