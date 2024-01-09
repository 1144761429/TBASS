using UnityEngine;

public class PlayerHealthAndShieldCheat : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Debug.Log(PlayerStats.Instance.RuntimeData.CurrentHealth);
            PlayerStats.Instance.AddHp(10, out float overflowAmount);
            Debug.Log(PlayerStats.Instance.RuntimeData.CurrentHealth);
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Debug.Log(PlayerStats.Instance.RuntimeData.CurrentHealth);
            PlayerStats.Instance.ReduceHp(10, out float overflowAmount);
            Debug.Log(PlayerStats.Instance.RuntimeData.CurrentHealth);
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            Debug.Log(PlayerStats.Instance.RuntimeData.CurrentArmor);
            PlayerStats.Instance.AddArmor(10, out float overflowAmount);
            Debug.Log(PlayerStats.Instance.RuntimeData.CurrentArmor);
        }

        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            Debug.Log(PlayerStats.Instance.RuntimeData.CurrentArmor);
            PlayerStats.Instance.ReduceArmor(10, out float overflowAmount);
            Debug.Log(PlayerStats.Instance.RuntimeData.CurrentArmor);
        }
    }
}