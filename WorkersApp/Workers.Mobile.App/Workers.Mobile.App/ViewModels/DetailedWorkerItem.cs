namespace Workers.ViewModels
{
    using Workers.BusinessLogic;
    using Workers.ViewModels.Interfaces;

    public class DetailedWorkerItem : WorkerItem, IWorkerItem
    {
        public DetailedWorkerItem(IWorker worker) : base(worker)
        {
        }

        public string FullName => $"{Name} {Surname}";

        void IWorkerItem.Refresh()
        {
            Refresh();

            RaisePropertyChanged(nameof(FullName));
        }
    }
}
