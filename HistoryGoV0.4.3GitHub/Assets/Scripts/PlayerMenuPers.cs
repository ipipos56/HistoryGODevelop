using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMenuPers : MonoBehaviour {


    private int i = 0;
    private int widt;
    private GameObject ob;
    private Text ob2;
    [SerializeField]
    private GameObject cont;
    private int dist = -23;
    private int dist2 = -390;
    [SerializeField]
    private Transform parent;
    [SerializeField]
    private Text te;
    [SerializeField]
    private Canvas can;


    private PersLoadClass persoist = new PersLoadClass();

    [SerializeField]
    private GameObject[] PersType;


    public void Awake()
    {
        widt = Screen.width;
    }
    public IEnumerator Start()
    {
        WWWForm form2 = new WWWForm();
        form2.AddField("name", "midar1000");//PlayerPrefs.GetString("Name"));
        form2.AddField("pass", "3107776a");//PlayerPrefs.GetString("Pass"));
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
        RectTransform rt = cont.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(1080, 260 * i);
        rt.localPosition = new Vector3(0, -18 * i, 0);
        for (int j = 0; j <= i / 2; j++)
        {
            CreatePers(persoist.persload[j].type, j, -1,j);
        }
        for (int j = i / 2 + 1; j < i; j++)
        {
            CreatePers(persoist.persload[j].type, j-i/2 - 1, 1,j);
        }
    }

    public void CreatePers(int id, int con,int set, int j)
    {
        Vector3 pos;
        pos.x = 15 * set;
        pos.y = 35 + dist * con;
        pos.z = 75;

        Vector3 pos2;
        if(set == -1)
            pos2.x = -150;
        else
            pos2.x = 350;
        pos2.y = (536 + 36 * i/2) + dist2 * con;
        pos2.z = -200;

        ob = Instantiate(PersType[id], pos, Quaternion.Euler(0, 180f, 0));
        ob.transform.SetParent(parent);
        ob.GetComponent<EvSys>().id = persoist.persload[j].id;
        ob.GetComponent<EvSys>().type = persoist.persload[j].type;
        ob2 = Instantiate(te, pos2, Quaternion.Euler(0f, 0f, 0f)) as Text;
        ob2.text = persoist.persload[j].intlvl.ToString();
        ob2.transform.SetParent(cont.transform,false);
    }
}
