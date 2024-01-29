using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public enum PoolObjectType
{
    Step
}

public class Factory : Singleton<Factory>
{
    StepPool step;

    protected override void OnInitialize()
    {
        base.OnInitialize();

        step = GetComponentInChildren<StepPool>();
        if (step != null)
            step.Initialize();
    }
    public GameObject GetObject(PoolObjectType type, Vector3? position = null, Vector3? euler = null)
    {
        GameObject result = null;
        switch (type)
        {
            case PoolObjectType.Step:
                result = step.GetObject(position, euler).gameObject;
                break;
        }
        return result;
    }

    public Step GetStep()
    {
        return step.GetObject();
    }

    public Step GetStep(Vector3 position, float angle = 0.0f)
    {
        return step.GetObject(position, angle * Vector3.forward);
    }
}
