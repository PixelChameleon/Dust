using System.Collections.Generic;
using UnityEngine;

namespace GlobalScripts {
    public class ItemStack {
        public int id;
        public new string Name;
        public List<string> Description = new List<string>();
        public Sprite Sprite;

        public ItemStack(int id, string name, Sprite sprite) {
            this.id = id;
            this.Name = name;
            this.Sprite = sprite;
        }
    }
}