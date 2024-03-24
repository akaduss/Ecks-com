using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBusyUI : MonoBehaviour
{

    private void Start()
    {
        UnitActionSystem.Instance.OnBusyChanged += SetBusyUI;

        gameObject.SetActive(false);
    }

    private void SetBusyUI(bool isBusy)
    {
        gameObject.SetActive(isBusy);
    }


}
