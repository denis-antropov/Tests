namespace Workers.ViewModels
{
    using Workers.BusinessLogic;
    using Workers.ViewModels.Interfaces;

    public class WorkerItemFactory : IWorkerItemFactory
    {
        public IWorkerItem Create(IWorker w)
        {
            return new WorkerItem(w);
        }
    }
}
