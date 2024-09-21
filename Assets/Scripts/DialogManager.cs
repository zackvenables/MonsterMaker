using Assets.Scripts;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] int lettersPerSecond;

    public event Action OnShowDialog;
    public event Action OnCloseDialog;
    public event Action OnStartBattle;

    public static DialogManager Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
    }

    Dialog dialog;
    int currentLine = 0;
    bool isTyping;

    public IEnumerator ShowDialog(Dialog dialog)
    {
        yield return new WaitForEndOfFrame();

        OnShowDialog?.Invoke();

        this.dialog = dialog;

        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }

    public void HandleUpdate() 
    {
        if (Input.GetKeyDown(KeyCode.E) && !isTyping)
        {
            ++currentLine;
            Debug.Log("current" + currentLine);
            Debug.Log(dialog);
            if (currentLine < dialog.Lines.Count) 
            {
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));

            }
            else 
            {
                currentLine = 0;
                dialogBox.SetActive(false);
                OnCloseDialog?.Invoke();

                if (this.dialog.IsBattleDialog)
                {
                    OnStartBattle();
                }
            }
        }
    }

    public IEnumerator TypeDialog(string dialog) 
    {
        isTyping = true;
        dialogText.text = "";

        foreach (var letter in dialog.ToCharArray()) 
        {

            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);   
        }

        isTyping = false;

    }
}
