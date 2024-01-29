using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackWall : MonoBehaviour
{
    const float backWallLength = 10.0f;

    float baseLineY;

    Transform[] bwSlots;
    private void Awake()
    {
        bwSlots = new Transform[transform.childCount];  // 배열 만들고
        for (int i = 0; i < bwSlots.Length; i++)
        {
            bwSlots[i] = transform.GetChild(i);         // 배열에 자식을 하나씩 넣기
        }
        baseLineY = transform.position.y - backWallLength;
    }
    private void Update()
    {
        for (int i = 0; i < bwSlots.Length; i++)
        {
            if (bwSlots[i].position.y < baseLineY)  // 기준선을 넘었는지 확인하고
            {
                MoveUp(i);                       // 넘었으면 오른쪽 끝으로 보내기
            }
        }
    }

    void MoveUp(int index)
    {
        bwSlots[index].Translate(backWallLength * bwSlots.Length * transform.up);
    }
}
