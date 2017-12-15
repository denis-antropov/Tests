namespace Workers.ViewModels
{
    using Workers.BusinessLogic;
    using Workers.ViewModels.Interfaces;

    public class DetailedWorkerItemFactory : IWorkerItemFactory
    {
        public IWorkerItem Create(IWorker w)
        {
            return new DetailedWorkerItem(w);
        }
    }
}
