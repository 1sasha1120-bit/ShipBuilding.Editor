using Common.Repositories.Interfaces;
using System;

namespace Common.Repositories {
    public abstract class BaseUnitOfWork : IBaseUnitOfWork {

        private bool disposed = false;

        public virtual void Dispose(bool disposing) {
            if (!this.disposed) {
                if (disposing) {
                    DoDispose();
                }
                this.disposed = true;
            }
        }

        protected abstract void DoDispose();

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public abstract void Save();
    }
}
