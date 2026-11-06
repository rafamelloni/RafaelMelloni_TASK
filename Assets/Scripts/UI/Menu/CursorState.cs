using System.Collections.Generic;
using UnityEngine;

public static class CursorState
{
    private static readonly HashSet<Object> requests = new HashSet<Object>();

    public static void Show(Object owner)
    {
        requests.Add(owner);
        Apply();
    }

    public static void Hide(Object owner)
    {
        requests.Remove(owner);
        Apply();
    }

    public static void Apply()
    {
        requests.RemoveWhere(owner => owner == null);

        bool isVisible = requests.Count > 0;

        Cursor.visible = isVisible;
        Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;
    }
}