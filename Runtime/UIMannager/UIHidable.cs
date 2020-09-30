using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FelipeUtils.UIMannagement
{
    /// <summary>
    /// Must organize the screen game objects in alphabetic order
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class UIHidable : MonoBehaviour, IComparable<UIHidable>
    {
        private bool isHide = false;
        public bool IsHide { get => isHide; }

        CanvasGroup _canvasGroup = null;
        CanvasGroup CanvasGroup
        {
            get
            {
                if(_canvasGroup == null)
                    _canvasGroup = GetComponent<CanvasGroup>();
                return _canvasGroup;
            }
        }

        public virtual void Hide() 
        {
            CanvasGroup.alpha = 0;
            CanvasGroup.interactable = false;
            isHide = true; 
        }

        public virtual void Show()
        {
            CanvasGroup.alpha = 1;
            CanvasGroup.interactable = true;
            isHide = false;
        }

        //sort, Icomparable
        public int CompareTo(UIHidable obj)
        {
            var otherName = obj.name;
            var thisName = name;
            return String.Compare(thisName, otherName);
        }

    }
}
