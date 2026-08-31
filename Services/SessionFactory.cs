using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesmenSimulator.Services
{
    public class SessionFactory : ISessionFactory
    {
        public GameSession Create(string ownerName, string storeName)
        {
            return new GameSession(new Owner(ownerName), new Store(storeName));
        }

        public GameSession Load()
        {
            throw new NotImplementedException();
        }
    }
}