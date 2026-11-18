using System;

namespace Common.Repositories.Interfaces {
    public interface IBaseUnitOfWork : IDisposable {
        void Save();
    }
}
