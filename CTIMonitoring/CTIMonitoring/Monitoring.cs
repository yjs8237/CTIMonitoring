using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace CTIMonitoring
{
    public abstract class Monitoring
    {
        private AEMSClient client;
        private LogWrite logwrite;

        private string currentConnectedIP;
        private ArrayList ipList;
        private int port;
        bool isConnected;
        public Monitoring()
        {
            logwrite = LogWrite.getInstance();
        }

        public int monDisConnect()
        {
            if (client == null)
            {
                return ERRORCODE.FAIL;
            }

            return client.disconnect();
        }

        public int monConnect(string mon_A_IP, string mon_B_IP, int port)
        {

            logwrite.write("monConnect", "** call monConnect(" + mon_A_IP + " , " + mon_B_IP + " , " + port + ")");

            client = new AEMSClient(logwrite , this);

            ipList = new ArrayList();
            ipList.Add(mon_A_IP);
            ipList.Add(mon_B_IP);
            this.port = port;

            isConnected = false;

            for (int i = 0; i < ipList.Count; i++)
			{
                string ip = (string)ipList[i];

                logwrite.write("monConnect", "TRY TO CONNECT " + ip + " , " + port);
                if (client.connect(ip, port) == ERRORCODE.SUCCESS)
                {
                    logwrite.write("monConnect", "CONNECT SUCCESS!!");
                    currentConnectedIP = ip;
                    isConnected = true;
                    client.startMonitoring();
                    break;
                }
                else
                {
                    logwrite.write("monConnect", "CONNECT FAIL !!");
                    isConnected = false;
                }
			}

            return isConnected ? ERRORCODE.SUCCESS : ERRORCODE.FAIL;
        }

        public int monReConnect()
        {
            isConnected = false;

            while (!isConnected)
            {
                for (int i = 0; i < ipList.Count; i++)
                {
                    string ip = (string)ipList[i];

                    if (client.connect(ip, port) == ERRORCODE.SUCCESS)
                    {
                        currentConnectedIP = ip;
                        isConnected = true;
                        client.startMonitoring();
                        break;
                    }
                    else
                    {
                        isConnected = false;
                    }
                }
            }

             return isConnected ? ERRORCODE.SUCCESS : ERRORCODE.FAIL;
        }




        public abstract void GetEventOnMon1(string data);
        public abstract void GetEventOnMon2(string data);
        public abstract void GetEventOnMon3(string data);
        public abstract void GetEventOnMon4(string data);

    }
}
