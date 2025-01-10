using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;

public class testFiles : MonoBehaviour
{
    [SerializeField] private TextAsset fileName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(run());
    }

    IEnumerator run() {
        List<string> lines = FileManager.ReadTextAsset(fileName,false);

        foreach (string line in lines)
            Debug.Log(line);

        yield return null;
    }
}
