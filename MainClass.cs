using System;

namespace Project1OS {
    static class MainClass {
        public static void Main(String[] args) {
            /*// process id's
            int[] processes = { 1, 2, 3 };
            int n = processes.Length;

            // Burst time of all processes
            int[] burst_time = { 10, 5, 8 };
            FCFS firstCome = new FCFS();
            firstCome.FindavgTime(processes, n, burst_time);
            RR roundrobin = new RR();
            roundrobin.FindavgTime(processes, n, burst_time , 2);*/

            int[] p1bursts = { 6, 4 };
            int[] p1io = { 10 };
            int[] p2bursts = { 9, 6 };
            int[] p2io = { 15 };
            int[] p3bursts = { 3, 2 };
            int[] p3io = { 5 };

            /*int[] p1bursts = { 5, 3, 5, 4, 6, 4, 3, 5 };
            int[] p1io = { 27, 31, 43, 18, 22, 26, 24 };
            int[] p2bursts = { 4, 5, 7, 12, 9, 4, 9, 7, 8 };
            int[] p2io = { 48, 44, 42, 37, 76, 41, 31, 43 };
            int[] p3bursts = { 8, 12, 18, 14, 4, 15, 14, 5, 6 };
            int[] p3io = {  33, 41, 65, 21, 61, 18, 26, 31 };
            int[] p4bursts = { 3, 4, 5, 3, 4, 5, 6, 5, 3 };
            int[] p4io = { 35, 41, 45, 51, 61, 54, 82, 77 };
            int[] p5bursts = { };
            int[] p5io = { };
            int[] p6bursts = { };
            int[] p6io = { };
            int[] p7bursts = { };
            int[] p7io = { };
            int[] p8bursts = { };
            int[] p8io = { };*/
            Process p1obj = new Process(1, p1bursts, p1io);
            Process p2obj = new Process(2, p2bursts, p2io);
            Process p3obj = new Process(3, p3bursts, p3io);
            FCFS firstcomefirstserve = new FCFS();
            firstcomefirstserve.AddProcess(p1obj);
            firstcomefirstserve.AddProcess(p2obj);
            firstcomefirstserve.AddProcess(p3obj);
            firstcomefirstserve.BeginSequence();

            /*int n, i, j;
            Console.WriteLine("ENter no of processes:");

            n = Int32.Parse(Console.ReadLine());
            int[] pid = new int[n];
            int[] bt = new int[n];
            int[] rt = new int[n];
            int[] at = new int[n];
            int[] ct = new int[n];
            int[] wt = new int[n];
            int[] tat = new int[n];

            float atat = 0, awt = 0, art = 0;   //declaring arrays and variables
            Console.WriteLine("\nEnter the bursttimes for " + n + " processes\n");
            for (i = 0; i < n; i++) {
                pid[i] = i + 1;       //assining process numbers
                at[i] = 0;   //assining arrival time for each process as 0 because all are arrived at same time
         //getting burst times from user
                bt[i] = Int32.Parse(Console.ReadLine());
            }
            for (i = 0; i < n; i++) {
                ct[i] = 0;   //initializing the completion times as 0
            }
            //ct[-1] = 0;
            for (i = 0; i < n; i++) {
                ct[i] = ct[i - 1] + bt[i];   //recurence relation for calculting the completion time for each process
                tat[i] = ct[i] - at[i];       //calculating the turn around time.[tat=ct-at]
                wt[i] = tat[i] - bt[i];   //calculation of waiting time
                atat = atat + tat[i];   //calculation of avarage turn around time
                awt = awt + wt[i];       //calculation of avarage waiting time
                rt[i] = ct[i - 1] - at[i];   //calculation of responce time
                art = art + rt[i];
            }
            atat = atat / n;
            awt = awt / n;
            art = art / n;
            Console.WriteLine("pid\t at\t bt\t ct\t tat\t wt\t rt\n");
            for (i = 0; i < n; i++) {
                Console.WriteLine(pid[i] + "\t" + at[i] + '\t' + bt[i] + '\t' + ct[i] + '\t' + tat[i] + '\t' + wt[i] + '\t' + rt[i]);
            }
            Console.WriteLine("Avarage TurnAroundTime=" + atat);
            Console.WriteLine("\nAvarage WaitingTime=" + awt);
            Console.WriteLine("\nAvarage respose time=" + art);*/
        }
    }
}
