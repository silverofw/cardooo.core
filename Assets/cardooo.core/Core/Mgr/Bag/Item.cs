using System.Collections;
using System.Collections.Generic;

namespace cardooo.core
{
    public class Item
    {
        protected const int UID_INDEX = 0;
        protected const int TYPE_INDEX = 1;
        protected const int QUENTITY_INDEX = 2;

        /// <summary>
        /// parameterIndex,value
        /// </summary>
        public Dictionary<int, ItemParameter> parameters = new Dictionary<int, ItemParameter>();

        public int UID { get { return parameters[UID_INDEX].intValue; } }
        public int TypeIndex { get { return parameters[TYPE_INDEX].intValue; } }
        public int Quentity { get { return parameters[QUENTITY_INDEX].intValue; } set { parameters[QUENTITY_INDEX].update(value); } }

        public Item(int uid, int typeIndex, int quentity = 1)
        {
            parameters.Add(UID_INDEX, new ItemParameter(uid));
            parameters.Add(TYPE_INDEX, new ItemParameter(typeIndex));
            parameters.Add(QUENTITY_INDEX, new ItemParameter(quentity));
        }

        public Item(string info)
        {
            string[] infos = info.Split(',');
            foreach (var p in infos)
            {
                if (p == "")
                    continue;
                string[] param = p.Split(':');                
                parameters.Add(int.Parse(param[0]), new ItemParameter(int.Parse(param[1]), param[2]));
            }
        }

        public string ToPrefsString()
        {
            string info = "";
            foreach (var p in parameters)
            {
                info += $"{p.Key}:{p.Value.ToPrefsString()},";
            }
            return info;
        }
    }
}
