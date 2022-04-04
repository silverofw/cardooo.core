﻿using System;
using System.Collections.Generic;

namespace cardooo.core
{
    public class EntityMgr : Singleton<EntityMgr>
    {
        public ulong NextEntityId { get; private set; } = 1;
        public Dictionary<ulong, Entity> EntityDic = new Dictionary<ulong, Entity>();

        public T Create<T>() where T : Entity
        {
            object[] paramArray = new object[] { NextEntityId };
            T t = (T)Activator.CreateInstance(typeof(T), args: paramArray);
            EntityDic.Add(NextEntityId, t);
            NextEntityId++;
            return t;
        }

        public void DeleteEntity(Entity entity)
        {
            if (EntityDic.ContainsKey(entity.Id))
            {
                entity.Terminate();
                EntityDic.Remove(entity.Id);
            }
            else
            {
                DLog.LogError($"[ENTITY] why entity id: {entity.Id} not in dic?");
            } 
        }

        public void Reset()
        {
            NextEntityId = 1;
            foreach (var v in EntityDic)
            {
                DeleteEntity(v.Value);
            }
            EntityDic.Clear();
        }
    }
}
