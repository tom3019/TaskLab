namespace TaskLab;

public class TaskLab2
{
    public async Task 情境2_之後await()
    {
        //這時候有一個thread近來，簡稱thread:1
        Console.WriteLine("start thread:" + Environment.CurrentManagedThreadId);

        //因為上面的thread 並沒有做任何事情所以執行A時也是重用thread:1
        var aTask =  A();

        //這也就是在Task上所謂的標籤
        if (aTask.IsCompleted)
        {
            await aTask;
        }
        else
        {
            // 如果尚未完成，可以在這裡其他的事情
            //當然可以使用Task.WhenAll一起執行然後等最後的task完成，這裡只是示範標籤
        }


        //這時候a會被執行並加上未完成的標籤，並把thread:1釋放給別人， 這邊也是所說的釋放資源
        //所以這時候bTask 也是thread:1，也代表一個thread同時執行2個方法，而不是兩個不同thread
        var bTask =  B();

        //await時會使用其他thread來等待，所以並不一定是thread:1
        await aTask;
        await bTask;

        //等待B的thread結束完之後已經釋放thread 5，所以重用，這時候出來是thread 5
        Console.WriteLine("end thread:" + Environment.CurrentManagedThreadId);
        Console.ReadLine();
    }

    private async Task A()
    {
        Console.WriteLine("a thread:" + Environment.CurrentManagedThreadId);
        await Task.Delay(1000);
        Console.WriteLine("A");
    }

    private async Task B()
    {
        Console.WriteLine("b thread:" + Environment.CurrentManagedThreadId);

        //因為Task.Run的關係所以額外分配一個thread ，簡稱thread 5
        await Task.Run(() =>
        {
            Console.WriteLine("Task.Run thread:" + Environment.CurrentManagedThreadId);
            Console.WriteLine("B");
        });
    }
}