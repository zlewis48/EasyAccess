using EasyAccess.Data;

namespace EasyAccess.Services
{
    public interface ITaskService
    {
        Task<List<TaskModel>> GetTasks();
        void AddTask(TaskModel task);
        void UpdateTask(TaskModel task);
        void DeleteTask(TaskModel task);
    }

    public class TaskService : ITaskService
    {
        private readonly List<TaskModel> _tasks;

        public TaskService()
        {
            _tasks = new List<TaskModel>();
        }

        public Task<List<TaskModel>> GetTasks()
        {
            return Task.FromResult(_tasks);
        }

        public void AddTask(TaskModel task)
        {
            _tasks.Add(task);
        }

        public void UpdateTask(TaskModel task)
        {
            var existingTask = _tasks.FirstOrDefault(t => t.Id == task.Id);
            if (existingTask != null)
            {
                existingTask.Name = task.Name;
                existingTask.Details = task.Details;
                existingTask.UpdatedDate = DateTime.Now;
            }
        }

        public void DeleteTask(TaskModel task)
        {
            _tasks.Remove(task);
        }
    }

}
