using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;
using UnityEngine.UI;
using UnityEditor;

public class FirstScaneController : MonoBehaviour {


    public InputField InputField_UserID;

    //Size from DB
    float A, B, X, Y;
    //End

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
	

	}

    public void btn_Version_Click(int Version)
    {
       
        if (InputField_UserID.text !="")
        {
            ParseQuery<ParseObject> query = ParseObject.GetQuery("TestObject");
            List<string> User_ID_List = new List<string>();
            query.FindAsync().ContinueWith(t =>             
            {
                IEnumerable <ParseObject> result = t.Result;    
                foreach (ParseObject parseObject in result)
                {
                    User_ID_List.Add(parseObject.Get<string>("User_ID"));      
                }

                if (!User_ID_List.Contains(InputField_UserID.text))
                {
                    ParseObject testObject = new ParseObject("TestObject");  // Если объекта нет в базе , добавляем его
                    testObject["User_ID"] = InputField_UserID.text;          // и задаем ему параментры размера фигур
                    testObject["A"] = 1;
                    testObject["B"] = 1; 
                    testObject["X"] = 1;
                    testObject["Y"] = 1;
                    testObject.SaveAsync();
                    Debug.Log("Successful");
                }

               
            });
            GetDataFromTable();
           
            Application.LoadLevel(1);
        }      
    }
    private void GetDataFromTable()
    {
        var query = new ParseQuery<ParseObject>("TestObject").WhereEqualTo("User_ID", InputField_UserID.text);
        query.FindAsync().ContinueWith(t =>
        {
            IEnumerable<ParseObject> result = t.Result;
            foreach (ParseObject parseObject in result)
            {
                A = parseObject.Get<float>("A");
                B = parseObject.Get<float>("B");
                X = parseObject.Get<float>("X");
                Y = parseObject.Get<float>("Y");
            }
        });

       
    }

    private void SaveData(int Version)
    {
        PlayerPrefs.SetFloat("A", A);
        PlayerPrefs.SetFloat("B", B);
        PlayerPrefs.SetFloat("X", X);
        PlayerPrefs.SetFloat("Y", Y);
        PlayerPrefs.SetString("User_ID", InputField_UserID.text); // Передаю во 2ю сцену Айдишник изера 
        PlayerPrefs.SetInt("Version", Version);// и номер кнопки которую нажали
        // ну на самом деле я слегка недопонял тз :) сделаю как понял сам 
    }
    

}
