namespace Workers.ViewModels
{
    using Workers.BusinessLogic.Interfaces;

    /// <summary>
    /// Represents a worker modifier which allows to modify worker instance
    /// </summary>
    public interface IWorkerModifier
    {
        /// <summary>
        /// Modifies worker instance
        /// </summary>
        /// <param name="worker">Worker instance</param>
        /// <returns>True, if worker is modified and saved; otherwise - false</returns>
        bool Modify(IWorker worker);
    }
}