using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Business.Model;

namespace TaskFlow.Business.Interface
{
    public interface ITaskRepository : IRepository<TaskModel>
    {
    }
}
