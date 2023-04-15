using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herohunk
{
    public class PlayerEnergy : MonoBehaviour
    {
        [SerializeField] EnergyBar energyBar;

        public const int MAX = 100;

        public const int PERCENT = 1;

        int energy;

        private void Start()
        {
            energyBar.Initialize(energy, MAX);
        }

        public void Obtain(int value)
        {
            if (energy == MAX) return;

            // energy += value;
            // energy = Mathf.Clamp(energy, 0, MAX);
            energy = Mathf.Clamp(energy + value, 0, MAX);
            energyBar.UpdateStats(energy, MAX);
        }

        public void Use(int value)
        {
            energy -= value;
        }

        public bool IsEnough(int value)
        {
            return energy >= value;
        }
    }
}