using System;
using System.Collections.Generic;

namespace Core
{
    public class DisposableServices
    {
        private List<IDisposable> _services = new();

        public void Add(IDisposable service) => _services.Add(service);

        public void Dispose() => _services.ForEach(i => i.Dispose());
    }
}