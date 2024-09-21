using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject menu;
    List<Text> menuItems;
    int selectedItem = 0;

    public event Action<int> OnMenuSelected;
    public event Action OnBack;


    private void Awake()
    {
        menuItems =  menu.GetComponentsInChildren<Text>().ToList();
    }

    public void OpenMenu() 
    {
        menu.SetActive(true);
        UpdateItemSelection();
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
    }

    public void HandleUpdate() 
    {
        int prevSelection = selectedItem;

        if (Input.GetKeyDown(KeyCode.W)) 
        {
            --selectedItem;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            ++selectedItem;
        }

        selectedItem = Mathf.Clamp(selectedItem, 0, menuItems.Count - 1);

        if(prevSelection != selectedItem)
        {
            UpdateItemSelection();
        }
        
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            OnMenuSelected?.Invoke(selectedItem);
            CloseMenu();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnBack?.Invoke();
            CloseMenu();
        }

    }

    void UpdateItemSelection ()
    {
        for (int i = 0; i < menuItems.Count; i++)
        {
            if (i == selectedItem) 
            {
                menuItems[i].color = Color.red;
            }
            else
            {
                menuItems[i].color = Color.black;
            }
        }
    }
}
