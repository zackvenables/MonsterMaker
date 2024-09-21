using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] HPBar hpBar;

    Char _char;

    public void SetData(Char cha)
    {
        _char = cha;

        nameText.text = cha.Base.Name;
        levelText.text = "Lvl " +  cha.Level;
        hpBar.SetHP((float) cha.HP / cha.MaxHp);
    }


    public IEnumerator UpdateHP()
    {
        //hpBar.SetHP();

        yield return hpBar.SetHPSmooth((float)_char.HP / _char.MaxHp);


    }
}
