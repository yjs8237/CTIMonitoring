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
    class AEMSClient
    {
        protected TcpClient sock = null;

        protected NetworkStream writeStream;
        protected StreamReader reader;
        protected StreamWriter writer;

        private Monitoring monObj;
        private LogWrite logwrite;

        private MonReceiver receiver;

        public AEMSClient(LogWrite logwrite, Monitoring monObj)
        {
            this.logwrite = logwrite;
            this.monObj = monObj;
        }

        public int connect(string ip, int port)
        {
            try
            {
                sock = new TcpClient();

                IAsyncResult result = sock.BeginConnect(ip, port, null, null);
                var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(3000), true);

                if (success)
                {
                    writeStream = sock.GetStream();

                    writer = new StreamWriter(writeStream);

                    Encoding encode = System.Text.Encoding.GetEncoding("UTF-8");
                    reader = new StreamReader(writeStream, encode);

                }
                else
                {
                    return ERRORCODE.SOCKET_CONNECTION_FAIL;
                }

            }
            catch (Exception e)
            {
                logwrite.write("connect", e.ToString());
                if (sock != null)
                {
                    sock.Close();
                    sock = null;
                }
                return ERRORCODE.SOCKET_CONNECTION_FAIL;
            }

            return ERRORCODE.SUCCESS;
        }


        public int disconnect()
        {
            try
            {
                if (this.sock != null)
                {
                    sock.Close();
                    sock = null;
                }
                if (this.writeStream != null)
                {
                    writeStream.Close();
                    writeStream = null;
                }
                if (this.reader != null)
                {
                    reader.Close();
                    reader = null;
                }

                receiver.disconnect();
            }
            catch (Exception e)
            {
                logwrite.write("disconnect", e.ToString());
                return ERRORCODE.FAIL;
            }

            return ERRORCODE.SUCCESS;
        }

        public int startMonitoring()
        {
            receiver = new MonReceiver(reader, logwrite, monObj);
            ThreadStart recvts = new ThreadStart(receiver.runThread);
            Thread recvThread = new Thread(recvts);
            recvThread.Start();

            return ERRORCODE.SUCCESS;
        }


    }
}
