using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using SmartLocalization;

public class FirstScane_SL_Script : MonoBehaviour {

    //-------Public text fields
    public Text txtEnterID;
    public List<Text> versionList = new List<Text>();

    //-------Smart Localiation Key-Value
    private string enterID_Key = "EnterID_Key";
    private string version_Key = "Version_Key";

    void Update()
    {       
       txtEnterID.text = LanguageManager.Instance.GetTextValue(enterID_Key);
        for (int i = 0; i < versionList.Count; i++)
            versionList[i].text = LanguageManager.Instance.GetTextValue(version_Key) + " " + (i + 1).ToString();
    }

    public void ChangeLanguage(string Language)
    {
        LanguageManager.Instance.ChangeLanguage(Language);
        Debug.Log(Language);
    }

	
}
