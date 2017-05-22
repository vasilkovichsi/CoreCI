using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using CoreCI.Common.Logging.Interfaces;
using CoreCI.Models.BuildItem;

namespace CoreCI.Logic.BuildQueue
{
    internal class BuildQueue : IBuildQueue
    {
        private readonly ILogger _logger;

        public BuildQueue(ILogger logger)
        {
            _logger = logger;
        }

        private readonly ConcurrentQueue<BuildItem> _queue = new ConcurrentQueue<BuildItem>();
        public bool Add(BuildItem item)
        {
            try
            {
                _queue.Enqueue(item);
            }
            catch (Exception e)
            {
                _logger.LogException(e);
                throw;
            }
            
            return true;
        }

        public bool Remove(BuildItem item)
        {
            try
            {
                return _queue.TryDequeue(out item);
            }
            catch (Exception e)
            {
                _logger.LogException(e);
                throw;
            }
        }

        public BuildItem GetNext()
        {
            try
            {
                BuildItem item;
                if (_queue.TryPeek(out item))
                {
                    return item;
                }
                else
                {
                    throw new KeyNotFoundException("cannot take value from queue");
                }
            }
            catch (Exception e)
            {
                _logger.LogException(e);
                throw;
            }
        }
    }
}