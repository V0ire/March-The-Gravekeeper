using System.Text.RegularExpressions;
using NUnit.Framework.Internal.Commands;
using UnityEngine;

namespace DIALOGUE
{
    public class DialogueParser {

        private const string commandRegexPattern = "\\w*[*\\s]\\(";
        public static DialogueLine Parse (string rawline) {
            Debug.Log($"Parsing Line  - '{rawline}'");

            (string speaker, string dialogue, string commands) = Ripcontent(rawline);

            Debug.Log($"Speaker = '{speaker}'\nDialogue  = '{dialogue}'\nCommands = '{commands}'");

            return new DialogueLine(speaker, dialogue, commands);
        }
    


    private static (string, string, string) Ripcontent (string rawline) {
        string speaker = "", dialogue = "", commands = "";

        int dialogueStart = -1;
        int dialogueEnd = -1;
        bool isEscaped = false;

        for (int i = 0; i < rawline.Length; i++) {
            char current = rawline[i];
            if (current == '\\')
                isEscaped = !isEscaped;
            else if (current == '*' && !isEscaped) {
                if (dialogueStart == -1)
                    dialogueStart = i;
                else if (dialogueEnd == -1)
                    dialogueEnd = i;
            }
            else isEscaped = false;
        }

        Regex commandRegex = new Regex(commandRegexPattern);
        Match match = commandRegex.Match(rawline);
        int commandStart = -1;
        if (match.Success) {
            commandStart = match.Index;

            if (dialogueStart == -1 && dialogueEnd == -1) return ("","", rawline.Trim());
        }

        if (dialogueStart != -1 && dialogueEnd != -1 && (commandStart == -1 || commandStart > dialogueEnd)) {
            speaker = rawline.Substring(0, dialogueStart).Trim();
            dialogue = rawline.Substring(dialogueStart + 1, dialogueEnd - dialogueStart - 1).Replace ("\\\"","\"");
            if (commandStart != -1) commands = rawline.Substring(commandStart).Trim();
        }
        else if (commandStart != -1 && dialogueStart > commandStart) commands = rawline;
        else speaker = rawline;

        return (speaker, dialogue, commands);
    }
    }
}
