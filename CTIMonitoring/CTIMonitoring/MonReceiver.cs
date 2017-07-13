using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Collections;
using System.Net;

namespace CTIMonitoring
{
    class MonReceiver
    {
        private StreamReader reader;
        private LogWrite logwrite;
        private Monitoring monObj;

        public MonReceiver(StreamReader reader, LogWrite logwrite ,Monitoring monObj)
        {
            this.reader = reader;
            this.logwrite = logwrite;
            this.monObj = monObj;
        }

        public void runThread()
        {
            try
            {

                string line = "";




                while ((line = reader.ReadLine()) != null)
                {
                    logwrite.write("runThread", line);
                    monObj.GetEventOnMon1(line);
                }


            }
            catch (Exception e)
            {
                logwrite.write("runThread", e.ToString());
                monObj.monReConnect();
            }
        }

        public int disconnect()
        {

            
            this.reader.Close();

            return ERRORCODE.SUCCESS;
        }

    }
}
