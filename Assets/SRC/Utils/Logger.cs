using UnityEngine;

public static class Logger
{
    
    public static void Error(string err)
    {
        Debug.LogError(err);        
    }

    public static void Print(string err)
    {
        Debug.Log(err);
    }
}