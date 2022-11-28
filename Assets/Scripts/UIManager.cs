using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Manager manager;

    private void Start()
    {
        manager = Manager.Instance;
    }
    public void ResetDots()
    {
        manager.RecreateDots();
    }
}
