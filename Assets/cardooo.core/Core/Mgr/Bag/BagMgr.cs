using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace cardooo.core
{
    public class BagMgr : Singleton<BagMgr>
    {
        /// <summary>
        /// UID, Item
        /// </summary>
        public List<Item> Items { get; private set; } = new List<Item>();

        protected override void Init()
        {
            base.Init();
            Load();
        }

        public void ClearAll()
        {
            int index = 0;
            while (true)
            {
                string info = PlayerPrefs.GetString($"BAG_{index}");
                if (info == "")
                    break;

                PlayerPrefs.SetString($"BAG_{index}", "");
            }
            Items = new List<Item>();

            DLog.Log("[BAG] Clear All!");
        }

        public void GetItemByUID(int uid, out Item target)
        {
            target = null;            
            foreach (var i in Items)
            {
                if (i.UID == uid)
                {
                    target = i;
                    break;
                }             
            }            
        }

        public void GetItemByType<T>(int typeIndex, out T target) where T : Item
        {
            target = null;            
            foreach (var i in Items)
            {
                if (i.TypeIndex == typeIndex)
                {
                    target = i as T;
                    break;
                }
            }
        }

        public void GetItemByType<T>(int typeIndex, out List<T> targets)where T : Item
        {
            targets = new List<T>();
            foreach (var i in Items)
            {
                if (i.TypeIndex == typeIndex)
                {
                    targets.Add(i as T);                    
                }
            }
        }

        public void AddItem(Item newItem)
        {
            GetItemByUID(newItem.UID, out Item target);

            if (target == null)
            {
                target = newItem;

                target.BagIndex = Items.Count;
                Items.Add(target);
            }
            else
            {
                target.Quentity += newItem.Quentity;
            }
            save(target);
        }

        public bool AddItemByType(int typeIndex, int quentity)
        {            
            GetItemByType(typeIndex, out Item target);

            if (target == null)
            {
                DLog.LogError($"not find item {typeIndex}");
                return false;
            }
            else
            {
                target.Quentity += quentity;
                save(target);
                return true;
            }
        }

        public bool UseItem(int uid, int quentity)
        {
            GetItemByUID(uid, out Item target);

            if (target == null)
            {
                DLog.LogError($"not find item {uid}");
                return false;
            }
            else
            {
                if (target.Quentity < quentity)
                    return false;

                target.Quentity -= quentity;
                save(target);
                return true;
            }
        }

        public bool UseItemByType(int typeIndex, int quentity)
        {
            GetItemByType(typeIndex, out Item target);

            if (target == null)
            {
                DLog.LogError($"not find item {typeIndex}");
                return false;
            }
            else
            {
                if (target.Quentity < quentity)
                    return false;

                target.Quentity -= quentity;
                save(target);
                return true;
            }
        }

        public void save(Item item)
        {
            DLog.Log($"[SAVE][BAG_{item.BagIndex}] {item.ToPrefsString()}");
            PlayerPrefs.SetString($"BAG_{item.BagIndex}", item.ToPrefsString());
        }

        void Load()
        {
            int index = 0;
            while (true)
            {
                string info = PlayerPrefs.GetString($"BAG_{index}");
                if (info == "")
                    break;

                Items.Add(new Item(info));
                index++;
            }
        }
    }
}