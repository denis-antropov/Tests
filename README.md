Tasks: 

1. It is required to create a queue with Push(T) and T Pop() methods. Methods can be called from different threads. Push method is a simple call which adds an item in the end. Pop method gets a fist item. If the queue is empty Pop method should wait a new item. This queues should be build based on a queue of BCL (System.Collections.Generic.Queue) . 

2. There are a collection of numbers and a special number X. It is required to print pairs of collection numbers, sum of each pair should be equals to X.

3. It is required to create an application which should communicate with a database. The database should contain a Worker table with columns: Id, Name, Surname, Birthdate, Sex, HasChildren.
    The application should contain next features:
    1) show list of workers
    2) adding a new worker
    3) editing an existent worker
    4) removing a worker

    Technical requirements:
    1) WPF (MVVM) - *(I added Xamarin project as well)*
    2) using of ORM (any)
    3) split application into layers (data layer, business logic, UI)
    4) clean code
