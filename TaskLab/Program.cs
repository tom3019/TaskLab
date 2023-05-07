﻿using TaskLab;

// var taskLab1 = new TaskLab1();
// await taskLab1.情境1_馬上await();

// var taskLab2 = new TaskLab2();
// await taskLab2.情境2_之後await();

var taskLab3 = new TaskLab3();
var task = taskLab3.補充與練習1_自訂Task物件並加上狀態("error");
Console.WriteLine(task.IsFaulted);


/* 關於討論需要更正的點
 1. task pool 與 thread pool 是平等的並沒有上下之分，只是管理及運用thread的方式不同
 2. 一個程序一開始只有一個主線程，他會分出多個子線程也就是pool，在這些pool中的線程沒有主跟子之分
    gpt:
        一個程序一開始只有一個主線程，
        當執行到某個方法需要做一些耗時的操作時，
        為了不阻塞主線程的執行，
        可以使用非同步方法和多線程技術來讓耗時操作在另一個線程上執行，
        這些線程可以與主線程並行執行。在執行多線程時，每個線程都是平等的，
        沒有所謂的主線程或子線程的區分。所以，我們可以說一個程序可能包含多個線程，
        但是這些線程沒有所謂的主線程或子線程的區分，而是平等的線程。

3. 測試不能同時多個測試案例同時執行是因為測試框架的關係，單元測試框架一般會使用單一執行緒來執行測試，因此不同的測試會依序執行，而且也不會與其他執行緒同時執行
    也就代表一個方法執行完才會執行下一個方法，所以就算你在測試方法中沒有await 但是對他來說方法已經結束，也代表不會同時處理多個測試案例

 */
 /* 補充
 1. 平行處理(Parallel)與Task.Run的差別
    gpt:
        使用 Task.Run() 會將要執行的方法放入 Task 中，由 Task Scheduler 安排執行緒池中的執行緒去執行該方法。
        而使用平行處理則是在同一個方法中，使用 Parallel 類別或 PLINQ 執行多個操作，
        這些操作可能會被分配到不同的執行緒上並行執行。
        簡單來說，Task.Run() 適用於要執行的方法是 I/O 等待型的操作，可以讓執行緒池中的執行緒去處理其他的工作，
        當 I/O 操作完成後再分配一個執行緒去繼續處理該方法。而平行處理適用於要執行的多個操作之間相對獨立，可以同時執行而且並不會等待其他操作完成的情況。
  */