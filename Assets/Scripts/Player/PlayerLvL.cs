using UnityEngine;

public class PlayerLvL : MonoBehaviour
{
    [SerializeField] private PlayerValues _playerValues;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        while(_playerValues.xp >= _playerValues.xpTillNextLvL)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        _playerValues.xpTillNextLvL *= 1.1f;
    }
}
