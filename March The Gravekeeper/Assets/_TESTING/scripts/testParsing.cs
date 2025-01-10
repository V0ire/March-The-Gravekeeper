using DIALOGUE;
using UnityEngine;

namespace TESTING
{
    public class testParsing : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string line = "Speaker \"Dialogue goes here!\" commands (arguments here)";

        DialogueParser.Parse(line);
    }   

    // Update is called once per frame
    void Update()
    {
        
    }
}
}
