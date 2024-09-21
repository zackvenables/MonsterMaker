using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [System.Serializable]
    public class Dialog
    {
        [SerializeField] List<string> lines;

        public bool IsBattleDialog;

        public List<string> Lines { 
            get { return lines; }
        }        
    }
}
