using System;
using System.Collections.Generic;
using System.Text;

namespace Project1OS {
    abstract class Scheduler {
        protected Process activeProcess;
        public int totalTime = 0;

        protected Queue<Process> readyQueue = new Queue<Process>();
        protected Queue<Process> waitingQueue = new Queue<Process>();
        protected List<Process> ioQueue = new List<Process>();

        public List<Process> transferList = new List<Process>();

        abstract public void BeginSequence();

        /// <summary>
        /// Add process to the scheduler. How this will be done depends on how this method was implemented.
        /// Therefore, ALWAYS use this method to add a process to a scheduler.
        /// </summary>
        /// <param name="process">Process to add to scheduler.</param>
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
