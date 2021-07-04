using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public float fireRate = 0.5F;
	public Transform firePoint;
	public GameObject bulletPrefab;
	private float nextFire = 0F;

	void Update()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			Shoot();
		}
	}

	void Shoot()
	{
		nextFire = Time.time + fireRate;
		Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
	}

}
