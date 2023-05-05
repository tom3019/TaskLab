namespace TaskLab;

public class TaskLab1
{
    public async Task 情境1_馬上await()
    {
        //這時候有一個thread進來，簡稱thread:1
        Console.WriteLine("start thread:" + Environment.CurrentManagedThreadId);

        //因為上面的thread 並沒有做任何與task有關的事情所以不會分配其他線程，所以await A()會繼續使用thread:1
        //await A()代表給他一個thread 但是一定會等到thread回來，就算delay 20秒也不會釋放資源，所以與同步方法無異
        //馬上await 他會等到task的標籤為Completed、Faulted、Canceled才會結束這個子線程，並且讓資源回到主線程重新分配
        await A();

        //主線程在這會在這又分配一個子線程
        //await B()也是給他一個thread，並且也會等到thread回來，簡稱 thread 5
        await B();

        //等待B的thread結束完之後已經釋放thread 7，並且之後也沒有做任何與task有關的事情，所以重用，這時候出來是thread 7
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

        //因為Task.Run的關係所以額外分配一個thread ，簡稱thread 7
        await Task.Run(() =>
        {
            Console.WriteLine("Task.Run thread:" + Environment.CurrentManagedThreadId);
            Console.WriteLine("B");
        });
    }
}