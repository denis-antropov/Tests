namespace Workers.ViewModels
{
    using System;
    using Workers.BusinessLogic;

    /// <summary>
    /// Represents a worker modifier which allows to modify worker instance
    /// </summary>
    public interface IWorkerModifier
    {
        event EventHandler<ModificationStateEventArgs> ModificationFinished;

        /// <summary>
        /// Modifies worker instance
        /// </summary>
        /// <param name="worker">Worker instance</param>
        /// <returns>True, if worker is modified and saved; otherwise - false</returns>
        void Modify(IWorker worker);
    }

    public class ModificationStateEventArgs : EventArgs
    {
        public ModificationStateEventArgs(bool state, IWorker worker)
        {
            State = state;
            Worker = worker;
        }

        public bool State { get; }

        public IWorker Worker { get; }
    }
}