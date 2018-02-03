using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class InputUtils
{
    public static EventTrigger.Entry CreateEventTrigger(EventTriggerType id, Action callback)
    {
        var entry = new EventTrigger.Entry()
        {
            eventID = id
        };
        entry.callback.AddListener(r => callback());
        return entry;
    }
}
public class CoroutineUtils
{
    public static IEnumerator WaitThen(float time, Action then)
    {
        yield return new WaitForSeconds(time);
        then();
    }

    public static IEnumerator WaitOneFrameThen(Action then)
    {
        yield return new WaitForEndOfFrame();
        then();
    }

    public static IEnumerator LerpRoutine(Action<float> setValue, float from, float to, float timeInSeconds)
    {
        var wait = new WaitForEndOfFrame();

        var timeAcc = 0f;
        setValue(from);
        while (timeAcc < timeInSeconds)
        {
            var lerpValue = Mathf.Lerp(from, to, timeAcc / timeInSeconds);
            setValue(lerpValue);
            timeAcc += Time.deltaTime;
            yield return wait;
        }
        setValue(to);
    }

    public static IEnumerator LerpRoutine(Action<float> setValue, float from, float to, float timeInSeconds, Action OnFinish)
    {
        var wait = new WaitForEndOfFrame();

        var timeAcc = 0f;
        setValue(from);
        while (timeAcc < timeInSeconds)
        {
            var lerpValue = Mathf.Lerp(from, to, timeAcc / timeInSeconds);
            setValue(lerpValue);
            timeAcc += Time.deltaTime;
            yield return wait;
        }
        setValue(to);
        OnFinish();
    }

    public static IEnumerator DoEveryFrame(Action then)
    {
        var wait = new WaitForEndOfFrame();
        while (true)
        {
            then();
            yield return wait;
        }
    }
    public static IEnumerator DoWhile(Func<bool> condition, Action action)
    {
        var wait = new WaitForEndOfFrame();
        while (condition())
        {
            action();
            yield return wait;
        }
    }

    public static IEnumerator DoWhile(Func<bool> condition, Action action, Action OnFinish)
    {
        var wait = new WaitForEndOfFrame();
        while (condition())
        {
            action();
            yield return wait;
        }
        OnFinish();
    }

    public static IEnumerator WaitWhile(Func<bool> condition, Action action)
    {
        var wait = new WaitWhile(condition);
        yield return wait;
        action();
    }
}

public static class StringExt
{
    public static Tuple<int, string> FindFirst(this string s, string[] toSearch)
    {
        var index = s.Length;
        var first = "";
        foreach (var searchTerm in toSearch)
        {
            var aux = s.IndexOf(searchTerm);
            if (aux != -1 && aux < index)
            {
                index = aux;
                first = searchTerm;
            }
        }

        if (index >= 0 && first != "")
        {
            return Tuple.Create(index, first);
        }
        return Tuple.Create(-1, "");
    }
    public static string ReplaceFromToOcurrence(this string s, string replace, string from, string to)
    {
        var aux = s;
        var indexFrom = aux.IndexOf(from);
        var indexTo = aux.IndexOf(to) + to.Length;
        var toReplace = aux.Substring(indexFrom, indexTo - indexFrom);
        aux = s.Replace(toReplace, replace);

        return aux;
    }
    public static string ReplaceBetween(this string s, string replace, string from, string to)
    {
        var aux = s;
        var indexFrom = aux.IndexOf(from) + from.Length;
        var indexTo = aux.IndexOf(to);
        var toReplace = aux.Substring(indexFrom, indexTo - indexFrom);
        aux = s.Replace(toReplace, replace);

        return aux;
    }
    public static string GetTextBetween(this string s, string from, string to)
    {
        var aux = s;
        var indexFrom = aux.IndexOf(from) + from.Length;
        if (indexFrom < 0) return "-1";
        aux = aux.ReplaceFirst(from, "");


        var indexTo = aux.IndexOf(to) + from.Length;
        if (indexTo < 0) return "-1";
        aux = s.Substring(indexFrom, indexTo - indexFrom);

        return aux;
    }
    public static string ReplaceFirst(this string s, string oldString, string newString)
    {
        var aux = s;

        var indexFrom = aux.IndexOf(oldString);
        var start = aux.Substring(0, indexFrom);
        var end = aux.Substring(indexFrom + oldString.Length, s.Length - (indexFrom + oldString.Length));
        aux = start + newString + end;
        return aux;
    }
}
