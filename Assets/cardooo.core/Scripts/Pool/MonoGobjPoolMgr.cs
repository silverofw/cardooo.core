using UnityEngine;
using System.Collections.Generic;

namespace cardooo.core
{
    public class MonoGobjPoolMgr : Singleton<MonoGobjPoolMgr>
    {
        GameObject Root = null;

        Dictionary<string, MonoGobjPool> Dic = new Dictionary<string, MonoGobjPool>();

        protected override void Init()
        {
            base.Init();

            Root = new GameObject();
            Root.name = "MonoGobjPoolRoot";

            GameObject.DontDestroyOnLoad(Root);
        }

        public MonoGobjPool CreatePool(string path, GameObject prefab, int count = 1)
        {
            if (!Dic.TryGetValue(path, out MonoGobjPool pool))
            {
                pool = new MonoGobjPool(path, prefab, Root, count);
                Dic.Add(path, pool);
            }

            return pool;
        }
    }
}