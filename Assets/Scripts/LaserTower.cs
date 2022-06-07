using UnityEngine;

public class LaserTower : Tower
{
    [SerializeField, Range(1f, 100f)]
    public float _damagePerSecond = 10f;

    [SerializeField]
    private Transform _turret;

    [SerializeField]
    private Transform _laserBeam;

    private Vector3 _laserBeamScale;
    private Vector3 _laserBeamStartPosition;
    private TargetPoint _target;

    public override TowerType Type => TowerType.Laser;

    private void Awake()
    {
        _laserBeamScale = _laserBeam.localScale;
        _laserBeamStartPosition = _laserBeam.localPosition - new Vector3(0,-2,0);

    }
    public override void GameUpdate()
    {
        if (IsAcquiareTarget(out _target) || IsTargetTracked(ref _target))
        {
            Shoot();
        }
        else
        {
            _laserBeam.localScale = Vector3.zero;
        }
    }
    private void Shoot()
    {
        var point = _target.Position;
        _turret.LookAt(point);
        _laserBeam.localRotation = _turret.localRotation;

        var distance = Vector3.Distance(_turret.position, point);
        _laserBeamScale.z = distance;
        _laserBeam.localScale = _laserBeamScale;
        _laserBeam.localPosition = _laserBeamStartPosition
            + 0.5f * distance * _laserBeam.forward - new Vector3(0,2f,0);

        _target.Enemy.TakeDamage(_damagePerSecond * Time.deltaTime);
    }
}

