### How to use
* Scene Transitions:
   * implement an MonoBehaviour, ISceneTransitions
   * inject this transition in SceneTransitionManager.Transition

* How to transition
    * Register the wanted scenes as Addressables
    * Call something like: 
    **SceneTransitionManager.LoadNewScene(*AssetReferenceScene*);**
    * Or just use an LoadAddressableScene MonoBehaviour and call **Do_ChangeScene()**

* Tips
    * create an SO Scene controller to have an easy GUI to load scenes