using System;

namespace Demo.PL.Services
{
    public interface IScopedService
    {
        public Guid Guid { get; set; }   //glopal universal id -- uniqe numbers and letters

        string GetGuid();
    }
}
