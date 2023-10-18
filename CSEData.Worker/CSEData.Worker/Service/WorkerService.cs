using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace CSEData.Worker.Service
{
    public  class WorkerService:ServiceBase
    {
        private Thread thread;
        public bool StopRequest=false;
        public WorkerService(Thread thread)
        {
            this.thread = thread;
        }
        public WorkerService()
        {
            
        }

        protected override void OnStart(string[] args)
        {
            thread = new Thread(StopThread);
            thread.Start();        }

        public void StopThread()
        {
            if (!StopRequest)
            {
                Thread.Sleep(5000); 
            }
        }
    }
}
