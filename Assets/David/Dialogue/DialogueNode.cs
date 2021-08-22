using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Dialogue
{

    [System.Serializable]
    public class DialogueNode
    {
        public string DialogueUniqueID;
        public string text;
        public List<string> childern = new List<string>();
        public Rect rect = new Rect(0,0,200,100);
    }

}