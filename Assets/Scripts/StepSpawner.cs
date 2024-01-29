using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class StepSpawner : MonoBehaviour
{
    public int rand;


    public GameObject stepPrefab;

    public float currentPositionX;
    public float currentPositionY;
    PlayerInputActions inputActions;

    const float MinX = -5.5f;
    const float MaxX = 5.5f;
    Vector3 pos;

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += Make;
        inputActions.Player.Move.canceled += Make;
    }
    private void OnDisable()
    {
        inputActions.Player.Move.canceled += Make;
        inputActions.Player.Move.performed += Make;
        inputActions.Player.Disable();
    }

    private void Make(InputAction.CallbackContext context)
    {
        rand = Random.Range(0, 2);
        float dir = context.ReadValue<float>();

        if (dir == -1.0f) 
        {
            if (rand == 0)
            {
                if (currentPositionX == MinX)
                {
                    currentPositionX += 1.0f;
                }
                else
                {
                    currentPositionX -= 1.0f;
                }
                pos.x = currentPositionX;
                pos.y = currentPositionY;
                Spawn();
            }
            else if (rand == 1)
            {
                if (currentPositionX == MaxX)
                {
                    currentPositionX -= 1.0f;
                }
                else
                {
                    currentPositionX += 1.0f;
                }
                pos.x = currentPositionX;
                pos.y = currentPositionY;
                Spawn();
            }
        }
        if(dir == 1.0f)
        {
            if (rand == 0)
            {
                if (currentPositionX == MinX)
                {
                    currentPositionX += 1.0f;
                }
                else
                {
                    currentPositionX -= 1.0f;
                }
                pos.x = currentPositionX;
                pos.y = currentPositionY;
                Spawn();
            }
            else if (rand == 1)
            {
                if (currentPositionX == MaxX)
                {
                    currentPositionX -= 1.0f;
                }
                else
                {
                    currentPositionX += 1.0f;
                }
                pos.x = currentPositionX;
                pos.y = currentPositionY;
                Spawn();
            }
        }
    }

    protected virtual void Spawn()
    {
        Factory.Instance.GetStep(pos);
        //Instantiate(stepPrefab, pos, Quaternion.identity);
    }
    private void Awake()
    {
        rand = Random.Range(0, 1);
        inputActions = new PlayerInputActions();
        pos = transform.position;
    }

    private void Start()
    {
        pos.x = transform.position.x - 0.5f;
        pos.y = transform.position.y - 14.25f;
        currentPositionX = pos.x;
        currentPositionY = pos.y;

        Spawn();

        for (int i = 0; i < 18; i++)
        {
            if (rand == 0)
            {
                if(currentPositionX == MinX)
                {
                    currentPositionX += 1.0f;
                }
                else
                {
                    currentPositionX -= 1.0f;
                }
                currentPositionY += 0.75f;
                pos.x = currentPositionX;
                pos.y = currentPositionY;
                Spawn();
            }
            if (rand == 1)
            {
                if(currentPositionX == MaxX)
                { 
                    currentPositionX -= 1.0f;
                }
                else
                {
                    currentPositionX += 1.0f;
                }
                currentPositionY += 0.75f;
                pos.x = currentPositionX;
                pos.y = currentPositionY;
                Spawn();
            }
            rand = Random.Range(0, 2);
        }
        currentPositionY = transform.position.y;
    }
}
