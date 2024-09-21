using UnityEngine;

namespace Assets.Scripts
{

    public enum GameState { FreeRoam, Dialog, Menu, Battle }

    public class GameController : MonoBehaviour
    {
        MenuController menuController;

        [SerializeField] PlayerController playerController;
        [SerializeField] BattleSystem battleSystem;
        [SerializeField] Camera worldCamera;

        GameState state = GameState.FreeRoam;

        // Use this for initialization
        void Start()
        {
            state = GameState.FreeRoam;

            DialogManager.Instance.OnShowDialog += () => 
            { 
                state = GameState.Dialog; 
            };

            DialogManager.Instance.OnCloseDialog += () => 
            {
                if (state == GameState.Dialog) 
                { 
                    state = GameState.FreeRoam; 
                }
            };

            DialogManager.Instance.OnCloseDialog += () =>
            {
                if (state == GameState.Dialog)
                {
                    state = GameState.FreeRoam;
                }
            };

            DialogManager.Instance.OnStartBattle += () =>
            {
                StartBattle();
            };


            menuController = GetComponent<MenuController>();
            menuController.OnBack += () => { state = GameState.FreeRoam; };
            menuController.OnMenuSelected += OnMenuSelected;
        }


        void StartBattle()
        {
            state = GameState.Battle;
            battleSystem.gameObject.SetActive(true);
            worldCamera.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (state == GameState.FreeRoam) 
            {
                playerController.HandleUpdate();

                if (Input.GetKeyDown(KeyCode.Escape)) 
                {
                    menuController.OpenMenu();
                    state = GameState.Menu;
                }
            }

            else if (state == GameState.Battle)
            {
                battleSystem.HandleUpdate();
            }
            
            
            else if (state == GameState.Dialog) 
            {
                DialogManager.Instance.HandleUpdate();
            }


            else if (state == GameState.Menu)
            {
                menuController.HandleUpdate();
            }
        }


        void OnMenuSelected(int selectedItem) 
        { 
            if( selectedItem == 0)
            {
                Debug.Log("Pokemon selected");
            }

            if (selectedItem == 1)
            {
                Debug.Log("Bag selected");
            }

            if (selectedItem == 2)
            {
                Debug.Log("Save selected");
            }

            if (selectedItem == 3)
            {
                Debug.Log("Load selected");
            }

            state = GameState.FreeRoam;
        }
    }
}
