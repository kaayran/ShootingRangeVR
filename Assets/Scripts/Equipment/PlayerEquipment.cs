using System;
using System.Collections.Generic;
using System.Linq;
using Equipment.Interfaces;
using UnityEngine;

namespace Equipment
{
    public class PlayerEquipment : MonoBehaviour
    {
        private List<EquipmentVisualizer<IEquippable>> _equipmentVisualizers;
        //private List<EquipmentSlot> _equipmentSlots;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _equipmentVisualizers = GetComponents<EquipmentVisualizer<IEquippable>>().ToList();
            //_equipmentSlots = GetComponents<EquipmentSlot>().ToList();

            foreach (var visualizer in _equipmentVisualizers)
            {
                visualizer.Init();
            }
        }
    }
}