using System.Collections.Generic;

public class ElementModel
{
    public int ID { get; }
    public string Sprite { get; }

    public ElementModel(int iD, string sprite)
    {
        ID = iD;
        Sprite = sprite;
    }
}