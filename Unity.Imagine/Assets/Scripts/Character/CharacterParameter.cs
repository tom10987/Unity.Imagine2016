
[System.Serializable]
public struct CharacterParameter
{
    public enum ModelType
    {
        BEAST,
        HUMAN,
        ROBO,
        NONE,
    }

    public enum CostumeType
    {
        A,
        B,
        C,
        NONE,
    }

    public enum DecorationType
    {
        NONE,
        A,
        B,
        C,
    }

    public ModelType modelType;
    public CostumeType costumeType;
    public DecorationType decorationType;
    public int attack;
    public int defense;
    public int speed;
}
