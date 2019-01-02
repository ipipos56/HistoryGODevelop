using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class UpdateGPSText : MonoBehaviour {

    public Text coordinates;
    public Text debug;
    public Text man;
    public Text lvl;
    public Text mon;
    public Text load;
    public Image loadi;
    public Image mana;

    public GameObject obj;
    [SerializeField]
    private float lat;
    [SerializeField]
    private float lon;

    [SerializeField]
    private float llat;
    [SerializeField]
    private float llon;

    //[SerializeField]
   // private Texture2D texture;
    //private GameObject tile;
    //[SerializeField]
    //private Renderer maprender;
    //private string url;
    private int loadPos = 0;
    [SerializeField]
    private int gpstest = 0;
    [SerializeField]
    private int r = 0;
    private string nickname;
    //private int heig = 0;
    private int widt = 0;
    public GameObject play;
    private float x=0,y=0;

    public int mnX;
    public int mnY;

    public void Awake()
    {
        //heig = Screen.height;
        widt = Screen.width;
        //maprender.enabled = true;
        man.fontSize = widt / 19;
        coordinates.fontSize = widt / 22;
        lvl.fontSize = widt / 22;
        mon.fontSize = widt / 22;
        debug.fontSize = widt / 15;
        load.fontSize = widt / 15;
        nickname = PlayerPrefs.GetString("Name");
        StartCoroutine(GetId());
        StartCoroutine(Check());
        StartCoroutine(loading());
    }

    private IEnumerator loading() {
        yield return new WaitForSeconds(0.5f);
        load.text = "Loading..";
        yield return new WaitForSeconds(0.5f);
        load.text = "Loading.";
        yield return new WaitForSeconds(0.5f);
        load.text = "Loading";
        yield return new WaitForSeconds(0.5f);
        load.text = "Loading...";
        yield return new WaitForSeconds(0.5f);
        load.text = "Loading..";
        yield return new WaitForSeconds(0.5f);
        load.text = "Loading.";
        yield return new WaitForSeconds(0.5f);
        load.text = "Loading";
        yield return new WaitForSeconds(0.5f);
        load.enabled = false;
        loadi.enabled = false;
    }
    private IEnumerator Check()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("User has not enabled GPS");
            debug.color = Color.red;
            debug.text = "Включите пожалуйста GPS и перезапустите приложение";
            yield break;
        }

        Input.location.Start();
        int maxWait = 30;

        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 0)
        {
            print("Timed out");
            debug.text = "Проверьте скорость интернета";
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            debug.color = Color.red;
            debug.text = "Проверьте интернет и GPS, невозможно распознать местоположение";
            yield break;
        }
        else
        {
            PlayerPrefs.SetFloat("lat", Input.location.lastData.latitude);
            PlayerPrefs.SetFloat("lon", Input.location.lastData.longitude);
            lat = PlayerPrefs.GetFloat("lat");
            lon = PlayerPrefs.GetFloat("lon");
            llat = lat;
            llon = lon;
        }
        gpstest = 1;
        //Input.location.Stop();
    }

    private void Update()
    {
        if (gpstest == 1)
        {
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                print("Unable to determine device location");
                debug.color = Color.red;
                debug.text = "Проверьте интернет и GPS, невозможно распознать местоположение";
            }
            else
            {
                PlayerPrefs.SetFloat("lat", Input.location.lastData.latitude);
                PlayerPrefs.SetFloat("lon", Input.location.lastData.longitude);
                lat = PlayerPrefs.GetFloat("lat");
                lon = PlayerPrefs.GetFloat("lon");
            }
            // Debug.Log(nickname + "LAT: " + lat.ToString() + " " + "LON: " + lon.ToString());
            //coordinates.text = nickname + "  LAT: " + lat.ToString() + " " + "LON: " + lon.ToString() + "     " + r;
            if (llon != lon || llat != lat)
            {
                if (loadPos == 0)
                {
                    loadPos = 1;
                    //obj.GetComponent<CreatePersons>().start = 0;
                    StartCoroutine(TransformPos());
                }
            }
        }
        //Thread.Sleep(100);
    }

    private IEnumerator TransformPos()
    {
        float newlat = lat;
        float newlon = lon;

        Debug.Log((newlon - llon));

        Vector3 pos;
        pos.x = x + ((newlon - llon) * mnX);
        pos.z = y + ((newlat - llat) * mnY);
        pos.y = 0f;

        play.transform.position = pos;

        x = play.transform.position.x;
        y = play.transform.position.z;

        llat = newlat;
        llon = newlon;
        loadPos = 0;
        yield break;
    }

    private IEnumerator GetId()
    {
        WWWForm form2 = new WWWForm();
        form2.AddField("name", PlayerPrefs.GetString("Name"));
        form2.AddField("pass", PlayerPrefs.GetString("Pass"));
        WWW www2 = new WWW("http://historygo.info/historygounity/getid.php", form2);
        //Debug.Log(www);
        yield return www2;
        if (www2.error != null)
        {
            Debug.Log("error: " + www2.error);
            //deb.text = www2.error;
            yield break;
        }
        //deb.text = www2.text;
        UserGet user = JsonUtility.FromJson<UserGet>(www2.text);
        PlayerPrefs.SetInt("mana", user.mana);
        PlayerPrefs.SetInt("lvl", user.level);
        PlayerPrefs.SetInt("money", user.money);
        PlayerPrefs.SetInt("point", user.point);
        man.text = PlayerPrefs.GetInt("mana").ToString();
        lvl.text = "Уровень: " + PlayerPrefs.GetInt("lvl").ToString();
        mon.text = "Монеты: " + PlayerPrefs.GetInt("money").ToString();
        if(user.level * 100 < user.point)
        StartCoroutine(LevelHigh());
        yield break;
    }

    private IEnumerator LevelHigh()
    {
        WWWForm form2 = new WWWForm();
        form2.AddField("name", PlayerPrefs.GetString("Name"));
        form2.AddField("pass", PlayerPrefs.GetString("Pass"));
        WWW www2 = new WWW("http://historygo.info/historygounity/levelhigh.php", form2);
        //Debug.Log(www);
        yield return www2;
        if (www2.error != null)
        {
            Debug.Log("error: " + www2.error);
            //deb.text = www2.error;
            yield break;
        }
        lvl.text = "Уровень: " + (PlayerPrefs.GetInt("lvl") + 1).ToString();
    }

    public void OnClickPersMenu()
    {
        SceneManager.LoadScene(3);
    }
}
