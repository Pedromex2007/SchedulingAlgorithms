using System;
using System.Collections.Generic;
using System.Text;

namespace Project1OS {
    class RR : Scheduler {
        readonly int orgTq;
        public bool MLFQUsed = false;
        int Tq { get; set;}
        public RR(int timeQuantum) {
            Tq = timeQuantum;
            orgTq = timeQuantum;
        }

        public override void BeginSequence() {
            RunProcesses();
            CalculateTimes();
        }
        public void RunProcesses() {

            while (true) {
                totalTime++;
                if (activeProcess == null) {
                    if (readyQueue.Count > 0) {
                        activeProcess = readyQueue.Dequeue();
                    }
                    if (ioQueue.Count <= 0 && readyQueue.Count <= 0 && activeProcess == null) {
                        break;
                    }
                }
                RunBurstCycle();
                RunIOCycle();
                Console.WriteLine("Total time: " + totalTime);
                //Console.WriteLine("Current Pos: " + activeProcess.arrPos);
            }
        }

        private void RunBurstCycle() {
            if (activeProcess == null) {
                Console.WriteLine("No active process.");
                return;
            }
            Console.WriteLine("RUNNING CPU, PROCESS" + activeProcess.processID + " : " + activeProcess.burst_times[activeProcess.ArrPos]);
            activeProcess.RunBurst();
            Tq--;
            if (activeProcess.IsBurstComplete()) {
                Console.WriteLine("\nProcess complete, transferring to IO queue.");
                if (activeProcess.IsInFinalBurst()) {
                    //Do NOT put it in IO queue, the process is done completely.
                    Console.WriteLine("Transfer to IO queue aborted.");
                    waitingQueue.Enqueue(activeProcess);
                    activeProcess.CompleteTime = totalTime;
                    Console.WriteLine("Process has finished final burst and has been placed into waiting queue.");
                    //Console.WriteLine("Finished last burst at: " + totalTime);
                } else {
                    ioQueue.Add(activeProcess);
                }
                Tq = orgTq;
                activeProcess = null;
            } else if(Tq <= 0) {
                Console.WriteLine("Process has not finished its burst in time and has been placed back into the ready queue.");
                if(!MLFQUsed) {
                    readyQueue.Enqueue(activeProcess);
                } else {
                    //Activate event.
                    Console.WriteLine("ABORTING TRANSFER TO READY QUEUE: This process will be placed into the transferQueue instead.");
                    OnTqExpire(activeProcess);
                }
                Tq = orgTq;
                activeProcess = null;
            }
            //Increment waiting time for processing not being used by the CPU.
            //Waiting time for processes in its IO burst will NOT be incremented.
            foreach (var process in readyQueue) {
                process.waitTime++;
            }
            foreach (var process in transferList) {
                process.waitTime++;
            }
        }

        private void RunIOCycle() {
            if (ioQueue.Count <= 0) return;
            List<Process> processesToRemove = new List<Process>();
            //Process processToRemove = null;
            //Console.WriteLine("IO Time:" + ioQueue.Peek().io_times[ioQueue.Peek().ArrPos]);
            foreach (Process process in ioQueue) {
                Console.WriteLine("IO Time:" + process.io_times[process.ArrPos]);
                process.RunIO();
                if (process.IsIOComplete()) {
                    process.ArrPos++;
                    Console.WriteLine("IO Complete, moving to ready queue.");
                    readyQueue.Enqueue(process);
                    //ioQueue.Remove(process);
                    processesToRemove.Add(process);
                    //processToRemove = process;
                }
            }

            foreach (var process in processesToRemove) {
                ioQueue.Remove(process);
            }

        }
        void OnTqExpire(Process processToMove) {
            processToMove.rank++;
            transferList.Add(processToMove);
        }
    }
}
