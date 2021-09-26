using System;
using System.Collections.Generic;
using System.Text;

namespace Project1OS {
    abstract class Scheduler {
        protected Queue<Process> readyQueue = new Queue<Process>();
        protected Queue<Process> waitingQueue = new Queue<Process>();
        protected Queue<Process> ioQueue = new Queue<Process>();
    }
}
