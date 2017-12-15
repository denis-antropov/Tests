namespace Workers.ViewModels.Interfaces
{
    using Workers.BusinessLogic;

    public interface IWorkerItemFactory
    {
        IWorkerItem Create(IWorker w);
    }
}