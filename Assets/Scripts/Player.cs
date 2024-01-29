using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    Animator anim;
    PlayerInputActions inputActions;

    Vector3 inputDir = Vector3.zero;

    readonly int Input_String = Animator.StringToHash("Input");
    readonly int Atc_String = Animator.StringToHash("Atc");

    int score = 0;

    public int Score
    {
        get => score;
        private set
        {
            if( score != value)
            {
                score = Math.Min(value, 99999);
                onScoreChange?.Invoke(score);
            }
        }
    }

    public Action<int> onScoreChange;
    public Action<int> onDie;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
        inputActions.Player.Attack.performed += AttackEnemy;
        inputActions.Player.Attack.canceled += AttackEnemy;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        float dir = context.ReadValue<float>();

        anim.SetFloat(Input_String, Mathf.Abs(dir));
        if(dir < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.Translate(dir * 1.0f, 0.0f, 0.0f);
        }
        else if (dir > 0)
        { 
            transform.localScale = new Vector3(1, 1, 1);
            transform.Translate(dir * 1.0f, 0.0f, 0.0f);
        }
    }

    private void OnDisable()
    {
        inputActions.Player.Move.canceled += OnMove;
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Attack.canceled -= AttackEnemy;
        inputActions.Player.Attack.performed -= AttackEnemy;
        inputActions.Player.Disable();
    }

    private void AttackEnemy(InputAction.CallbackContext context)
    {
        float dir = context.ReadValue<float>();
        anim.SetFloat(Atc_String, dir);
    }

    public void AddScore(int getScore)
    {
        Score += getScore;
    }

    private void Update()
    {
        if(transform.position.y <= -3.0f)
        {
            OnDie();
        }
    }

    void OnDie()
    {
          Collider2D body = GetComponent<Collider2D>();
          body.enabled = false;

          inputActions.Player.Disable();

          onDie?.Invoke(Score);
    }

}
