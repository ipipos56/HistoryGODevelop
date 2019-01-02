using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ChoseBattlePerson : MonoBehaviour {



    public Text intlvl;
    public Text cou;
    public Text ind;
    public Text ex;
    public Text ri;
    public Text le;
    public Text hello;
    public Text maxPers;
    public Text ChosePers;
    private int[] types;
    private int[] inlvs;
    private int i = 0;
    private int j = 0;
    private int widt;
    private int heig;
    private GameObject ob;

    private PersLoadClass persoist = new PersLoadClass();

    [SerializeField]
    private GameObject[] PersType;


    public void Awake()
    {
        widt = Screen.width;
        heig = Screen.height;
        cou.fontSize = widt / 16;
        intlvl.fontSize = widt / 16;
        hello.fontSize = widt / 17;
        ind.fontSize = widt / 16;
        ri.fontSize = widt / 22;
        le.fontSize = widt / 22;
        maxPers.fontSize = widt / 22;
        ChosePers.fontSize = widt / 22;
        ex.fontSize = widt / 22;
    }
    public IEnumerator Start()
    {
        WWWForm form2 = new WWWForm();
        form2.AddField("name", PlayerPrefs.GetString("Name"));
        form2.AddField("pass", PlayerPrefs.GetString("Pass"));
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
        persoist = JsonUtility.FromJson<PersLoadClass>("{\"persload\":" + www2.text + "}");
        i = persoist.persload.Count;
        cou.text = "Общее количество: " + i.ToString();
        intlvl.text = "Уровень интелекта: " + persoist.persload[0].intlvl.ToString();
        ind.text = "Текущая личность: 1";
        CreatePers(persoist.persload[0].type);
    }

    public void OnExitClick()
    {
        SceneManager.LoadScene(1);
    }

    public void OnChoseClick()
    {
        PlayerPrefs.SetInt("ChosPers",persoist.persload[j].id);
        SceneManager.LoadScene(6);
    }

    public void OnMaxClick()
    {
        int maxPers = -100;
        for(int col=0;col<i-1;col++)
        {
            if (persoist.persload[col].intlvl > maxPers)
            {
                maxPers = persoist.persload[col].intlvl;
                j = col;
            }
        }
        Destroy(ob);
        CreatePers(persoist.persload[j].type);
        ind.text = "Текущая личность: " + (j + 1).ToString();
        intlvl.text = "Уровень интелекта: " + persoist.persload[j].intlvl.ToString();
    }

    public void CreatePers(int id)
    {
        Vector3 pos;
        pos.x = 0;
        pos.y = -2.88f;
        pos.z = 18;
        ob = Instantiate(PersType[id], pos, Quaternion.Euler(0, 180f, 0));
    }

    public void OnRightClick()
    {
        if (j < i - 1)
        {
            j++;
            Destroy(ob);
            CreatePers(persoist.persload[j].type);
            ind.text = "Текущая личность: " + (j + 1).ToString();
            intlvl.text = "Уровень интелекта: " + persoist.persload[j].intlvl.ToString();
        }
    }

    public void OnLeftClick()
    {
        if (j > 0)
        {
            j--;
            Destroy(ob);
            CreatePers(persoist.persload[j].type);
            ind.text = "Текущая личность: " + (j + 1).ToString();
            intlvl.text = "Уровень интелекта: " + persoist.persload[j].intlvl.ToString();
        }
    }
}
