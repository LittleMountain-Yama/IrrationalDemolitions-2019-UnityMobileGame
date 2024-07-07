using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    public delegate void Commands(params object[] parameterContainer);
    Dictionary<string, Commands> commandsDic = new Dictionary<string, Commands>();

    public InputField consoleView;
    public InputField consoleInput;

    CannonControler _cC;
    GameManager _gm;

    private void Awake()
    {
        _cC = FindObjectOfType<CannonControler>();
        _gm = FindObjectOfType<GameManager>();

        consoleView.lineType = InputField.LineType.MultiLineNewline;

        AddCommand("AddNormal", AddNormal);
        AddCommand("AddExplosive", AddExplosive);
        AddCommand("AddTriple" , AddTriple);
        AddCommand("AddPoints", AddPoints);
        AddCommand("GoToScene", GoToScene);
    }

    void AddCommand(string cmdText, Commands cmdVoid)
    {       
        if (!commandsDic.ContainsKey(cmdText))
            commandsDic.Add(cmdText, cmdVoid);
        else
            commandsDic[cmdText] += cmdVoid;
    }

    public void EnterNewCommmand()
    {
        CheckKey(consoleInput.text);
    }

    void CheckKey(string key)
    {
        char[] delimiter = new char[] { '-', ' ' };
        string[] substrings = key.Split(delimiter);
        int value = -1;

        if (substrings.Length > 1)
            value = int.Parse(substrings[substrings.Length - 1]);

        if (commandsDic.ContainsKey(substrings[0]))
        {
            if (value != -1)
                commandsDic[substrings[0]](new object[] { value });
            else
                commandsDic[substrings[0]]();
        }
        else
            consoleView.text += "Command not available";
    }

    #region CommandVoids

    void AddNormal(params object[] parameters)
    {
        _cC.normalAmmo += ((int)parameters[0]);
        consoleView.text = "Added " + ((int)parameters[0]) + " normal proyectiles";
        consoleInput.text = "";
    }

    void AddExplosive(params object[] parameters)
    {
        _cC.explosiveAmmo += ((int)parameters[0]);
        consoleView.text = "Added " + ((int)parameters[0]) + " explosive proyectiles";
        consoleInput.text = "";
    }

    void AddTriple(params object[] parameters)
    {
        _cC.tripleAmmo += ((int)parameters[0]);
        consoleView.text = "Added " + ((int)parameters[0]) + " triple proyectiles";
        consoleInput.text = "";
    }

    void AddPoints(params object[] parameters)
    {
        _gm.points += ((int)parameters[0]);
        consoleView.text = "Added " + ((int)parameters[0]) + "points";
        consoleInput.text = "";
    }

    void GoToScene(params object[] parameters)
    {
        if ((int)parameters[0] <= 4 && (int)parameters[0] >= 0)
        {
            _gm.GoToScene((int)parameters[0]);
        }
    }

    #endregion
}
