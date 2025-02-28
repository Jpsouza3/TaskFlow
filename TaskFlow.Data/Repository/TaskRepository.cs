using TaskFlow.Business.Interface;
using TaskFlow.Business.Model;
using TaskFlow.Data.Interface;
using TaskFlow.Data.Repository;

public class TaskRepository : Repository<TaskModel>, ITaskRepository
{
    public TaskRepository(IConnectionFactory connectionFactory)
        : base(connectionFactory)
    {
    }
}