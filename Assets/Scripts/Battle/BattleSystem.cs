using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public enum BattleState
    {
        Start, PlayerAction, PlayerMove, EnemyMove, Busy
    }

    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleHud enemyHud;
    [SerializeField] BattleDialogBox dialogBox;

    BattleState state;
    int currentAction;
    int currentMove;


    public void Start()
    {
        
        StartCoroutine(SetupBattle());
    }

    public void HandleUpdate()
    {
        //put battle state code here
    }

    public IEnumerator SetupBattle()
    {
        playerUnit.Setup();
        enemyUnit.Setup();
        playerHud.SetData(playerUnit.Char);
        enemyHud.SetData(enemyUnit.Char);

        dialogBox.SetMoveNames(playerUnit.Char.Moves);

        yield return dialogBox.TypeDialog($"A {enemyUnit.Char.Base.Name} appeared.");
        yield return new WaitForSeconds(1f);

        PlayerAction();
    }

    void PlayerAction()
    {
        state = BattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog("Choose an action"));

        dialogBox.EnableActionSelector(true);
    }

    void PlayerMove()
    {
        state = BattleState.PlayerMove;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }

    IEnumerator PerformPlayerMove()
    {
        state = BattleState.Busy;

        var move = playerUnit.Char.Moves[currentMove];

        yield return dialogBox.TypeDialog($"{playerUnit.Char.Base.Name} used {move.Base.Name}");
        yield return new WaitForSeconds(1f);

        bool isFainted = enemyUnit.Char.TakeDamage(move, playerUnit.Char);
        yield return enemyHud.UpdateHP();

        if (isFainted)
        {
            yield return dialogBox.TypeDialog($"{enemyUnit.Char.Base.Name} fainted!");
        }
        else
        {
            StartCoroutine(EnemyMove());
        }
    }

    IEnumerator EnemyMove()
    {
        state = BattleState.EnemyMove;

        var move = enemyUnit.Char.GetRandomMove();

        yield return dialogBox.TypeDialog($"{enemyUnit.Char.Base.Name} used {move.Base.Name}");
        yield return new WaitForSeconds(1f);

        bool isFainted = playerUnit.Char.TakeDamage(move, enemyUnit.Char);
        yield return playerHud.UpdateHP();

        if (isFainted)
        {
            yield return dialogBox.TypeDialog($"{playerUnit.Char.Base.Name} fainted!");
        }
        else
        {
            PlayerAction();
        }
    }


    private void Update()
    {
        if (state == BattleState.PlayerAction) 
        {
            HandleActionSelection();
        }
        else if (state == BattleState.PlayerMove)
        {
            HandleMoveSelection();
        }
    }

    void HandleActionSelection()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if(currentAction < 1)
            {
                ++currentAction;
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (currentAction > 0)
            {
                --currentAction;
            }
        }

        dialogBox.UpdateActionSelection(currentAction);


        if (Input.GetKeyDown(KeyCode.E))
        {
            if(currentAction == 0)
            {
                //fight
                PlayerMove();
            }
            else if (currentAction == 1)
            {
                //run
            }
        }
    }

    void HandleMoveSelection()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentMove < playerUnit.Char.Moves.Count -1)
            {
                ++currentMove;
            }

        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentMove > 0)
            {
                --currentMove;
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (currentMove < playerUnit.Char.Moves.Count - 2)
            {
                currentMove += 2;
            }

        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentMove > 1)
            {
                currentMove -=2;
            }
        }

        dialogBox.UpdateMovesSelection(currentMove, playerUnit.Char.Moves[currentMove]);


        if (Input.GetKeyDown(KeyCode.E))
        {
            dialogBox.EnableMoveSelector(false);
            dialogBox.EnableDialogText(true);

            StartCoroutine(PerformPlayerMove());
        }
    }
}
