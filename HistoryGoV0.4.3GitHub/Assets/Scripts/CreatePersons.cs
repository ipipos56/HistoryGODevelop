using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class CreatePersons : MonoBehaviour {

    private float latPl;
    private float lonPl;
    private float llatPl = 0;
    private float llonPl = 0;
    private float latPl2 = 55.741838f; 
    private float lonPl2 = 49.190207f;
    public int mnX;
    public int mnY;
    public GameObject play;
    private float x=0,y=0;

    public GameObject obj;
    [SerializeField]
    private int loadPers = 0;
    [SerializeField]
    private int creatPers = 0;
    [SerializeField]
    private int delPe = 0;

    private PersonList personList = new PersonList();
    private BlintList blintList = new BlintList();
    public GameObject BlintCol;

    [SerializeField]
    private GameObject[] PersType;





    public void Update() {
        //PlayerPrefs.SetFloat("lat",latPl2);
        //PlayerPrefs.SetFloat("lon",lonPl2);
        latPl = PlayerPrefs.GetFloat("lat");
        lonPl = PlayerPrefs.GetFloat("lon");
        if (latPl != 0f && lonPl != 0f)
        {
            if (llatPl != latPl || llonPl != lonPl)
            {
                if (loadPers == 0)
                {
                    loadPers = 1;
                    StartCoroutine(LoadPersons());
                    StartCoroutine(LoadBlints());
                }
                if (creatPers == 1 && delPe == 0)
                {
                    delPe = 1;
                    GameObject[] pe = GameObject.FindGameObjectsWithTag("Persons");
                    foreach (var p in pe)
                    {
                        Destroy(p);
                    }
                    GameObject[] pe2 = GameObject.FindGameObjectsWithTag("colisei");
                    foreach (var p in pe2)
                    {
                        Destroy(p);
                    }
                    loadPers = 0;
                    creatPers = 0;
                    delPe = 0;
                }
            }
        }

    }

    public IEnumerator LoadPersons()
    {
        WWWForm form = new WWWForm();
        form.AddField("latpers", latPl.ToString());
        form.AddField("lonpers", lonPl.ToString());
        WWW www = new WWW("http://historygo.info/historygounity/loadpers.php", form);
        yield return www;
        if (www.error != null)
        {
            Debug.Log("error: " + www.error);
            yield break;
        }
        //Debug.Log(www.text);
        personList = JsonUtility.FromJson<PersonList>("{\"persons\":" + www.text + "}");
        x = play.transform.position.x;
        y = play.transform.position.z;
        foreach (PersonModel pers in personList.persons)
        {
            Vector3 pos;
            pos.x = x + ((pers.lonpers - lonPl) * mnX);
            pos.z = y + ((pers.latpers - latPl) * mnY);
            pos.y = -35f;
            //Debug.Log(i);
            GameObject ob = Instantiate(PersType[pers.type], pos, Quaternion.identity);
            ob.GetComponent<EvSys>().latEn = pers.latpers;
            ob.GetComponent<EvSys>().lonEn = pers.lonpers;
            ob.GetComponent<EvSys>().id = pers.id;
            ob.GetComponent<EvSys>().type = pers.type;
        }
        //llatPl = latPl;
        //llonPl = lonPl;
        //creatPers = 1;
        //Debug.Log(creatPers);
    }

    public IEnumerator LoadBlints()
    {
        WWWForm form = new WWWForm();
        form.AddField("latpers", latPl.ToString());
        form.AddField("lonpers", lonPl.ToString());
        WWW www = new WWW("http://historygo.info/historygounity/loadblints.php", form);
        yield return www;
        if (www.error != null)
        {
            Debug.Log("error: " + www.error);
            yield break;
        }
        Debug.Log(www.text);
        blintList = JsonUtility.FromJson<BlintList>("{\"blints\":" + www.text + "}");
        x = play.transform.position.x;
        y = play.transform.position.z;
        foreach (BlintModel blint in blintList.blints)
        {
            Vector3 pos;
            pos.x = x + ((blint.lonblint - lonPl) * mnX);
            pos.z = y + ((blint.latblint - latPl) * mnY);
            pos.y = -35f;
            //Debug.Log(i);
            GameObject ob = Instantiate(BlintCol, pos, Quaternion.Euler(-90,0,0));
            ob.GetComponent<EvSysColisei>().latEn = blint.latblint;
            ob.GetComponent<EvSysColisei>().lonEn = blint.lonblint;
            ob.GetComponent<EvSysColisei>().id = blint.id;
            ob.GetComponent<EvSysColisei>().level = blint.level;
            ob.GetComponent<EvSysColisei>().countQues = blint.questcount;
        }
        llatPl = latPl;
        llonPl = lonPl;
        creatPers = 1;
       //Debug.Log(creatPers);
    }

}
