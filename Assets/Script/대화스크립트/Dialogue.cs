using UnityEngine;

[System.Serializable]

public class Sentence
{
    public string characterName;
    public string text;
    public Sprite image;
}

[System.Serializable]
public class Dialogue
{
    public string characterName;
    public Sentence[] sentences;
}
