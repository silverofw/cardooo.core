using UnityEngine;
using System.Collections.Generic;

namespace cardooo.core
{
    public class GobjPoolMgr : Singleton<GobjPoolMgr>
    {
        public const string uidSpilt = "]_";
        public int Uid { get; private set; } = 0;
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
            if (path.Contains(uidSpilt))
            {
                DLog.LogError($"[ERROR] path can not contain \"{uidSpilt}\"");
                return null;
            }

            if (!Dic.TryGetValue(path, out GobjPool pool))
            {
                pool = new GobjPool(path, prefab, Root, count);
                Dic.Add(path, pool);
            }

            return pool;
        }

        public void Recycle(GameObject obj)
        {
            Dic[obj.name.Split(uidSpilt)[1]].Recycle(obj);
        }

        public int GetUid()
        {
            Uid += 1;
            return Uid;
        }
    }
}