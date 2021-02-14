using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LocalizationSystemText
{
    [Serializable]
    public class UnityEvent_TranslateText : UnityEvent<string> { }

    public class RequireLocalizedText : MonoBehaviour
    {
        internal static List<RequireLocalizedText> AllLocalizableTexts = new List<RequireLocalizedText>();

        [SerializeField]
        private string textLocalizationTag = default;

        [SerializeField, Space(15)]
        private UnityEvent_TranslateText MethodToChangeText = default;

        private int tagIndex = -1;

        public void SetKey(string key)
        {
            textLocalizationTag = key;
            var index = LocalizableTextSheet.GetLocalizedTextTagIndex(key);
            if (tagIndex == index)
                return;

            tagIndex = index;
            UpdateThisTextLanguage();
        }

        public void UpdateThisTextLanguage()
        {
            if (tagIndex < 0)
                SetKey(textLocalizationTag);

            var localizedText =
                LocalizableTextSheet.GetLocalizedTextByIndex(tagIndex);

            MethodToChangeText?.Invoke(localizedText);
        }

        private void Awake()
        {
            AllLocalizableTexts.Add(this);
        }

        private void OnDestroy()
        {
            AllLocalizableTexts.Remove(this);
        }

    }

}
