using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;
using UnityEngine.UI;

public class FirstScaneController : MonoBehaviour {


    public InputField inputField_UserID;

    //Size from DB
    float A, B, X, Y;

    //Thread bool flags
    bool flag = false, threadFlag=false, unThreadFlag = false;

    //Button index
    int btnIndex;
    
    //Audio
    AudioSource audioSourse;
    public AudioClip btnClickSound;



	void Start () {
      
        audioSourse = GetComponent<AudioSource>();
 
	}
	
	// Update is called once per frame
	void Update () {
        if (threadFlag && unThreadFlag)
        {
            SaveData(btnIndex);
            Application.LoadLevel(1);
        }

	}

    public void btn_Version_Click(int Version)
    {
      
        if (inputField_UserID.text != "")
        {
            ParseQuery<ParseObject> query = ParseObject.GetQuery("TestObject");
           query.FindAsync().ContinueWith(t =>
            {
                IEnumerable<ParseObject> result = t.Result;
                foreach (ParseObject parseObject in result)
                {
                    if (parseObject.Get<string>("User_ID") == inputField_UserID.text)
                    {
                        A = parseObject.Get<float>("A");
                        B = parseObject.Get<float>("B");
                        X = parseObject.Get<float>("X");
                        Y = parseObject.Get<float>("Y");
                        flag = true;
                        Debug.Log(A);
                    }
                }

                if (!flag)
                {
                    ParseObject testObject = new ParseObject("TestObject");  // Если объекта нет в базе , добавляем его
                    testObject["User_ID"] = inputField_UserID.text;          // и задаем ему параментры размера фигур
                    testObject["A"] = 1;
                    testObject["B"] = 1;
                    testObject["X"] = 1;
                    testObject["Y"] = 1;
                    testObject.SaveAsync();
                    A = 1;
                    B = 1;
                    X = 1;
                    Y = 1;
                    Debug.Log("Successful_ELSE");
                }
                
                threadFlag = true;
            });

           btnIndex = Version;
           unThreadFlag = true;
        }      
    }

    private void SaveData(int Version)
    {
        PlayerPrefs.SetFloat("A", A);
        PlayerPrefs.SetFloat("B", B);
        PlayerPrefs.SetFloat("X", X);
        PlayerPrefs.SetFloat("Y", Y);
        PlayerPrefs.SetString("User_ID", inputField_UserID.text); // Передаю во 2ю сцену Айдишник изера 
        PlayerPrefs.SetInt("Version", Version);// и номер кнопки которую нажали
        // ну на самом деле я слегка недопонял тз :) сделаю как понял сам 
    }
    
    // Sound 
    public void Play_btnClickSound()
    {
        audioSourse.Play();
    }


}
