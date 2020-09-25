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

        CanvasGroup canvasGroup = null;

        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public virtual void Hide() 
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            isHide = true; 
        }

        public virtual void Show()
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            isHide = false;
        }

        public int CompareTo(UIHidable obj)
        {
            var otherName = obj.name;
            var thisName = name;
            return String.Compare(thisName, otherName);
        }

    }
}
