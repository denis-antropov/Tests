using System;
using Workers.BusinessLogic.Interfaces;

namespace Workers.ViewModels
{
    public interface IWorkerModifier
    {
        bool Modify(IWorker worker);
    }
}