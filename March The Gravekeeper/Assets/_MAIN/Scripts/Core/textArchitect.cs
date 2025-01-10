using System.Collections;
using UnityEngine;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine.TextCore.Text;
using UnityEngine.Accessibility;
using UnityEditor.Rendering;

public class textArchitect
{
    private TextMeshProUGUI tmpro_ui;
    private TextMeshPro tmpro_world;
    public TMP_Text tmpro => tmpro_ui != null ? tmpro_ui : tmpro_world;
    
    public string currentText => tmpro.text;
    public string targetText { get; private set; } = "";
    public string preText { get; private set; } = "";
    private int preTextLength = 0;

    public string fullTargetText => preText + targetText;

    public enum BuildMethod { instant, typewriter, fade}
    public BuildMethod buildMethod = BuildMethod.typewriter;

    public Color textColor { get { return tmpro.color; } set {tmpro.color = value; } }

    public float speed  { get { return baseSpeed * speedMultiplier; } set { speedMultiplier = value; } }
    private const float baseSpeed = 1;
    private float speedMultiplier = 1;

    public int charPerCycle { get { return speed <= 2f ? charMultiplier : speed <= 2.5f ? charMultiplier * 2 : charMultiplier * 3; } }
    private int charMultiplier = 1; 

    public bool hurryUp = false;

    
    public textArchitect(TextMeshProUGUI tmpro_ui) {
        this.tmpro_ui = tmpro_ui;
    }

    public textArchitect(TextMeshPro tmpro_world) {
        this.tmpro_world = tmpro_world;
    }

    public Coroutine Build(string text) {
        preText = "";
        targetText = text;

        stop();

        buildProcess = tmpro.StartCoroutine(building());
        return buildProcess;
    }

    public Coroutine Append(string text) {
        preText = tmpro.text;
        targetText = text;

        stop();

        buildProcess = tmpro.StartCoroutine(building());
        return buildProcess;
    }

    private Coroutine buildProcess = null;
    public bool isBuilding => buildProcess != null;

    public void stop() {
        if (!isBuilding) return;

        tmpro.StopCoroutine(buildProcess);
        buildProcess = null;
    }

    IEnumerator building() {
        Prepare();

        switch (buildMethod) {
            case BuildMethod.typewriter : 
                yield return Build_Typewriter();
                break;
            case BuildMethod.fade : 
                yield return Build_Fade();
                break;
        }

        onComplete();
    }

    private void onComplete() {
        buildProcess = null;
        hurryUp = false;
    }

    public void forceComplete() {
        switch (buildMethod) {
            case BuildMethod.typewriter : 
                tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
                break;
            case BuildMethod.fade : break;
        }

        stop();
        onComplete();
    }

    private void Prepare() {
        switch (buildMethod) {
            case BuildMethod.instant :
                Prepare_Instant();
                break;
            case BuildMethod.typewriter :
                Prepare_Typewriter();
                break;
            case BuildMethod.fade :
                Prepare_Fade();
                break;
        }
    }

    private void Prepare_Instant() {
        tmpro.color = tmpro.color;
        tmpro.text = fullTargetText;
        tmpro.ForceMeshUpdate();
        tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
    }

    private void Prepare_Typewriter() {
        tmpro.color = tmpro.color;
        tmpro.maxVisibleCharacters = 0;
        tmpro.text = preText;

        if(preText != "") {
            tmpro.ForceMeshUpdate();
            tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
        }

        tmpro.text += targetText;
        tmpro.ForceMeshUpdate();
    }

    private void Prepare_Fade() {
        
    }

    private IEnumerator Build_Typewriter() {
        while (tmpro.maxVisibleCharacters < tmpro.textInfo.characterCount) {
            tmpro.maxVisibleCharacters += hurryUp ? charPerCycle * 5 : charPerCycle;

            yield return new WaitForSeconds(0.015f / speed);
        }
    }

    private IEnumerator Build_Fade() {
        yield return null;        
    }
}
