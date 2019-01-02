using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class DataBaseLog : MonoBehaviour {

    public string UserName;
    public string Password;
    public InputField names;
    public InputField pass;
    public Button play;
    public Text deb;
    public Text nam;
    public Text nam2;
    public Text pa;
    public Text pa2;
    public Text bu;
    private int indPl = 0;
    private int widt;
    //private int heig;
    

    private void Start()
    {
        widt = Screen.width;
       // heig = Screen.height;
        deb.fontSize = widt / 20;
        nam.fontSize = widt / 12;
        nam2.fontSize = widt / 12;
        pa.fontSize = widt / 12;
        pa2.fontSize = widt / 12;
        bu.fontSize = widt / 12;
        PlayerPrefs.SetFloat("lat", 0f);
        PlayerPrefs.SetFloat("lon", 0f);
        if (PlayerPrefs.HasKey("Name"))
        {
            names.text = PlayerPrefs.GetString("Name");
            pass.text = PlayerPrefs.GetString("Pass");
        }
    }


    private IEnumerator RegisterUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("Name", UserName);
        form.AddField("Pass", Password);
        WWW www = new WWW("http://historygo.info/historygounity/index.php", form);
        //Debug.Log(www);
        yield return www;
        if(www.error != null)
        {
            deb.color = Color.red;
            Debug.Log("error: " + www.error);
            deb.text = www.error;
            yield break;
        }
        Debug.Log(www.text);
        bool check = Int32.TryParse(www.text, out indPl);
        if (check)
        {
            deb.color = Color.green;
            deb.text = "Успешная авторизация";
            SceneLoad();
        }
        else
            deb.text = www.text;
    }
    public void onClickLogin()
    {
        UserName = names.text;
        Password = pass.text;
        if (UserName != "" && Password != "")
        {
            PlayerPrefs.SetString("Name", names.text);
            PlayerPrefs.SetString("Pass", pass.text);
            StartCoroutine(RegisterUser());
        }
        else
        {
            deb.color = Color.red;
            deb.text = "Введите логин и пароль";
        }
    }

    private void SceneLoad()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
