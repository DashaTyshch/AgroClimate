using Dima.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Models
{
    public class Storage
    {
        private static Storage _instance;

        public Request SelectingRequest { get; private set; }

        private static Storage GetInstance()
        {
            if (_instance == null)
                _instance = new Storage();
            return _instance;
        }

        public void ChangeSelectedRequest(Request request)
        {
            SelectingRequest = request;
        }

    }
}
