using UnityEngine;

public class SFXManager : MonoBehaviour
{
	public static SFXManager _instance;
	[SerializeField] private AudioSource _phoneMusic;
	[Header("shootAudio")]
	[SerializeField] public AudioSource _shootMortar;
	[SerializeField] public AudioSource _shootLaser;
	[SerializeField] public AudioSource _shootElectroMortar;
	[SerializeField] public AudioSource _shootTurret;
	[SerializeField] public AudioSource _shootTurretTypeTwo;

	[Header("ZombieAudio")]
	[SerializeField] private AudioSource _smallZombie;
	[SerializeField] private AudioSource _mediumZombie;
	[SerializeField] private AudioSource _largeZombie;

	


    private void Awake()
    {
		_instance = this;
    }
}
