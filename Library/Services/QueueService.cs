using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public class QueueService
    {
        public QueueService()
        {
            Console.WriteLine("Test");

            // Register Events
        }

        // Accept Queue
        public Task AcceptQueue()
        {
            return Task.CompletedTask;
        }
    }
}
