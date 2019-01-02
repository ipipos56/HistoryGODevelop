
using System.Collections.Generic;

[System.Serializable]
public class BlintList
{
    public List<BlintModel> blints = new List<BlintModel>();
}

[System.Serializable]
public class PersonList
{
    public List<PersonModel> persons = new List<PersonModel>();
}

[System.Serializable]
public class PersLoadClass
{
    public List<PersGet> persload = new List<PersGet>();
}

[System.Serializable]
public class posPl
{
    public float x;
    public float y;
}
