using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace cardooo.core
{
    public class GameFSM : Singleton<GameFSM>
    {
        Dictionary<System.Type, IFSMState> fsmStateDic = new Dictionary<System.Type, IFSMState>();

        public IFSMState CurFsmState { get; private set; } = null;

        public GameFSM()
        {
            AddNewFSMState(new InitFSMState());
            TransTo<InitFSMState>();
        }

        public void AddNewFSMState(IFSMState state)
        {
            if (fsmStateDic.ContainsKey(state.GetType()))
            {
                DLog.LogError($"[CORE] It is already have same type state! ${state}");
                return;
            }

            fsmStateDic.Add(state.GetType(), state);
        }

        public void TransTo<T>(System.Action callback = null) where T : IFSMState
        {
            if (CurFsmState != null && CurFsmState.GetType() == typeof(T))
            {
                DLog.Log($"[FSM] It is already in {typeof(T)} now!");
                return;
            }
            if (!fsmStateDic.ContainsKey(typeof(T)))
            {
                DLog.LogError($"[FSM] {typeof(T)} is not in dic!");
                return;
            }
            if (CurFsmState != null)
            {
                CurFsmState.StateEnd();
            }

            CurFsmState = fsmStateDic[typeof(T)];
            CurFsmState.StateBegin();

            if (callback != null)
            {
                callback?.Invoke();
            }
        }
    }
}