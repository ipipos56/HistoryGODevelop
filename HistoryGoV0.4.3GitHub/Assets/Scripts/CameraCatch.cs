using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CameraCatch : MonoBehaviour {

    private int otv = 1;
    [SerializeField]
    private Text tex;
    [SerializeField]
    private Button butTrue;
    [SerializeField]
    private Button butFalse;

    [SerializeField]
    private GameObject[] PersType;

    //private int heig = 0;
    private int widt = 0;

    public void Awake()
    {
        //heig = Screen.height;
        widt = Screen.width;
        tex.fontSize = widt / 17;
    }

    public IEnumerator Start()
    {
        Vector3 pos;
        pos.x = 0;
        pos.y = -6.53f;
        pos.z = 18;
        Instantiate(PersType[PlayerPrefs.GetInt("CatchEnType")], pos, Quaternion.Euler(0,180f,0));
        WWWForm form2 = new WWWForm();
        string f1 = PlayerPrefs.GetString("Name");
        string f2 = PlayerPrefs.GetString("Pass");
        form2.AddField("name", f1);
        form2.AddField("pass", f2);
        WWW www2 = new WWW("http://historygo.info/historygounity/getquest.php", form2);
        yield return www2;
        if (www2.error != null)
        {
            Debug.Log("error: " + www2.error);
            yield break;
        }
        //deb.text = www2.text;
        Debug.Log(www2.text);
        QuestGet quest = JsonUtility.FromJson<QuestGet>(www2.text);
        //tex.text = www2.text;
        tex.text = "Правда ли, что " + quest.question;
        otv = quest.otv;
        butFalse.enabled = true;
        butTrue.enabled = true;
    }

    public void ButtTrue()
    {
        if(otv == 1)
        {
            tex.text = "Правильно";
            StartCoroutine(RegisterPers());
        }
        else
        {
            tex.text = "Неправильно";
            StartCoroutine(Minus());
        }
        butFalse.enabled = false;
        butTrue.enabled = false;
    }

    public void ButtFalse()
    {
        if (otv == 0)
        {
            tex.text = "Правильно";
            StartCoroutine(RegisterPers());
        }
        else
        {
            tex.text = "Неправильно";
            StartCoroutine(Minus());
        }
        butFalse.enabled = false;
        butTrue.enabled = false;
    }

    private IEnumerator RegisterPers()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", PlayerPrefs.GetString("Name"));
        form.AddField("pass", PlayerPrefs.GetString("Pass"));
        form.AddField("id", PlayerPrefs.GetInt("CatchEnId"));
        WWW www = new WWW("http://historygo.info/historygounity/regpers.php", form);
        Debug.Log(www);
        yield return www;
        if (www.error != null)
        {
            Debug.Log("error: " + www.error);
            tex.text = www.error;
            yield break;
        }
        //tex.text = www.text;
        SceneManager.LoadScene(1);
    }

    private IEnumerator Minus()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", PlayerPrefs.GetString("Name"));
        form.AddField("pass", PlayerPrefs.GetString("Pass"));
        WWW www = new WWW("http://historygo.info/historygounity/minusmana.php", form);
        //Debug.Log(www);
        yield return www;
        if (www.error != null)
        {
            Debug.Log("error: " + www.error);
            yield break;
        }
        SceneManager.LoadScene(1);
    }



}
