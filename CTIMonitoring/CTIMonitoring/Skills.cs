using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Collections;

namespace VO
{
    class SkillList
    {
        private ArrayList skillList;

        public SkillList()
        {
            this.skillList = new ArrayList();
        }


        public void addSkill(SkillVO skillVO)
        {
            this.skillList.Add(skillVO);
        }



        public ArrayList getSkillList()
        {
            return this.skillList;
        }

    }
}
