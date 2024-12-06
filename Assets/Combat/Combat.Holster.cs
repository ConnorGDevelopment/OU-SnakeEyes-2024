using UnityEngine;
using UnityEngine.Events;

namespace Combat
{
    public class Holster : MonoBehaviour
    {
        public GameObject Controller;

        public GameObject WeaponImprint;

        public UnityEvent WeaponDestroyed = new();

        public void ImprintWeapon(Weapon weapon)
        {
            GameObject weaponImprint = Instantiate(weapon.gameObject);
            weaponImprint.SetActive(false);
            weaponImprint.transform.SetParent(transform, false);
            weaponImprint.GetComponent<Weapon>().ReloadAmmo();
        }

        public void SpawnWeapon()
        {
            Instantiate(WeaponImprint);
        }

        public void Start()
        {
            WeaponDestroyed.AddListener(SpawnWeapon);
        }
    }
}