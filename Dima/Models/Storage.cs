using Dima.Database.Entities;
using System;
using System.Collections.Generic;

namespace Dima.Models
{
    public class Storage
    {
        private static Storage _instance;

        public Request SelectedRequest { get; private set; }

        private Storage() { }

        public static Storage GetInstance()
        {
            if (_instance == null)
                _instance = new Storage();
            return _instance;
        }

        public void ChangeSelectedRequest(Request request)
        {
            SelectedRequest = request;
        }

    }
}
