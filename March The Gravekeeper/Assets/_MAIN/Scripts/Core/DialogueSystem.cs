using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public DialogueContainer dialogueContainer = new DialogueContainer();

    public static DialogueSystem instance; 

    private void Awake() {
        if (instance == null) instance = this;
        else DestroyImmediate(gameObject);
    }
}
