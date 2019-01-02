using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AttackNo : MonoBehaviour {

    [SerializeField]
    private Text tex;
    [SerializeField]
    private Text info;
    [SerializeField]
    private Text ques;

    [SerializeField]
    private Text catc;
    [SerializeField]
    private Text exit;
    [SerializeField]
    private GameObject col;

    [SerializeField]
    private Button cat;

    //private int heig = 0;
    private int widt = 0;

    public void Awake()
    {
        //heig = Screen.height;
        widt = Screen.width;
        tex.fontSize = widt / 18;
        info.fontSize = widt / 18;
        ques.fontSize = widt / 18;
        catc.fontSize = widt / 22;
        exit.fontSize = widt / 22;

        Vector3 pos;
        pos.x = 0;
        pos.y = 4.5f;
        pos.z = 30;
        Instantiate(col, pos, Quaternion.Euler(-90, 180, 0));
    }

    public IEnumerator Start()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", PlayerPrefs.GetString("Name"));
        form.AddField("pass", PlayerPrefs.GetString("Pass"));
        form.AddField("id", PlayerPrefs.GetInt("CatchBLId").ToString());
        WWW www = new WWW("http://historygo.info/historygounity/blintinfo.php", form);
        Debug.Log(www);
        yield return www;
        if (www.error != null)
        {
            Debug.Log("error: " + www.error);
            tex.text = www.error;
            yield break;
        }
        BlintGet blint = JsonUtility.FromJson<BlintGet>(www.text);
        if (blint.playercatch == 34)
            tex.text = "Blint никто не захватил";
        else
            tex.text = "Blint захватил " + blint.catchname;
        info.text = "Уровень точки Blint " + blint.level;
        if (blint.catchname == PlayerPrefs.GetString("Name"))
        {
            //tex.text = "Blint захватили вы";
            ques.text = "Количество вопросов для улучшения точки " + blint.questcount;
            if (blint.level == 10)
                cat.enabled = false;
        }
        else
        {
            ques.text = "Количество вопросов для захвата " + blint.questcount;
        }
        //tex.text = www.text;
        //yield return new WaitForSeconds(3f);
        //SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        SceneManager.LoadScene(1);
    }

    public void Catch()
    {
        SceneManager.LoadScene(5);
    }
}
