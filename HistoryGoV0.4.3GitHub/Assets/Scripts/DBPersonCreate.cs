using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DBPersonCreate : MonoBehaviour
{

    public float latPersleft;
    public float lonPersleft;
    public float latPersright;
    public float lonPersright;
    private float setlatpers;
    private float setlonpers;
    private int type;
    private int created;
    private int time;


    private IEnumerator Start()
    {
    setlatpers = latPersleft;
    setlonpers = lonPersleft;
        while (setlonpers < lonPersright)
        {
            while (setlatpers < latPersright)
            {
                setlatpers = ((setlatpers * 100000) + Random.Range(5, 100)) / 100000;
                type = Random.Range(1, 4);
                created = 0;
                time = Random.Range(0, 24);
                WWWForm form = new WWWForm();
                form.AddField("type", type.ToString());
                form.AddField("latpers", setlatpers.ToString());
                form.AddField("lonpers", setlonpers.ToString());
                form.AddField("cre", created.ToString());
                form.AddField("time", time.ToString());
                WWW www = new WWW("http://historygo.info/historygounity/createpers.php", form);
                yield return www;
                if (www.error != null)
                {
                    Debug.Log("error: " + www.error);
                    yield break;
                }      
            }
            setlatpers = latPersleft;
            setlonpers = ((setlonpers * 100000) + Random.Range(10, 100)) / 100000;
        }
        Debug.Log("Complete");
    }
}
