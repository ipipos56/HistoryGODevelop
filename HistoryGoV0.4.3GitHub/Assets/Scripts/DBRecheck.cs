using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DBRecheck : MonoBehaviour
{

    public int id;


    private IEnumerator Start()
    {
        id = 1;
        while (id < 148735)
        {
            WWWForm form = new WWWForm();
            form.AddField("pass", "recheckUpdate");
            form.AddField("id", id.ToString());
            WWW www = new WWW("http://historygo.info/historygounity/recheck.php", form);
            yield return www;
            if (www.error != null)
            {
                Debug.Log("error: " + www.error);
            }
            else
            {
                Debug.Log(www.text);
                id++;
            }
        }
        Debug.Log("Complete");
    }
}
