using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Business.Model
{
    public class TEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
