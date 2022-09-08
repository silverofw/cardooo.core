using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace cardooo.core
{
    public interface IFSMState
    {
        void StateBegin();
        void StateEnd();
    }
}