using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Collections;
using System.Net;
using JSON;
using VO;

namespace CTIMonitoring
{
    class MonReceiver
    {
        private LogWrite logwrite;
        private Monitoring monObj;
        private TcpClient sock = null;
        private NetworkStream writeStream;

        private StreamReader reader;

        private JsonHandler jsonHandler;
     
        public MonReceiver(TcpClient sock, LogWrite logwrite, Monitoring monObj)
        {
            this.sock = sock;
            this.logwrite = logwrite;
            this.monObj = monObj;

            this.writeStream = sock.GetStream();
            //Encoding encode = System.Text.Encoding.GetEncoding("UTF-8");
            //this.reader = new StreamReader(writeStream, encode);
            Encoding encode = Encoding.Default;
            this.reader = new StreamReader(writeStream, encode);
            this.jsonHandler = new JsonHandler();
        }
        
        public void runThread()
        {
            try
            {

                string line = "";

                while ((line = reader.ReadLine()) != null)
                {
                    
                    logwrite.write("runThread", line);

                    if (line.Length == 0)
                    {
                        continue;
                    }
                    
                    MonitoringVO monVO = jsonHandler.recvJson(line);

                    if (monVO != null)
                    {
                        switch (monVO.getCmd())
                        {
                            case "M07" :
                                logwrite.write("runThread", "M07 data -> " + monVO.getStrResult());
                                setSkillInfo(monVO.getStrResult());
                                break;

                            case "M08" :
                                logwrite.write("runThread", "M08 data -> " + monVO.getStrResult());
                                break;
                            default :
                                break;
                        }
                    }

                    
                    monObj.GetEventOnMon1(line);
                }

            }
            catch (Exception e)
            {
                logwrite.write("runThread", e.ToString());
                if (!monObj.disconnectReq)
                {
                    // 접속이 끊어지면 접속 재시도
                    monObj.monReConnect();
                }
            }
        }

        private int setSkillInfo(string skillInfo)
        {



            return 0;
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
            }
            catch (Exception e)
            {

            }
        
            return ERRORCODE.SUCCESS;
        }

    }
}
