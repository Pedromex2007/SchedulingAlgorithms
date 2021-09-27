using System;
using System.Collections.Generic;
using System.Text;

namespace Project1OS {
    class Scheduler {
        protected Process activeProcess;
        protected int totalTime = 0;

        protected Queue<Process> readyQueue = new Queue<Process>();
        protected Queue<Process> waitingQueue = new Queue<Process>();
        protected List<Process> ioQueue = new List<Process>();

        public virtual void AddProcess(Process process) {
            readyQueue.Enqueue(process);
        }

        public void CalculateTimes() {
            Console.WriteLine("Tr times");
            foreach (var process in waitingQueue) {
                Console.WriteLine(process.processID + " : " + process.turnAroundTime);
            }
            Console.WriteLine("Wt times");
            foreach (var process in waitingQueue) {
                process.waitTime--;
                Console.WriteLine(process.processID + " : " + process.waitTime);
            }
        }

    }
}
