using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CatchColisei : MonoBehaviour {

    private int otv = 1;
    [SerializeField]
    private Text tex;
    [SerializeField]
    private Text counText;
    [SerializeField]
    private Button butTrue;
    [SerializeField]
    private Button butFalse;

    private string te;

    [SerializeField]
    private GameObject Colis;

    private int count;

    private int heig = 0;
    private int widt = 0;

    public void Awake()
    {
        heig = Screen.height;
        widt = Screen.width;
        tex.fontSize = widt / 17;
        counText.fontSize = widt / 20;
    }

    public void Start()
    {
        count = PlayerPrefs.GetInt("CatchBlCount");
        te = Endword(count);
        counText.text = "Вам осталось ответить на " + count.ToString() + te;
        Vector3 pos;
        pos.x = 0;
        pos.y = -4f;
        pos.z = 30;
        Instantiate(Colis, pos, Quaternion.Euler(-90, 180, 0));
        StartCoroutine(Ques());
    }

    public string Endword(int count)
    {
        string te = "";
        if (count == 1)
            te = " вопрос";
        else if (count > 1 && count < 5)
            te = " вопроса";
        else if (count > 4 || count == 0)
            te = " вопросов";
        return te;
    }

    public IEnumerator Ques()
    {
        WWWForm form2 = new WWWForm();
        string f1 = PlayerPrefs.GetString("Name");
        string f2 = PlayerPrefs.GetString("Pass");
        form2.AddField("name", f1);
        form2.AddField("pass", f2);
        form2.AddField("idpers", PlayerPrefs.GetInt("ChosPers").ToString());
        form2.AddField("id", PlayerPrefs.GetInt("CatchBLId").ToString());
        WWW www2 = new WWW("http://historygo.info/historygounity/getquestcolis.php", form2);
        yield return www2;
        yield return new WaitForSeconds(2f);
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
        butFalse.enabled = false;
        butTrue.enabled = false;
        if (otv == 1)
        {
            tex.text = "Правильно";
            if(count>1)
            {
                count--;
                te = Endword(count);
                counText.text = "Вам осталось ответить на " + count.ToString() + te;
                StartCoroutine(Ques());
            }
            else
            {
                tex.text = "Blint захвачен";
                StartCoroutine(RegisterPers());
            }
        }
        else
        {
            tex.text = "Неправильно";
            StartCoroutine(Minus());
        }
    }

    public void ButtFalse()
    {
        butFalse.enabled = false;
        butTrue.enabled = false;
        if (otv == 0)
        {
            tex.text = "Правильно";
            if (count > 1)
            {
                count--;
                te = Endword(count);
                counText.text = "Вам осталось ответить на " + count.ToString() + te;
                StartCoroutine(Ques());
            }
            else
            {
                tex.text = "Blint захвачен";
                StartCoroutine(RegisterPers());
            }
        }
        else
        {
            tex.text = "Неправильно";
            StartCoroutine(Minus());
        }
    }

    private IEnumerator RegisterPers()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", PlayerPrefs.GetString("Name"));
        form.AddField("pass", PlayerPrefs.GetString("Pass"));
        form.AddField("id", PlayerPrefs.GetInt("CatchBLId").ToString());
        form.AddField("idpers", PlayerPrefs.GetInt("ChosPers").ToString());
        WWW www = new WWW("http://historygo.info/historygounity/catchblint.php", form);
        Debug.Log(www);
        yield return www;
        if (www.error != null)
        {
            Debug.Log("error: " + www.error);
            tex.text = www.error;
            yield break;
        }
        //tex.text = www.text;
        yield return new WaitForSeconds(3f);
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
