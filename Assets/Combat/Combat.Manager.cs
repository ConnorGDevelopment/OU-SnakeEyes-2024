using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace Combat
{
    public class Manager : MonoBehaviour
    {
        private List<GameObject> _weaponPrefabs;

        public Dictionary<string, GameObject> WeaponMemory = new();
        public Dictionary<string, GameObject> Holsters = new();
        public UnityEvent<string> OnWeaponDestroyed;

        public void Start()
        {
            _weaponPrefabs = Resources.LoadAll<GameObject>("WeaponPrefabs").ToList();
            OnWeaponDestroyed.AddListener(SpawnWeapon);
        }

        public GameObject MatchWeaponPrefab(GameObject weaponObject)
        {
            var match = _weaponPrefabs.Where(weaponPrefab =>
                        weaponPrefab.GetComponent<MeshRenderer>().material == weaponObject.GetComponent<MeshRenderer>().material &&
                        weaponPrefab.GetComponent<MeshFilter>().mesh == weaponObject.GetComponent<MeshFilter>().mesh &&
                        weaponPrefab.GetComponent<Weapon>().BaseStats.Name == weaponObject.GetComponent<Weapon>().BaseStats.Name);

            if (match.Any())
            {
                return match.First();
            }
            else
            {
                Debug.Log("No Matching Prefab Found, Manually Inserting into temp Prefab Collection", weaponObject);
                _weaponPrefabs.Add(weaponObject);
                return weaponObject;
            }
        }

        public void RememberWeapon(SelectEnterEventArgs ctx)
        {

            WeaponMemory[ctx.interactorObject.transform.name] = MatchWeaponPrefab(ctx.interactableObject.transform.gameObject);
            Debug.Log($"{ctx.interactableObject.transform.name} remembered for {ctx.interactorObject}");
        }

        public void SpawnWeapon(string interactorKey)
        {
            GameObject weaponPrefab = MatchWeaponPrefab(WeaponMemory[interactorKey]);
            GameObject holster = Holsters[interactorKey];

            GameObject newWeapon = Instantiate(weaponPrefab, holster.transform.position, holster.transform.rotation);
            if (newWeapon.TryGetComponent(out Rigidbody rb))
            {
                rb.useGravity = false;
                Debug.Log($"{gameObject.name} set Gravity to false for {newWeapon.name}", newWeapon);
            }
        }

        public static Manager FindLive(GameObject gameObject)
        {
            if (GameObject.FindWithTag("LiveCombatManager").TryGetComponent(out Manager combatManager))
            {
                return combatManager;
            }
            else
            {
                Debug.Log("Could not find Combat Manager", gameObject);
                return null;
            }
        }

    }
}