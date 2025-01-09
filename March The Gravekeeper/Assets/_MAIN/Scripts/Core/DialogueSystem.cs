using Unity.VisualScripting;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private DialogueContainer dialogueContainer = new DialogueContainer();

    public static DialogueSystem instance; 

    private void awake() {
        if (instance = null) instance = this;
        else DestroyImmediate(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
