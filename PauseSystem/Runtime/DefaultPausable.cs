using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultPausable : MonoBehaviour, IPausable
{
    public void OnPause()
    {
        gameObject.SetActive(false);
    }

    public void OnUnpause()
    {
        gameObject.SetActive(true);
    }
}
