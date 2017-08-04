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

                logwrite.write("connect", "\t Try to Connect " + ip + " , " + port);

                IAsyncResult result = sock.BeginConnect(ip, port, null, null);
                var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(3000), true);

                if (success)
                {
                    logwrite.write("connect", "\t Connection Success !!" + ip + " , " + port);

                    writeStream = sock.GetStream();
                    writer = new StreamWriter(writeStream);

                    //Encoding encode = System.Text.Encoding.GetEncoding("ANSI");
                    //reader = new StreamReader(writeStream, encode);
                    //reader = new StreamReader(writeStream);
                    
                }
                else
                {
                    logwrite.write("connect", "\t Connection Fail !!" + ip + " , " + port);
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

                receiver.disconnect();
            }
            catch (Exception e)
            {
                logwrite.write("disconnect", e.ToString());
                return ERRORCODE.FAIL;
            }

            return ERRORCODE.SUCCESS;
        }

        public int send(string msg)
        {
            try
            {
                logwrite.write("send", "SEND MSG -> " + msg);
                writer.WriteLine(msg);
                writer.Flush();
            }
            catch (Exception e)
            {
                logwrite.write("send", e.ToString());
            }
            return ERRORCODE.SUCCESS;
        }

        public int startMonitoring()
        {
            receiver = new MonReceiver(sock, logwrite, monObj);
            ThreadStart recvts = new ThreadStart(receiver.runThread);
            Thread recvThread = new Thread(recvts);
            recvThread.Start();

            return ERRORCODE.SUCCESS;
        }


    }
}
