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

    //Size from DB
    float A, B, X, Y;
    //End

    //Public UI elements 
    public InputField InputField_A, InputField_B;
    public Slider Slider_X, Slider_Y;
    public Text txt_A, txt_B, txt_X, txt_Y;
    //End

    //Public Cube elements
    public GameObject Cube_A, Cube_B, Cube_X, Cube_Y;
    // End

	void Start () {

        Version = PlayerPrefs.GetInt("Version");
        User_ID = PlayerPrefs.GetString("User_ID");
        A=PlayerPrefs.GetFloat("A");
        B=PlayerPrefs.GetFloat("B");
        X= PlayerPrefs.GetFloat("X");
        Y = PlayerPrefs.GetFloat("Y");
        Debug.Log(Version.ToString());
        ChoseState();
     
        //Create listener for  input field
        InputField_A.onEndEdit.AddListener(txt_A_Listener);
        InputField_B.onEndEdit.AddListener(txt_B_Listener);
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
        CubeScale(Cube_A, PlayerPrefs.GetFloat("A"));
        CubeScale(Cube_B, PlayerPrefs.GetFloat("B"));
        CubeScale(Cube_X, PlayerPrefs.GetFloat("X"));
        CubeScale(Cube_Y, PlayerPrefs.GetFloat("Y"));
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
            {
                CubeScale(Cube_A, number);
                UpdateDataInTable("A",number);
            }
        }
    }
    private void txt_B_Listener(string arg)
    {
        if (arg != "")
        {
            float number = Convert.ToSingle(arg);
            if (number >= 0.1 && number <= 2)
            {
                CubeScale(Cube_B, number);
                UpdateDataInTable("B", number);
            }
        }
    }
    private void Slider_X_Listener(float arg)
    {
        CubeScale(Cube_X, arg);
        UpdateDataInTable("X", arg);
    }
    private void Slider_Y_Listener(float arg)
    {
        CubeScale(Cube_Y, arg);
        UpdateDataInTable("Y", arg);
    }
    //
    //End
    //

    //
    //Change Cube Scale
    //
    private void CubeScale(GameObject Cube, float Size)
    {
        if (Cube.active)
        Cube.transform.localScale = new Vector3(Size, Size, Size);
    }
    //
    //End
    //

    public void bntBack_Click()
    {
        Application.LoadLevel(0);
    }


    //
    //Update Data in Parse
    //
    private void UpdateDataInTable(string NameInTable, float ValueInTable)
    {

        var query = new ParseQuery<ParseObject>("TestObject")
     .WhereEqualTo("User_ID", User_ID);

        query.FindAsync().ContinueWith(t =>
        {
            var tokens = t.Result;
            IEnumerator<ParseObject> enumerator = tokens.GetEnumerator();
            enumerator.MoveNext();
            var token = enumerator.Current;
            token[NameInTable] = ValueInTable;
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
