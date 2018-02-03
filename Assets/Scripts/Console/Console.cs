using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    public CommandList commandList;

    public InputField inputText;
    public Text consoleText;

    private void Start()
    {
        WriteLine("Welcome to the console!");
        WriteLine("Here is a list of all loaded commands.");
        WriteLine("\n");
        ListAllCommands();
    }

    public void SendCommand(string commandName)
    {
        var command = commandList.FindCommand(commandName);
        if (command != null)
        {
            command.Execute();
        }
        else WriteLine("Command not found");
        inputText.text = "";
    }

    public void WriteLine(string text)
    {
        consoleText.text += "\n" + text;
    }

    public void ListAllCommands()
    {
        foreach (var command in commandList.commands)
        {
            WriteLine(command.name + ": " + command.description);
        }
    }

    public void ClearConsoleText()
    {
        consoleText.text = "";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) SendCommand(inputText.text);
    }
}
