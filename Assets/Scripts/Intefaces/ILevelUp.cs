public interface ILevelUp
{
    void LevelUp(CharacterData data, int level);
    public int minLevel { get; }
}