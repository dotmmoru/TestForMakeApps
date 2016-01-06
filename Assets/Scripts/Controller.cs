using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;
using UnityEngine.UI;
using UnityEditor;
using System;
using System.Threading.Tasks;
public class Controller : MonoBehaviour {

    // Player Prefs values
    int Version; 
    string User_ID;
    // End

    //Public UI elements 
    public InputField InputField_A, InputField_B;
    public Slider Slider_X, Slider_Y;
    public Text txt_A, txt_B, txt_X, txt_Y;
    //End

    //Public Cube elements
    public GameObject Cube_A, Cube_B, Cube_X, Cube_Y;
    // End

	void Start () {

        GetDataFromParse();
       
       /* ParseObject testObject = new ParseObject("TestObject");
        testObject["foo"] = "bar";
        testObject.SaveAsync();*/

        Version = PlayerPrefs.GetInt("Version");
        User_ID = PlayerPrefs.GetString("User_ID");
        ChoseState();

        //Create listener for  input field
        InputField_A.onValueChange.AddListener(txt_A_Listener);
        InputField_B.onValueChange.AddListener(txt_B_Listener);
        //Create listener for slider
        Slider_X.onValueChanged.AddListener(Slider_X_Listener);
        Slider_Y.onValueChanged.AddListener(Slider_Y_Listener);


	}
	
	// Update is called once per frame
	void Update () {

	}

    // выбираем для работы нужне ui элементы 
    // быть может есть лучший вариант , если есть сообщите его, этот плох тем, что в больших интерфейсах очень неудобно все описывать ((
    //
    private void ChoseState() 
    {
        switch (Version)
        {
            case 1: 
                {
                    txt_A.gameObject.SetActive(true);
                    txt_B.gameObject.SetActive(true);
                    InputField_A.gameObject.SetActive(true);
                    InputField_B.gameObject.SetActive(true);
                    Cube_A.SetActive(true);
                    Cube_B.SetActive(true);
                } break;
            case 2: {
                    txt_A.gameObject.SetActive(true);
                    txt_B.gameObject.SetActive(true);
                    txt_X.gameObject.SetActive(true);
                    InputField_A.gameObject.SetActive(true);
                    InputField_B.gameObject.SetActive(true);
                    Slider_X.gameObject.SetActive(true);
                    Cube_A.SetActive(true);
                    Cube_B.SetActive(true);
                    Cube_X.SetActive(true);
                    } break;
            case 3: {
                    txt_X.gameObject.SetActive(true);
                    txt_Y.gameObject.SetActive(true);
                    Slider_X.gameObject.SetActive(true);
                    Slider_Y.gameObject.SetActive(true);
                    Cube_X.SetActive(true);
                    Cube_Y.SetActive(true);
                    } break;
        }
    }
    //
    //End
    //

    //
    //Listeners
    //
    private void txt_A_Listener(string arg)
    {
        if (arg != "")
        {
            float number = Convert.ToSingle(arg);
            if (number >= 0.1 && number <= 2)
                Cube_A.transform.localScale = new Vector3(number, number, number);
        }
    }
    private void txt_B_Listener(string arg)
    {
        if (arg != "")
        {
            float number = Convert.ToSingle(arg);
            if (number >= 0.1 && number <= 2)
                Cube_B.transform.localScale = new Vector3(number, number, number);
        }
    }
    private void Slider_X_Listener(float arg)
    {
        Cube_X.transform.localScale = new Vector3(arg, arg, arg);
    }
    private void Slider_Y_Listener(float arg)
    {
        Cube_Y.transform.localScale = new Vector3(arg, arg, arg);
    }
    //
    //End
    //

    //
    //Get&Update Data in Parse
    //

    private void GetDataFromParse()
    {

        var query = new ParseQuery<ParseObject>("TestObject")
     .WhereEqualTo("User_ID", User_ID);

        query.FindAsync().ContinueWith(t =>
        {
            var tokens = t.Result;
            IEnumerator<ParseObject> enumerator = tokens.GetEnumerator();
            enumerator.MoveNext();
            var token = enumerator.Current;
            token["A"] = 20;
            return token.SaveAsync();
        }).Unwrap().ContinueWith(t =>
        {
            // Everything is done!
            Debug.Log("Token has been updated!");
        });
    }
    //
    //End
    //
}
