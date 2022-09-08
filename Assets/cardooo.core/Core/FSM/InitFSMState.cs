using UnityEngine;
using System.Collections;
namespace cardooo.core
{
    public class InitFSMState : IFSMState
    {
        public virtual void StateBegin()
        {
            DLog.Log($"[InitFSMState][StateBegin]");
        }

        public virtual void StateEnd()
        {

        }
    }
}