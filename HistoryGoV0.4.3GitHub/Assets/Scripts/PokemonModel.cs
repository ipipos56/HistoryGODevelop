
[System.Serializable]
public class PersonModel
{
    public int id;
    public int type;
    public float latpers;
    public float lonpers;
    public int created;
    public int time;
}

[System.Serializable]
public class BlintModel
{
    public int id;
    public int level;
    public float latblint;
    public float lonblint;
    public int questcount;
    public int time;
    public int catched;
    public int playercatch;
}

[System.Serializable]
public class UserGet
{
    public int mana;
    public int level;
    public int money;
    public int point;
}

[System.Serializable]
public class BlintGet
{
    public int level;
    public int questcount;
    public int playercatch;
    public string catchname;
    public int id;
}

[System.Serializable]
public class PersGet
{
    public int id;
    public int type;
    public int intlvl;
}

[System.Serializable]
public class QuestGet
{
    public string question;
    public int otv;
}
