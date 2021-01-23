using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
// using FelipeUtils.Lambda;

public class Evaluatetest : MonoBehaviour
{
    List<string> collection = new List<string> { "sdsd", "aaaaa", "btb" };
    IEnumerable<bool> tocall = default;

    private void Awake()
    {
        tocall = collection.Select(x =>
        {
            Debug.Log(x);
            return true;//returns anything
        });
    }

    [ContextMenu("Evaluate")]
    void Evaluate()
    {
        // tocall.Evaluate();
    }
}
