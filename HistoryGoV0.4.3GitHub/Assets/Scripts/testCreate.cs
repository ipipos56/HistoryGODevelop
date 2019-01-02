using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCreate : MonoBehaviour
{

    public float latblint;
    public float lonblint;
    public int level;
    public int questCount;


    private IEnumerator Start()
    {

        WWWForm form = new WWWForm();
        form.AddField("level", level.ToString());
        form.AddField("latblint", latblint.ToString());
        form.AddField("lonblint", lonblint.ToString());
        form.AddField("quest", questCount.ToString());
        WWW www = new WWW("http://historygo.info/historygounity/regblint.php", form);
        yield return www;
        if (www.error != null)
        {
            Debug.Log("error: " + www.error);
            yield break;
        }

        Debug.Log("Complete");
    }
}

