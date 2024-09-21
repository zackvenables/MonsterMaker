using Assets.Scripts;
using UnityEngine;

namespace Assets
{
    public class InteractableBox : Interactable
    {
        [SerializeField] Dialog dialog;
        public override void OnInteract() 
        {
            Debug.Log("this is a box");

            StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
        }
    }
}
