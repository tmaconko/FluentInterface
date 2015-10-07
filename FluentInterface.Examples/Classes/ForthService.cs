using System;

namespace FluentInterface.Examples.Classes
{
    internal class ForthService : IForthService
    {
        public ForthService(string someData)
        {

        }

        public void DestroyMe()
        {
            throw new NotImplementedException();
        }
    }
}