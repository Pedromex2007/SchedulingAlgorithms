using System;
using System.Collections.Generic;
using System.Text;

namespace Project1OS {
    class FCFS : Scheduler {
        Process activeProcess;
        int totalTime = 0;
        public void AddProcess(Process process) {
            readyQueue.Enqueue(process);
        }
        public void BeginSequence() {
            RunProcesses();
            CalculateTimes();
        }
        public void CalculateTimes() {
            Console.WriteLine("Tr times");
            foreach (var process in waitingQueue) {
                Console.WriteLine(process.turnAroundTime);
            }
            Console.WriteLine("Wt times");
            foreach (var process in waitingQueue) {
                process.waitTime--;
                Console.WriteLine(process.waitTime);
            }
        }
        public void RunProcesses() {

            while (true) {
                //if (readyQueue.Count <= 0 && activeProcess == null) {
                //    break;
                //}
                totalTime++;
                if (activeProcess == null) {
                    if (readyQueue.Count > 0) {
                        activeProcess = readyQueue.Dequeue();
                    }  
                    if(ioQueue.Count <= 0 && readyQueue.Count <= 0 && activeProcess == null) {
                        break;
                    }
                }
                RunBurstCycle();
                RunIOCycle();
                Console.WriteLine("Total time: " + totalTime);
                //Console.WriteLine("Current Pos: " + activeProcess.arrPos);
            }
        }

        public void RunBurstCycle() {
            if (activeProcess == null) {
                Console.WriteLine("No active process.");
                return;
            }
            Console.WriteLine("RUNNING CPU, PROCESS" + activeProcess.processID + " : " + activeProcess.burst_times[activeProcess.ArrPos]);
            activeProcess.RunBurst();
            if(activeProcess.IsBurstComplete()) {
                Console.WriteLine("\nProcess complete, transferring to IO queue.");
                if(activeProcess.IsInFinalBurst()) {
                    //Do NOT put it in IO queue, the process is done completely.
                    waitingQueue.Enqueue(activeProcess);
                    activeProcess.CompleteTime = totalTime;
                    Console.WriteLine("Process has finished final burst and has been placed into waiting queue.");
                    Console.WriteLine("Finished last burst at: " + totalTime);
                } else {
                    ioQueue.Enqueue(activeProcess);
                }
                activeProcess = null;
            }
            //Increment waiting time for processing not being used by the CPU.
            //Waiting time for processes in its IO burst will NOT be incremented.
            foreach (var process in readyQueue) {
                process.waitTime++;
            }
        }

        public void RunIOCycle() {
            if (ioQueue.Count <= 0) return;
            Console.WriteLine("IO Time:" + ioQueue.Peek().io_times[ioQueue.Peek().ArrPos]);
            ioQueue.Peek().RunIO();
            if(ioQueue.Peek().IsIOComplete()) {
                ioQueue.Peek().ArrPos++;
                Console.WriteLine("IO Complete, moving to ready queue.");
                readyQueue.Enqueue(ioQueue.Dequeue());
            }
        }



        /*// Function to find the waiting time for all
        // processes
        public void FindWaitingTime(int[] processes, int n, int[] bt, int[] wt) {
            // waiting time for first process is 0
            wt[0] = 0;

            // calculating waiting time
            for (int i = 1; i < n; i++) {
                wt[i] = bt[i - 1] + wt[i - 1];
            }
        }

        // Function to calculate turn around time
        public void FindTurnAroundTime(int[] processes, int n, int[] bt, int[] wt, int[] tat) {
            // calculating turnaround time by adding
            // bt[i] + wt[i]
            for (int i = 0; i < n; i++) {
                tat[i] = bt[i] + wt[i];
            }
        }

        // Function to calculate average time
        public void FindavgTime(int[] processes, int n, int[] bt) {
            int[] wt = new int[n];
            int[] tat = new int[n];
            int total_wt = 0, total_tat = 0;

            //Function to find waiting time of all processes
            FindWaitingTime(processes, n, bt, wt);

            //Function to find turn around time for all processes
            FindTurnAroundTime(processes, n, bt, wt, tat);

            //Display processes along with all details
            Console.Write("Processes Burst time Waiting" + " time Turn around time\n");

            // Calculate total waiting time and total turn
            // around time
            for (int i = 0; i < n; i++) {
                total_wt = total_wt + wt[i];
                total_tat = total_tat + tat[i];
                Console.Write(" {0} ", (i + 1));
                Console.Write("     {0} ", bt[i]);
                Console.Write("     {0}", wt[i]);
                Console.Write("     {0}\n", tat[i]);
            }
            float s = (float)total_wt / (float)n;
            int t = total_tat / n;
            Console.Write("Average waiting time = {0}", s);
            Console.Write("\n");
            Console.Write("Average turn around time = {0} ", t);
        }*/
    }
}
