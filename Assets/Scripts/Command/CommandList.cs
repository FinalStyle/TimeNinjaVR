using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Commands", menuName = "Console/CommandList")]
public class CommandList : MonoBehaviour
{
    public List<Command> commands;

    public Command FindCommand(string commandName)
    {
        foreach (var command in commands)
        {
            if (command.name.ToLower() == commandName.ToLower()) return command;
        }
        return null;
    }
}
