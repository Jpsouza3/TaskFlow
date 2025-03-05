using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Business.Model
{
    [Table("Tasks")]
    public class TaskModel : TEntity
    {
        public string? Name { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
