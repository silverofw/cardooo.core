using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cardooo.core
{
    public class GoldItem : Item
    {
        public GoldItem(int uid,int typeIndex, int quentity = 1) : base(uid, typeIndex, quentity)
        {
            int msgIndex = 10;
            parameters.Add(msgIndex, new ItemParameter("GOLD"));
        }        
    }
}
