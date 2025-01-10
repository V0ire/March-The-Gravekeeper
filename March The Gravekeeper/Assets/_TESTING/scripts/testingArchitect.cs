using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace TESTING {
    public class testingArchitect : MonoBehaviour
    {
        DialogueSystem ds;
        textArchitect architect;

        string[] lines = new string[3] {
            "contoh kalimat 1",
            "contoh kalimat 2",
            "contoh kalimat 3"
        };

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            ds = DialogueSystem.instance;
            architect = new textArchitect(ds.dialogueContainer.dialogueText);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (architect.isBuilding) {
                    if (!architect.hurryUp)
                        architect.hurryUp = true;
                    else architect.forceComplete();
                }
                architect.Build(lines[Random.Range(0,lines.Length)]);
            }
            else if (Input.GetKeyDown(KeyCode.A)) {
                architect.Append(lines[Random.Range(0,lines.Length)]);
            }
        }
    }
}