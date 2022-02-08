using UnityEngine;
using System.Collections;
using System;

namespace cardooo.core
{
    public class Singleton<T> where T : Singleton<T>
    {
        static T _instance = default(T);
        public static T Instance
        {
            get {
                if (_instance == null)
                {
                    _instance = Activator.CreateInstance<T>();
                    _instance.Init();
                }
                return _instance;
            }
        }

        protected virtual void Init() { }
    }

    public class Mgr<T> : Singleton<T> where T : Mgr<T>
    {
        public virtual void InitMgr()
        {

        }

        public virtual void Terminate()
        {

        }
    }
}
