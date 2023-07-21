using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeInvoker : MonoBehaviour
{
    public static UpgradeInvoker Instance;
    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        else
        {
            Instance = this;
        }
    }
    public void InvokeUpgradeCommand(IUpgradeCommand command)
    {
        command.Execute();
    }
}
