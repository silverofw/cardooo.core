using UnityEngine;
using System.Collections.Generic;

namespace cardooo.core
{
    public class GobjPoolMgr : Singleton<GobjPoolMgr>
    {
        int uid = 0;
        GameObject Root = null;

        Dictionary<string, GobjPool> Dic = new Dictionary<string, GobjPool>();

        protected override void Init()
        {
            base.Init();

            Root = new GameObject();
            Root.name = "MonoGobjPoolRoot";

            Object.DontDestroyOnLoad(Root);
        }

        public GobjPool CreatePool(string path, GameObject prefab, int count = 1)
        {
            if (!Dic.TryGetValue(path, out GobjPool pool))
            {
                pool = new GobjPool(path, prefab, Root, count);
                Dic.Add(path, pool);
            }

            return pool;
        }

        public void Recycle(GameObject obj)
        {
            Dic[obj.name.Split("]_")[1]].Recycle(obj);
        }

        public int GetUid()
        {
            uid += 1;
            return uid;
        }
    }
}