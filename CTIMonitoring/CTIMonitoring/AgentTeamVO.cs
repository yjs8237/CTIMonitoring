using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VO
{
    class AgentTeamVO
    {
        private string agentTeamID; // ID
        private string agentTeamKor;    // 팀명 (한글)
        private string agentTeamEng;    // 팀명 (영문)


        public void setAgentTeamID(string ID)
        {
            this.agentTeamID = ID;
        }
        public string getAgentTeamID()
        {
            return this.agentTeamID;
        }

        public void setAgentTeamKor(string agentTeamKor)
        {
            this.agentTeamKor = agentTeamKor;
        }
        public string getAgentTeamKor()
        {
            return this.agentTeamKor;
        }

        public void setAgentTeamEng(string agentTeamEng)
        {
            this.agentTeamEng = agentTeamEng;
        }
        public string getAgentTeamEng()
        {
            return this.agentTeamEng;
        }
    }
}
