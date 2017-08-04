using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VO
{
    class SkillVO
    {
        private string skillID;
        private string skillKor;
        private string skillEng;

        public void setSkillID(string skillID)
        {
            this.skillID = skillID;
        }
        public string getSkillID()
        {
            return this.skillID;
        }

        public void setSkillKor(string skillKor)
        {
            this.skillKor = skillKor;
        }
        public string getSkillKor()
        {
            return this.skillKor;
        }

        public void setSkillEnd(string skillEng)
        {
            this.skillEng = skillEng;
        }
        public string getSkillEnd()
        {
            return this.skillEng;
        }
    }
}
