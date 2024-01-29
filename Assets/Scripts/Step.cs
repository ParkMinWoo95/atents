using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Step : RecycleObject
{
    PlayerInputActions inputActions;

    public int score = 10;
    Player player;
    Action onScore;

    protected override void OnEnable()
    {
        OnInitialize();     // 적 초기화 작업

        inputActions.Player.Enable();
        inputActions.Player.Move.performed += Down;
        inputActions.Player.Move.canceled += Down;
    }

    protected override void OnDisable()
    {
        if (player != null)
        {
            onScore -= PlayerAddScore;    // 순차적으로 제거
            onScore = null;               // 확실하게 정리한다고 표시
            player = null;
        }

        inputActions.Player.Move.canceled += Down;
        inputActions.Player.Move.performed += Down;
        inputActions.Player.Disable();

        base.OnDisable();
    }

    protected virtual void OnInitialize()
    {
        if(player == null)
        {
            player = GameManager.Instance.Player;
        }
        if(player != null)
        {
            onScore += PlayerAddScore;
        }
    }

    void PlayerAddScore()
    {
        player.AddScore(score);
    }

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onScore?.Invoke();
        }
    }


    private void Down(InputAction.CallbackContext context)
    {
        float dir = context.ReadValue<float>();

        if (dir < 0)
        {
            transform.Translate(0.0f, -0.75f, 0.0f);
        }
        else if (dir > 0)
        {
            transform.Translate(0.0f, -0.75f, 0.0f);
        }
    }
}
