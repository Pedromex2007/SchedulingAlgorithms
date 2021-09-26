using System;
using System.Collections.Generic;
using System.Text;

namespace Project1OS {
    class Process {
        public int processID;
        public int arrivalTime = 0;
        public int waitTime = 0;
        private int completeTime;
        /// <summary>
        /// Sets complete time. Since arrival time is always zero, turn around time for this process will always be equal to completion time.
        /// </summary>
        public int CompleteTime { 
            get {
                return completeTime;
            }
            set {
                completeTime = value;
                turnAroundTime = completeTime;
            }
        }
        public int turnAroundTime = 0;

        private int arrPos = 0;

        public int ArrPos { 
            get {
                return arrPos;
            }
            set {
                arrPos = value;
                if(arrPos > burst_times.Length) {
                    arrPos = burst_times.Length;
                }
            }
        }

        public int[] burst_times;
        public int[] io_times;
        public Process(int id, int[] bursts, int[] ios) {
            processID = id;
            burst_times = bursts;
            io_times = ios;
        }
        public bool IsInFinalBurst() {
            Console.WriteLine("Final burst is: " + burst_times[burst_times.Length-1]);
            if (arrPos >= burst_times.Length-1) {
                return true;
            } else return false;
        }
        public bool IsBurstComplete() {
            if (burst_times[arrPos] == -1) {
                return true;
            } else return false;
        }
        public bool IsIOComplete() {
            if (io_times[arrPos] == -1) {
                return true;
            } else return false;
        }
        public bool IsIOTimesComplete() {
            if (arrPos >= io_times.Length-1) {
                return true;
            } else return false;
        }
        public void RunBurst() {
            try {
                burst_times[arrPos]--;
                if (burst_times[arrPos] <= 0) {
                    burst_times[arrPos] = -1;
                }
            }
            catch (Exception) {
                Console.WriteLine("Position exceeds burst time length.");
            }
        }
        public void RunIO() {
            try {
                io_times[arrPos]--;
                if (io_times[arrPos] <= 0) {
                    io_times[arrPos] = -1;
                }
            }
            catch (Exception) {
                Console.WriteLine("Position exceeds IO time length.");
            }

        }
    }
}
