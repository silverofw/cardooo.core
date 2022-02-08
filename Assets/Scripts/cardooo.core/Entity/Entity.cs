using System.Collections;
using System.Collections.Generic;

namespace cardooo.core
{
    public class Entity
    {
        public ulong Id = 0;
        public Dictionary<System.Type, EntityComponent> ComDic = new Dictionary<System.Type, EntityComponent>();
        public EventHandler EventHandler = new EventHandler();

        public Entity(ulong id)
        {
            Id = id;
        }

        public void Terminate()
        {
            foreach (KeyValuePair<System.Type, EntityComponent> pair in ComDic)
            {
                pair.Value.Terminal();
            }
            ComDic.Clear();
        }

        public void AddComponent(EntityComponent component)
        {
            ComDic.TryGetValue(component.GetType(), out EntityComponent com);
            if (com == null)
            {
                ComDic[component.GetType()] = component;
            }
            else
            {
                DLog.LogError("[Entity][AddComponent] already have same component!");
            }
        }

        public T GetEntityComp<T>() where T : EntityComponent
        {
            ComDic.TryGetValue(typeof(T), out EntityComponent com);
            if (com == null)
            {
                DLog.LogError("[Entity][GetComponent] dic do not have thsis type component!");
            }
            if (!(com is T))
            {
                DLog.LogError("[Entity][GetComponent] component type is wrong!");
            }
            return (T)com;
        }

        public void RemoveComponent<T>() where T: EntityComponent
        {
            ComDic.Remove(typeof(T));
        }
    }
}
