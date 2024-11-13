using Rogue;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Combat {
	public class Manager : MonoBehaviour {
        public class WeaponMemory
        {
            public Material Material { get; }
            public Mesh Mesh { get; }
            public UpgradeData UpgradeData { get; }

            public WeaponMemory(GameObject weaponGameObject) {
                
            }
        }


        public Dictionary<string, IWeaponMemory> WeaponMemory = new();

        public void RememberWeapon(SelectEnterEventArgs ctx) {
            if (ctx.interactableObject.transform.gameObject.TryGetComponent(out IWeaponMemory baseWeapon)) {
                WeaponMemory[ctx.interactorObject.transform.name] = baseWeapon;
            } 
        }
	}
}