using System;
using System.Linq;
using System.Collections.Generic;
namespace cardooo.core
{
    public class EntitySystemMgr : Singleton<EntitySystemMgr>
    {
        Dictionary<Type, EntitySystem> entitySystemDatas = new Dictionary<Type, EntitySystem>();

        public void Update(float delta)
        {
            Dictionary<Type, EntitySystem>.Enumerator items = entitySystemDatas.GetEnumerator();
            while (items.MoveNext())
            {
                items.Current.Value.SystemProcess(delta);
            }
        }

        public void AddNewSystem<T>() where T : EntitySystem
        {
            if (!entitySystemDatas.ContainsKey(typeof(T)))
            {
                entitySystemDatas.Add(typeof(T), (T)Activator.CreateInstance(typeof(T)));
            }
        }

        public void RemoveSystem<T>() where T : EntitySystem
        {
            if (entitySystemDatas.ContainsKey(typeof(T)))
            {
                entitySystemDatas.Remove(typeof(T));
            }
        }

        public void AddNewEntityInSystem<T>(Entity entity) where T : EntitySystem
        {
            if (!entitySystemDatas.ContainsKey(typeof(T)))
            {
                entitySystemDatas.Add(typeof(T), (T)Activator.CreateInstance(typeof(T)));
            }

            entitySystemDatas[typeof(T)].AddEntity(entity);
        }

        public void RemoveEntityInSystem<T>(Entity entity) where T : EntitySystem
        {
            if (entitySystemDatas.ContainsKey(typeof(T)))
            {
                entitySystemDatas[typeof(T)].RemoveEntity(entity);
                //DebugHandler.Log($"[RemoveEntityInSystem] {typeof(T)} : {entitySystemDatas[typeof(T)].entityList.Count}");
            }
        }

        public void Reset()
        {
            entitySystemDatas = new Dictionary<Type, EntitySystem>();
        }
    }
}