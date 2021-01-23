using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Actions
{
    public class ChangeAudiolistenerVolume : MonoBehaviour
    {
        public void ChangeGameAudioVolume(Slider slider)
        {
            AudioListener.volume = slider.value;
        }
    }

}
