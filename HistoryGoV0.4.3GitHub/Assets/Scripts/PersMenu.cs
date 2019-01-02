using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PersMenu : MonoBehaviour
{

    [SerializeField]
    private Text intlvl;
    [SerializeField]
    private Text ex;
    private int[] inlvs;
    private int i = 0;
    private int j = 0;
    private int widt;
    //private int heig;
    private GameObject ob;

    [SerializeField]
    private Button exit;
    [SerializeField]
    private Button stor;
    [SerializeField]
    private Text storte;


    [SerializeField]
    private Image img;
    [SerializeField]
    private Button ye;
    [SerializeField]
    private Button no;
    [SerializeField]
    private Text ans;
    [SerializeField]
    private Text yete;
    [SerializeField]
    private Text note;

    private PersGet person;

    [SerializeField]
    private GameObject[] PersType;


    public void Awake()
    {
        widt = Screen.width;
        intlvl.fontSize = widt / 16;
        ex.fontSize = widt / 22;
        yete.fontSize = widt / 22;
        note.fontSize = widt / 22;
        storte.fontSize = widt / 22;
    }
    public IEnumerator Start()
    {
        WWWForm form2 = new WWWForm();
        form2.AddField("name", PlayerPrefs.GetString("Name"));
        form2.AddField("pass", PlayerPrefs.GetString("Pass"));
        form2.AddField("id", PlayerPrefs.GetString("idload"));
        WWW www2 = new WWW("http://historygo.info/historygounity/getpers.php", form2);
        //Debug.Log(www);
        yield return www2;
        if (www2.error != null)
        {
            Debug.Log("error: " + www2.error);
            //deb.text = www2.error;
            yield break;
        }
        //deb.text = www2.text;
        if (www2.text == null)
        {
            SceneManager.LoadScene(1);
        }
        person = JsonUtility.FromJson<PersGet>(www2.text);
        intlvl.text = "Уровень интелекта: " + person.intlvl.ToString();
        CreatePers(person.type);
    }

    public void OnExitClick()
    {
        SceneManager.LoadScene(1);
    }

    public void OnStorePers()
    {
        exit.enabled = false;
        stor.enabled = false;
        img.enabled = true;
        ye.enabled = true;
        ye.image.enabled = true;
        no.enabled = true;
        no.image.enabled = true;
        ans.enabled = true;
        yete.enabled = true;
        note.enabled = true;
        Destroy(ob);
    }

    public void CreatePers(int id)
    {
        Vector3 pos;
        pos.x = 0;
        pos.y = -2.88f;
        pos.z = 27;
        ob = Instantiate(PersType[id], pos, Quaternion.Euler(0, 180f, 0));
    }

    public void YesClick()
    {
        CreatePers(person.type);
        img.enabled = false;
        ye.enabled = false;
        ye.image.enabled = false;
        no.enabled = false;
        no.image.enabled = false;
        ans.enabled = false;
        yete.enabled = false;
        note.enabled = false;
        StartCoroutine(StorePers());
    }
    public void NoClick()
    {
        CreatePers(person.type);
        img.enabled = false;
        ye.enabled = false;
        ye.image.enabled = false;
        no.enabled = false;
        no.image.enabled = false;
        ans.enabled = false;
        yete.enabled = false;
        note.enabled = false;
        exit.enabled = true;
        stor.enabled = true;
    }

    private IEnumerator StorePers()
    {
        WWWForm form2 = new WWWForm();
        form2.AddField("name", PlayerPrefs.GetString("Name"));
        form2.AddField("pass", PlayerPrefs.GetString("Pass"));
        form2.AddField("id", (person.id).ToString());
        WWW www2 = new WWW("http://historygo.info/historygounity/storepers.php", form2);
        //Debug.Log(www);
        yield return www2;
        if (www2.error != null)
        {
            Debug.Log("error: " + www2.error);
            //deb.text = www2.error;
            yield break;
        }
        //deb.text = www2.text;
        SceneManager.LoadScene(1);
    }
}