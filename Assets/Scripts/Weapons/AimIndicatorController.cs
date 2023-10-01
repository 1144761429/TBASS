using UnityEngine;
using UnityEngine.UI;
using WeaponSystem;

public class AimIndicatorController : MonoBehaviour
{
    private Loadout _loadout;
    private Weapon _currentWeapon;
    [SerializeField] private Transform _aimLaserTop;
    [SerializeField] private Transform _aimLaserBottom;
    [SerializeField] private Image _aimAreaFill;

    private float _topBoundAimAngle;
    private float _bottomBoundAimAngle;

    private bool _aimIndicatorInitialized;

    private void Awake()
    {
        _loadout = GetComponentInParent<Loadout>();
    }

    public void UpdateAimAngle(float angle)
    {
        _topBoundAimAngle = angle / 2;
        _bottomBoundAimAngle = angle / 2;
        UpdateAimVisual();
    }

    private void UpdateAimVisual()
    {
        _aimLaserTop.localEulerAngles = new Vector3(0, 0, _topBoundAimAngle);
        _aimLaserBottom.localEulerAngles = new Vector3(0, 0, -_bottomBoundAimAngle);
        _aimAreaFill.transform.localEulerAngles = new Vector3(0, 0, -_bottomBoundAimAngle);
        _aimAreaFill.fillAmount = (_topBoundAimAngle + _bottomBoundAimAngle) / 360;
    }

    private void UpdateCurrentWeapon()
    {
        _currentWeapon = _loadout.CurrentWeapon;
    }
}
