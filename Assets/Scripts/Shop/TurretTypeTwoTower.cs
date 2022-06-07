using UnityEngine;
public class TurretTypeTwoTower : Tower
{
    [SerializeField, Range(0.5f, 5f)] public float _shootsPerSeconds = 1f;
    [SerializeField] protected Transform _mortar;
    [SerializeField, Range(0.5f, 3f)] public float _shellBlastRadius = 1f;
    [SerializeField, Range(0f, 50f)] public float _damage;
    public override TowerType Type => TowerType.TurretTypeTwo;

    private float _launchSpeed;
    private float _launchProgress;


    private void Awake()
    {
        OnValidate();
    }

    private void OnValidate()
    {
        float x = _targetingRange + 1.81f;
        float y = -_mortar.position.y;
        _launchSpeed = Mathf.Sqrt(9.81f * (y + Mathf.Sqrt(x * x + y * y)));
    }

    public override void GameUpdate()
    {
        _launchProgress += Time.deltaTime * _shootsPerSeconds;
        while (_launchProgress >= 1f)
        {
            if (IsAcquiareTarget(out TargetPoint target))
            {
                Launch(target);
                _launchProgress -= 1f;
            }
            else
            {
                _launchProgress = 0.999f;
            }
        }
    }
    private void Launch(TargetPoint target)
    {
        Vector3 launchPoint = _mortar.position;
        Vector3 targetPoint = target.Position;
        targetPoint.y = 0f;

        Vector3 dir;
        dir.x = targetPoint.x - launchPoint.x;
        dir.y = targetPoint.y - launchPoint.y;
        dir.z = targetPoint.z - launchPoint.z;

        float x = dir.magnitude;
        float y = -launchPoint.y;
        //dir /= x;

        float g = 9.81f;
        float s = _launchSpeed;
        float s2 = s * s;
        _mortar.localRotation = Quaternion.LookRotation(dir) * Quaternion.Euler(10f, 0f, 0f);

        Game.SpawnShell().Initialize(launchPoint, targetPoint,
            new Vector3(s * dir.x, -x * 2, s * dir.z), _shellBlastRadius, _damage, Type);
    }
}