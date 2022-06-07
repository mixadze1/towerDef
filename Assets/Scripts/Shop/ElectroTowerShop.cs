using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using propertiesTower;
using TMPro;

public class ElectroTowerShop : MonoBehaviour
{

    [SerializeField] private ElectoRocketTower _electro;
    [SerializeField] private Shop _shop;
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
    [SerializeField, Range(1f, 10f)] private float _upgradeDamage = 5;
    [SerializeField, Range(0.05f, 0.25f)] private float _upgradeRange = 0.1f;
    [SerializeField, Range(0f, 0.05f)] private float _upgradeSpeed = 0.02f;
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        CalculateTower();
        CalculateText();
    }

    private void CalculateText()
    {
        if (PlayerPrefs.GetFloat(PrefsMortar.DAMAGE) > 40)
        {
            _damageText.text = PlayerPrefs.GetFloat(PrefsElectro.DAMAGE).ToString("F2");
            _rangeBlast.text = PlayerPrefs.GetFloat(PrefsElectro.RANGE).ToString("F2");
            _speedText.text = PlayerPrefs.GetFloat(PrefsElectro.SPEED).ToString("F2");
            _rangeText.text = PlayerPrefs.GetFloat(PrefsElectro.RANGE).ToString("F2");
        }
        else
        {
            _damageText.text = _electro._damage.ToString("F2");
            _rangeBlast.text = _electro._shellBlastRadius.ToString("F2");
            _speedText.text = _electro._shootsPerSeconds.ToString("F2");
            _rangeText.text = _electro._targetingRange.ToString("F2");
        }


        _blastTextUpgrade.text = "+ " + _upgradeBlast.ToString("F2");
        _damageTextUpgrade.text = "+ " + _upgradeDamage.ToString("F2");
        _rangeTextUpgrade.text = "+ " + _upgradeRange.ToString("F2");
        _speedTextUpgrade.text = "+ " + _upgradeSpeed.ToString("F2");
    }

    private void CalculateTower()
    {
        if (_electro._damage < PlayerPrefs.GetFloat(PrefsMortar.DAMAGE))
        {
            _electro._shootsPerSeconds = PlayerPrefs.GetFloat(PrefsElectro.SPEED);
            _electro._shellBlastRadius = PlayerPrefs.GetFloat(PrefsElectro.BLAST);
            _electro._damage = PlayerPrefs.GetFloat(PrefsElectro.DAMAGE);
            _electro._targetingRange = PlayerPrefs.GetFloat(PrefsElectro.RANGE);
        }
    }
    public void Upgrade()
    {
        if (GUIManager.instance.Dollar > _shop.PriceUpgradeMortar)
        {
            GUIManager.instance.Dollar -= _shop.PriceUpgradeMortar;
            PlayerPrefs.SetInt(Dollar.DECIMAL, GUIManager.instance.Dollar);

            _electro._damage += _upgradeDamage;
            PlayerPrefs.SetFloat(PrefsElectro.DAMAGE, _electro._damage);

            _electro._shellBlastRadius += _upgradeRange;
            PlayerPrefs.SetFloat(PrefsElectro.BLAST, _electro._shellBlastRadius);

            _electro._shootsPerSeconds += _upgradeSpeed;
            PlayerPrefs.SetFloat(PrefsElectro.SPEED, _electro._shootsPerSeconds);

            _electro._targetingRange += _upgradeRange;
            PlayerPrefs.SetFloat(PrefsElectro.RANGE, _electro._targetingRange);
            Init();
        }
    }

}
