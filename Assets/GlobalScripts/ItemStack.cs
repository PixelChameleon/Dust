using System.Collections.Generic;
using UnityEngine;

namespace GlobalScripts {
    public class ItemStack {
        public int id;
        public new string Name;
        public List<string> Description = new List<string>();

        public ItemStack(int id, string name) {
            this.id = id;
            this.Name = name;
        }
    }
}