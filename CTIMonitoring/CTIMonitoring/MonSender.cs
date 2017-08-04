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
    class MonSender
    {
        private LogWrite logwrite;
        private Monitoring monObj;
        private TcpClient sock = null;
        private NetworkStream writeStream;

        private StreamReader reader;

        public MonSender(TcpClient sock, LogWrite logwrite, Monitoring monObj)
        {
            this.sock = sock;
            this.logwrite = logwrite;
            this.monObj = monObj;

            this.writeStream = sock.GetStream();
            Encoding encode = System.Text.Encoding.GetEncoding("UTF-8");
            this.reader = new StreamReader(writeStream, encode);
        }

        public void runThread()
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    if (monObj.monReConnect() == ERRORCODE.SUCCESS)
                    {
                        logwrite.write("MonSender renThread", "Connection Success !!");
                        break;
                    }
                    else
                    {
                        logwrite.write("MonSender renThread", "Connection Fail !! Retry");
                    }
                }
            }
            catch (Exception e)
            {
                logwrite.write("MonSender renThread", e.ToString());
            }


        }

    }
}
