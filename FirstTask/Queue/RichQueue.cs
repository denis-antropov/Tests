namespace Queue
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// Represents a queue with rich functionality
    /// </summary>
    /// <typeparam name="T">Specifies the type of elements in the queue.</typeparam>
    public sealed class RichQueue<T> : IDisposable
    {
        /// <summary>
        /// Inner queue
        /// </summary>
        private readonly Queue<T> _queue;

        /// <summary>
        /// Allows only one thread work inside of Pop method at one time
        /// </summary>
        private readonly object _popSync;

        /// <summary>
        /// Reset event for waiting new item when Pop method is called
        /// </summary>
        private readonly ManualResetEvent _resetEvent;

        /// <summary>
        /// Flag indicating that instance is disposed or not
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the RichQueue class
        /// </summary>
        public RichQueue()
        {
            _queue = new Queue<T>();
            _resetEvent = new ManualResetEvent(false);
            _popSync = new object();
            _disposed = false;
        }

        /// <summary>
        /// Gets the number of elements contained in queue
        /// </summary>
        /// <exception cref="ObjectDisposedException">The instance is disposed</exception>
        public int Count
        {
            get
            {
                if (_disposed)
                    throw new ObjectDisposedException(Localization.strInstanceIsDisposed);

                lock (_queue)
                {
                    return _queue.Count;
                }
            }
        }

        /// <summary>
        /// Adds an object to the quue
        /// </summary>
        /// <param name="item">New item</param>
        /// <exception cref="ObjectDisposedException">The instance is disposed</exception>
        public void Push(T item)
        {
            if (_disposed)
                throw new ObjectDisposedException(Localization.strInstanceIsDisposed);

            lock (_queue)
            {
                _queue.Enqueue(item);
                _resetEvent.Set();
            }
        }

        /// <summary>
        /// Removes and returns the object at the beginning of the queue.
        /// If no queued items, then waits new one
        /// </summary>
        /// <returns>First item in the queue</returns>
        /// <exception cref="ObjectDisposedException">The instance is disposed</exception>
        public T Pop()
        {
            if(_disposed)
                throw new ObjectDisposedException(Localization.strInstanceIsDisposed);

            lock (_popSync)
            {
                bool needWait = false;
                lock (_queue)
                {
                    if (_queue.Count == 0)
                    {
                        // We should wait while queue will not be empty
                        needWait = true;
                        _resetEvent.Reset();
                    }
                }

                if (needWait)
                {
                    // Wait signal from Push method
                    _resetEvent.WaitOne();
                }

                lock (_queue)
                {
                    // Now, we sure that queue has items
                    return _queue.Dequeue();
                }
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_disposed)
                return;

            _resetEvent.Dispose();
            _queue.Clear();
            _disposed = true;

            GC.SuppressFinalize(this);
        }
    }
}