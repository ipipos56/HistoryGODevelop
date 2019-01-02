using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ControlCamera : MonoBehaviour, IDragHandler {

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private Text deb;
    [SerializeField]
    private Quaternion origin;
    private const float sens = 0.5f;
    private float angX;
    private float angY;

    private float latPl;
    private float lonPl;
    private float latEn;
    private float lonEn;
    private int id;
    private int type;
    private int counQes;

    private bool drag = false;

    //private int heig = 0;
    private int widt = 0;

    public void Awake()
    {
        //heig = Screen.height;
        widt = Screen.width;
        deb.fontSize = widt / 22;
    }

    public void Start()
    {
        origin = cam.transform.rotation;
    }

    public void Update()
    {
        if (Input.touchCount > 0 && !drag)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                string EnName = hit.transform.tag;
                if (EnName == "Persons")
                {
                    latPl = PlayerPrefs.GetFloat("lat");
                    lonPl = PlayerPrefs.GetFloat("lon");
                    lonEn = hit.transform.GetComponent<EvSys>().lonEn;
                    latEn = hit.transform.GetComponent<EvSys>().latEn;
                    id = hit.transform.GetComponent<EvSys>().id;
                    type = hit.transform.GetComponent<EvSys>().type;
                    if (Mathf.Sqrt((((latPl - latEn) * 10000 ) * ((latPl - latEn) * 10000)) + (((lonPl - lonEn) * 10000) * ((lonPl - lonEn) * 10000))) <= 4f)
                    {
                        if (PlayerPrefs.GetInt("mana") > 0)
                        {
                            PlayerPrefs.SetInt("CatchEnId", id);
                            PlayerPrefs.SetInt("CatchEnType", type);
                            SceneManager.LoadScene(2);
                        }
                        else
                        {
                            StartCoroutine(Wrong("У вас закончилась мана"));
                        }
                    }
                    else
                    {
                        StartCoroutine(Wrong("Личность находится далеко, подойдите поближе"));
                    }
                }
                else if (EnName == "colisei")
                {
                    latPl = PlayerPrefs.GetFloat("lat");
                    lonPl = PlayerPrefs.GetFloat("lon");
                    lonEn = hit.transform.GetComponent<EvSysColisei>().lonEn;
                    latEn = hit.transform.GetComponent<EvSysColisei>().latEn;
                    id = hit.transform.GetComponent<EvSysColisei>().id;
                    counQes = hit.transform.GetComponent<EvSysColisei>().countQues;
                    if (Mathf.Sqrt((((latPl - latEn) * 10000) * ((latPl - latEn) * 10000)) + (((lonPl - lonEn) * 10000) * ((lonPl - lonEn) * 10000))) <= 4f)
                    {
                        if (PlayerPrefs.GetInt("mana") > 0)
                        {
                            PlayerPrefs.SetInt("CatchBLId", id);
                            PlayerPrefs.SetInt("CatchBlCount", counQes);
                            SceneManager.LoadScene(7);
                        }
                        else
                        {
                            StartCoroutine(Wrong("У вас закончилась мана"));
                        }
               
                    }
                    else
                    {
                        StartCoroutine(Wrong("Blint находится далеко, подойдите поближе"));
                    }
                }
            }
        }
    }

    public void OnDrag(PointerEventData EventData)
    {
        drag = true;
        angX += EventData.delta.x * sens;
        angY += EventData.delta.y * sens;
        angY = Mathf.Clamp(angY, -2, 50);
        Quaternion rotX = Quaternion.AngleAxis(angX, Vector3.forward);
        Quaternion rotY = Quaternion.AngleAxis(angY, Vector3.left);
        cam.transform.rotation = origin * rotX * rotY;
        drag = false;
    }

    private IEnumerator Wrong(string a)
    {
        deb.text = a;
        yield return new WaitForSeconds(5f);
        deb.text = "";
    }
}
