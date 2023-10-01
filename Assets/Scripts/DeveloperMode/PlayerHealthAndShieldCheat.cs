using UnityEngine;

public class PlayerHealthAndShieldCheat : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            PlayerStats.Instance.AddHp(10);
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            PlayerStats.Instance.ReduceHp(10);
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            PlayerStats.Instance.AddCurrentShield(10);
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            PlayerStats.Instance.ReduceCurrentShield(10);
        }
    }
}