using UnityEngine;

[CreateAssetMenu(fileName = "PlayerValues", menuName = "Scriptable Objects/PlayerValues")]
public class PlayerValues : ScriptableObject
{
    public float xp;

    public float xpTillNextLvL;

    public int lvl;

    public float hp;

    public float maxhp;

}
