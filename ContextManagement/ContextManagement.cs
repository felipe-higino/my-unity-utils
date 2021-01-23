using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FelipeUtils.ContextManagement
{
    public static class StaticContextManager<E_Contexts> where E_Contexts : Enum
    {
        static public event Action<E_Contexts, E_Contexts> OnChangeContext = default;

        static private E_Contexts _Context = default;
        static public E_Contexts Context
        {
            get => _Context;
            set
            {
                var oldContext = _Context;
                var newContext = value;
                _Context = newContext;
                OnChangeContext?.Invoke(oldContext, newContext);
            }
        }
    }

    namespace Setter
    {
        public class ContextSetter<E_Contexts> : MonoBehaviour where E_Contexts : Enum
        {
            [SerializeField] E_Contexts selectedContext = default;
            public void Do_ChangeContext()
            {
                StaticContextManager<E_Contexts>.Context = selectedContext;
            }
        }
    }
}
