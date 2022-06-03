using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTower : Tower
{
	public override TowerType Type => TowerType.Turret;

	[SerializeField] private Shell bulletPrefab; // префаб снаряда, если его нет, то будет выбран режим стрельбы "лучом"
	[SerializeField] private float fireRate = 1; // скорострельность
	[SerializeField] private float smooth = 1; // сглаживание движения башни
	[SerializeField] private float rayOffset = 1; // делаем поисковый луч, немного больше области поиска
	[SerializeField] private float damage = 10; // повреждение (при стрельбе "лучом")
	[SerializeField] private Transform[] bulletPoint; // точки, откуда ведется стрельба
	[SerializeField] private Transform turretRotation; // объект вращения, башня турели
	[SerializeField] private Transform center; // центр между пушками, для поискового луча
	[SerializeField] private LayerMask layerMask; // фильтр коллайдеров по маске слоя
	[Header("Лимиты по осям башни:")]
	[SerializeField] private bool useLimits;
	[SerializeField][Range(0, 180)] private float limitY = 50;
	[SerializeField][Range(0, 180)] private float limitX = 30;
	private SphereCollider turretTrigger;
	private Transform target;
	private Vector3 offset;
	private int index;
	private float curFireRate;
	private Quaternion defaultRot = Quaternion.identity;

	void Awake()
	{
		turretTrigger = GetComponent<SphereCollider>();
		turretTrigger.isTrigger = true;
		offset = turretTrigger.center;
		curFireRate = fireRate;
		turretTrigger.enabled = true;
		enabled = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if (CheckLayerMask(other.gameObject, layerMask))
		{
			target = other.transform;
			turretTrigger.enabled = false;
			enabled = true;
		}
	}
	Transform FindTarget() // возвращает ближайшую цель
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position + offset, turretTrigger.radius, layerMask);

		Collider currentCollider = null;
		float dist = Mathf.Infinity;

		foreach (Collider coll in colliders)
		{
			float currentDist = Vector3.Distance(transform.position + offset, coll.transform.position);

			if (currentDist < dist)
			{
				currentCollider = coll;
				dist = currentDist;
			}
		}

		return (currentCollider != null) ? currentCollider.transform : null;
	}

	Vector3 CalculateNegativeValues(Vector3 eulerAngles)
	{
		eulerAngles.y = (eulerAngles.y > 180) ? eulerAngles.y - 360 : eulerAngles.y;
		eulerAngles.x = (eulerAngles.x > 180) ? eulerAngles.x - 360 : eulerAngles.x;
		eulerAngles.z = (eulerAngles.z > 180) ? eulerAngles.z - 360 : eulerAngles.z;
		return eulerAngles;
	}

	bool Search() // разворот башни на цель
	{
		if (rayOffset < 0) rayOffset = 0;
		float dist = Vector3.Distance(transform.position + offset, target.position);
		Vector3 lookPos = target.position - turretRotation.position;
		Debug.DrawRay(turretRotation.position, center.forward * (turretTrigger.radius + rayOffset));
		Vector3 rotation = Quaternion.Lerp(turretRotation.rotation, Quaternion.LookRotation(lookPos), smooth * Time.deltaTime).eulerAngles;

		if (useLimits)
		{
			rotation = CalculateNegativeValues(rotation);
			rotation.y = Mathf.Clamp(rotation.y, -limitY, limitY);
			rotation.x = Mathf.Clamp(rotation.x, -limitX, limitX);
		}

		rotation.z = 0;
		turretRotation.eulerAngles = rotation;

		if (dist > turretTrigger.radius + rayOffset)
		{
			target = null;
			return false;
		}

		if (IsRaycastHit(center)) return true;

		return false;
	}

	bool CheckLayerMask(GameObject obj, LayerMask layers)
	{	
		if (((1 << obj.layer) & layers) != 0)
		{
			return true;
		}

		return false;
	}

	bool IsRaycastHit(Transform point)
	{
		RaycastHit hit;
		Ray ray = new Ray(point.position, point.forward);
		if (Physics.Raycast(ray, out hit, turretTrigger.radius + rayOffset))
		{
			if (CheckLayerMask(hit.transform.gameObject, layerMask))
			{
				return true;
			}
		}

		return false;
	}

	void Shot()
	{
		if (!Search()) return;

		curFireRate += Time.deltaTime;
		if (curFireRate > fireRate)
		{
			Transform point = GetPoint();
			curFireRate = 0;

			if (bulletPrefab != null)
			{
				Shell bullet = Instantiate(bulletPrefab, point.position, Quaternion.identity) as Shell;
				//bullet.SetBullet(layerMask, point.forward);
			}
			else if (IsRaycastHit(point))
			{
				//target.GetComponent<UnitHP>().Adjust(-damage);
			}
		}
	}

	void Choice()
	{
		curFireRate = fireRate;

		target = FindTarget();

		turretRotation.rotation = Quaternion.Lerp(turretRotation.rotation, defaultRot, smooth * Time.deltaTime);

		if (Quaternion.Angle(turretRotation.rotation, defaultRot) == 0)
		{
			turretRotation.rotation = defaultRot;
			turretTrigger.enabled = true;
			enabled = false;
		}
	}

	Transform GetPoint()
	{
		if (index == bulletPoint.Length - 1) index = 0; else index++;
		return bulletPoint[index];
	}

	void LateUpdate()
	{
		if (target != null) Shot(); else Choice();
	}
}