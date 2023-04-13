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

        int GetItemByUID(int uid, out Item target)
        {
            target = null;
            int index = 0;
            foreach (var i in Items)
            {
                if (i.UID == uid)
                {
                    target = i;
                    break;
                }
                index++;
            }
            return index;
        }

        int GetItemByType(int typeIndex, out Item target)
        {
            target = null;
            int index = 0;
            foreach (var i in Items)
            {
                if (i.TypeIndex == typeIndex)
                {
                    target = i;
                    break;
                }
                index++;
            }
            return index;
        }

        public void AddItem(int uid, Item newItem)
        {
            int index = GetItemByUID(uid, out Item target);

            if (target == null)
            {
                target = newItem;                
                Items.Add(newItem);
            }
            else
            {
                target.Quentity += newItem.Quentity;
            }
            save(index, target);
        }

        public bool AddItemByType(int typeIndex, int quentity)
        {            
            int index = GetItemByType(typeIndex, out Item target);

            if (target == null)
            {
                DLog.LogError($"not find item {typeIndex}");
                return false;
            }
            else
            {
                target.Quentity += quentity;
                save(index, target);
                return true;
            }
        }

        public bool UseItem(int uid, int quentity)
        {
            int index = GetItemByUID(uid, out Item target);

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
                save(index, target);
                return true;
            }
        }

        public bool UseItemByType(int typeIndex, int quentity)
        {
            int index = GetItemByType(typeIndex, out Item target);

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
                save(index, target);
                return true;
            }
        }

        void save(int index, Item item)
        {
            DLog.Log($"[SAVE][BAG_{index}] {item.ToPrefsString()}");
            PlayerPrefs.SetString($"BAG_{index}", item.ToPrefsString());
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