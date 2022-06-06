using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using propertiesTower;
public class MortarTowerShop : MonoBehaviour
{
  
    [SerializeField] private MortarTower _mortar;
    [Header("UIMortar")]
    [SerializeField] private TextMeshProUGUI _rangeBlast;
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _rangeText;
    [SerializeField] private TextMeshProUGUI _speedText;

    [Header("UpgradeUI")]
    [SerializeField] private TextMeshProUGUI _blastTextUpgrade;
    [SerializeField] private TextMeshProUGUI _damageTextUpgrade;
    [SerializeField] private TextMeshProUGUI _rangeTextUpgrade;
    [SerializeField] private TextMeshProUGUI _speedTextUpgrade;

    [SerializeField, Range(0.05f, 0.25f)] private float _upgradeBlast = 0.1f;
    [SerializeField, Range (1f, 10f)] private float _upgradeDamage = 5;
    [SerializeField, Range(0.05f, 0.25f)] private float _upgradeRange = 0.1f;
    [SerializeField, Range(0.01f, 0.05f)] private float _upgradeSpeed = 0.02f;
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        CalculateTower();
        if (PlayerPrefs.GetFloat(PrefsForMortarTower.MORTAR_DAMAGE) > 40)
        {
            _damageText.text = PlayerPrefs.GetFloat(PrefsForMortarTower.MORTAR_DAMAGE).ToString("F2");
            _rangeBlast.text = PlayerPrefs.GetFloat(PrefsForMortarTower.MORTAR_RANGE).ToString("F2");
            _speedText.text = PlayerPrefs.GetFloat(PrefsForMortarTower.MORTAR_SPEED).ToString("F2");
            _rangeText.text = PlayerPrefs.GetFloat(PrefsForMortarTower.MORTAR_RANGE).ToString("F2");
        }
        else
        {
            _damageText.text = _mortar._damage.ToString("F2");
            _rangeBlast.text = _mortar._shellBlastRadius.ToString("F2");
            _speedText.text = _mortar._shootsPerSeconds.ToString("F2");
            _rangeText.text = _mortar._targetingRange.ToString("F2");
        }
        

        _blastTextUpgrade.text = "+ " + _upgradeBlast.ToString("F2");
        _damageTextUpgrade.text = "+ " + _upgradeDamage.ToString("F2");
        _rangeTextUpgrade.text = "+ " + _upgradeRange.ToString("F2");
        _speedTextUpgrade.text = "+ " + _upgradeSpeed.ToString("F2");
    }
    private void CalculateTower()
    {
        if (_mortar._damage < PlayerPrefs.GetFloat(PrefsForMortarTower.MORTAR_DAMAGE))
        {
            _mortar._shootsPerSeconds = PlayerPrefs.GetFloat(PrefsForMortarTower.MORTAR_SPEED);
            _mortar._shellBlastRadius = PlayerPrefs.GetFloat(PrefsForMortarTower.MORTAR_BLAST);
            _mortar._damage = PlayerPrefs.GetFloat(PrefsForMortarTower.MORTAR_DAMAGE);
            _mortar._targetingRange = PlayerPrefs.GetFloat(PrefsForMortarTower.MORTAR_RANGE);
        }
    }
    public void Upgrade()
    {
       
        _mortar._damage += _upgradeDamage;
        PlayerPrefs.SetFloat(PrefsForMortarTower.MORTAR_DAMAGE, _mortar._damage);

        _mortar._shellBlastRadius += _upgradeRange;
        PlayerPrefs.SetFloat(PrefsForMortarTower.MORTAR_BLAST, _mortar._shellBlastRadius);

        _mortar._shootsPerSeconds += _upgradeSpeed;
        PlayerPrefs.SetFloat(PrefsForMortarTower.MORTAR_SPEED, _mortar._shootsPerSeconds);

        _mortar._targetingRange += _upgradeRange;
        PlayerPrefs.SetFloat(PrefsForMortarTower.MORTAR_RANGE, _mortar._targetingRange);
        Init();
    }

}
