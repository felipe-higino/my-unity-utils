using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace FelipeUtils.UIMannagement
{
    public abstract class UIMannager : Singleton<UIMannager>
    {
        void Awake()
        {
            AwakeSingleton(this);
        }

        protected UIHidable[] Screens;
    
        protected void StartDefault()
        {
            Screens = FindObjectsOfType<UIHidable>();
            Array.Sort(Screens);
        }

        protected void HideAll()
        {
            foreach (var screen in Screens)
                screen.Hide();
        }
    }
}
