using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using TMPro;

public class InputDateMask : MonoBehaviour {
    public TMP_InputField inputField;

    private void Awake () {
        inputField.onValueChanged.AddListener (delegate { OnValueChanged (); });
    }

    private void LateUpdate()
    {
        if(inputField != null && inputField.gameObject.activeSelf)
            inputField.MoveToEndOfLine (false, true);
    }

    public void OnValueChanged () {
        if (string.IsNullOrEmpty (inputField.text)) {
            inputField.text = string.Empty;
        } else {
            string input = inputField.text;
            string MatchPattern = @"^((\d{2}-){0,2}(\d{1,2})?)$";
            string ReplacementPattern = "$1-$3";
            string ToReplacePattern = @"((\.?\d{2})+)(\d)";

            input = Regex.Replace (input, ToReplacePattern, ReplacementPattern);
            Match result = Regex.Match (input, MatchPattern);
            if (result.Success) {
                inputField.text = input;
                inputField.caretPosition++;
            }
        }
    }
}