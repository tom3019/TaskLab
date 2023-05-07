namespace TaskLab;

public class TaskLab3
{
    public Task<string> 補充與練習1_自訂Task物件並加上狀態(string testMessage)
    {
        var taskCompletionSource = new TaskCompletionSource<string>();

        if (testMessage == "error")
        {
            taskCompletionSource.SetException(
                new List<Exception>
                {
                    new ArgumentException()
                });
            return taskCompletionSource.Task;
        }

        if (testMessage == "Canceled")
        {
            taskCompletionSource.SetCanceled();
            return taskCompletionSource.Task;
        }

        taskCompletionSource.SetResult(testMessage);
        return taskCompletionSource.Task;
    }
}