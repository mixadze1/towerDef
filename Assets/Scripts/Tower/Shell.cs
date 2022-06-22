using UnityEngine;

public   class Shell : WarEntity
{
    private Vector3 _launchPoint, _targetPoint, _launchVelocity;
    private float _age;
    private float _blastRadius, _damage;
    private TowerType _type;
    public void Initialize(Vector3 launchPoint, Vector3 targetPoint, Vector3 launchVelocity, float blastRaius, float damage, TowerType type)
    {
        _launchPoint = launchPoint; 
        _targetPoint = targetPoint;
        _launchVelocity = launchVelocity;
        _blastRadius = blastRaius;
        _damage = damage;
        _type = type;
    }
    public override bool GameUpdate()
    {
        if (_type == TowerType.Mortar)
        {
            _age += Time.deltaTime;
            Vector3 p = _launchPoint + _launchVelocity * _age;
            p.y -= 0.5f * 9.81f * _age * _age;
            if (p.y <= 0f)
            {
                //Debug.Log("zdec");
                //SFXManager._instance._shootMortar.Play();
                Game.SpawnExplosion().Initialize(_targetPoint, _blastRadius, _type, _damage);
                OriginFactory.Reclaim(this);
                return false;
            }
            transform.localPosition = p;

            Vector3 d = _launchVelocity;
            d.y -= 9.81f * _age;
            transform.localRotation = Quaternion.LookRotation(d);

            Game.SpawnExplosion().Initialize(p, 0.05f, _type, _damage);

            return true;
        }
        else
        {
            transform.localScale =new Vector3( 0.1f, 0.1f, 0.1f);
            _age += Time.deltaTime;
            Vector3 p = _launchPoint + _launchVelocity * _age;
            //p.y -= 0.5f * 9.81f * _age * _age;
            if (p.y <= 0f)
            {
                Game.SpawnExplosion().Initialize(_targetPoint, _blastRadius, _type, _damage);
                OriginFactory.Reclaim(this);
                return false;
            }
            transform.localPosition = p;

            Vector3 d = _launchVelocity;
            d.y -= 9.81f * _age;

            transform.localRotation = Quaternion.LookRotation(d);

            Game.SpawnExplosion().Initialize(p, 0.05f, _type, _damage);

            return true;
        }
        
    }
}
