using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartUnitApi.Repositories
{
    public class RepositoryActionResult<T> where T : class
    {
        public T Model { get; private set; }
        public RepositoryActionStatus Status { get; private set; }
        public Exception Exception { get; private set; }

        public RepositoryActionResult(T model, RepositoryActionStatus status)
        {
            Model = model;
            Status = status;
        }

        public RepositoryActionResult(T model, RepositoryActionStatus status, Exception exception) : this(model, status)
        {
            Exception = exception;
        }
    }
}
