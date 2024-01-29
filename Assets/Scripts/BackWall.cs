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
        bwSlots = new Transform[transform.childCount];  // �迭 �����
        for (int i = 0; i < bwSlots.Length; i++)
        {
            bwSlots[i] = transform.GetChild(i);         // �迭�� �ڽ��� �ϳ��� �ֱ�
        }
        baseLineY = transform.position.y - backWallLength;
    }
    private void Update()
    {
        for (int i = 0; i < bwSlots.Length; i++)
        {
            if (bwSlots[i].position.y < baseLineY)  // ���ؼ��� �Ѿ����� Ȯ���ϰ�
            {
                MoveUp(i);                       // �Ѿ����� ������ ������ ������
            }
        }
    }

    void MoveUp(int index)
    {
        bwSlots[index].Translate(backWallLength * bwSlots.Length * transform.up);
    }
}
