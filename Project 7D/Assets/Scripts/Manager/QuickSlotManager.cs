
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Debug.Log("1번");
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            Debug.Log("2번");
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            Debug.Log("3번");
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            Debug.Log("4번");
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            Debug.Log("5번");
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            Debug.Log("6번");
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            Debug.Log("7번");
        else if (Input.GetKeyDown(KeyCode.Alpha8))
            Debug.Log("8번");
        else if (Input.GetKeyDown(KeyCode.Alpha9))
            Debug.Log("9번");
    }
}
