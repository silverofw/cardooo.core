using UnityEngine;
using System.Collections.Generic;
namespace cardooo.core
{
    public class EntitySystem
    {
        public List<Entity> entityList = new List<Entity>();

        public void SystemProcess(float delta)
        {
            foreach(Entity e in entityList)
            {
                Process(delta, e);
            }
        }

        public bool AddEntity(Entity entity)
        {
            if (!entityList.Contains(entity))
            {
                entityList.Add(entity);
                return true;
            }
            else
            {
                DLog.LogError("[CORE] this entity is in dic already!");
                return false;
            }
        }

        public void RemoveEntity(Entity entity)
        {
            entityList.Remove(entity);
        }

        public virtual void Process(float delta, Entity entity)
        {

        }
    }
}
