using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//[CreateAssetMenu(fileName = "Command", menuName = "Console/Command")]
[System.Serializable]
public class Command
{
    public string name;
    public string description;
    public UnityEvent action;

    public void Execute()
    {
        action.Invoke();
    }
}
