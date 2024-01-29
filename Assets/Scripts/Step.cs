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
        OnInitialize();     // �� �ʱ�ȭ �۾�

        inputActions.Player.Enable();
        inputActions.Player.Move.performed += Down;
        inputActions.Player.Move.canceled += Down;
    }

    protected override void OnDisable()
    {
        if (player != null)
        {
            onScore -= PlayerAddScore;    // ���������� ����
            onScore = null;               // Ȯ���ϰ� �����Ѵٰ� ǥ��
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
