using UnityEngine;

/*A simple switch that changes the instructions depending on the device used*/
public class InfoBox : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField]
    GameObject pcInstructions;
    [SerializeField]
    GameObject androidInstructions;
#pragma warning restore 649

    private void Awake()
    {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        pcInstructions.SetActive(true);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR && !UNITY_STANDALONE_WIN
        androidInstructions.SetActive(true);
#endif
    }
}
