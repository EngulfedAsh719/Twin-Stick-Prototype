using UnityEngine;

public class LevelUpHealth : MonoBehaviour, ILevelUp
{
    public int minLevel => _minLevel;

    public int _minLevel;
    
    private CharacterHealth characterHealth;
    public void LevelUp(CharacterData data, int level)
    {
        if (characterHealth == null)
        {
            characterHealth = GetComponent<CharacterHealth>();
            if (characterHealth == null) return;
        }

        if (data.currentLevel >= minLevel)
        {
            characterHealth.Health += 10;
        }
    }
}
