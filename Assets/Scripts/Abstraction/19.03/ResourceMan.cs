using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class ResourceMan : MonoBehaviour
{
    private List<string> names;
    private List<int> counts;

    public List<string> Names { get => names; set => names = value; }
    public List<int> Counts { get => counts; set => counts = value; }

    public int AmmountShower(string name)
    {
        for (int i = 0; i < names.Count - 1; i++)
        {
            if (names[i] == name)
            {
                return counts[i];
            }
        }
        return -1;
    }
}
