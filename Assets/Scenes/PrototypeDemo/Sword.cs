using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scenes.New_Folder
{
    
    public class Sword: MonoBehaviour
    {
        [SerializeField]
        public int atk = 10;
        private string notifyStr;

        public void Start()
        {
        }
        public Sword clone()
        {
            Sword sword = Instantiate(this);
            return sword;
        }

        public int attack()
        {
            return atk;
        }
        public string notify()
        {
            return notifyStr;
        }
    }

    
}
