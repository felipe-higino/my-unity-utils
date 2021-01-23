using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actions
{
    /// <summary>
    /// Destroys the game object if is an builded instance of game
    /// </summary>
    class DevOnlyObject : MonoBehaviour
    {
        [Tooltip(
        @"Set true if this game object would appear on play.
Game object is destroyed if game is not playing in editor"
        )]
        [SerializeField] bool appearOnPlay = true;

        private void Awake()
        {
            if (!appearOnPlay || !Application.isEditor)
                Destroy(gameObject);
        }
    }

}
