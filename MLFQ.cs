using System;
using System.Collections.Generic;
using System.Text;

namespace Project1OS {
    class MLFQ : Scheduler {
        RR queue1 = new RR(5);
        RR queue2 = new RR(10);
        FCFS queue3 = new FCFS();
    }
}
