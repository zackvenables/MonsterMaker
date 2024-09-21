using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] CharBase _base;
    [SerializeField] int level;

    [SerializeField] bool isPlayerUnit;

    public Char Char { get; set; }

    public void Setup()
    {
        Char = new Char(_base, level);

        if (isPlayerUnit)
        {
            GetComponent<Image>().sprite = Char.Base.BackSprite;
        }
        else
        {
            GetComponent<Image>().sprite = Char.Base.FrontSprite;
        }


    }
}
