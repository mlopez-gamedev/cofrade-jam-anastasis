using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class HolySpiritAvatar : MonoBehaviour
    {
        [SerializeField] HolySpiritUnit _unitPrefab;
        [ShowInInspector, HideInEditorMode] List<HolySpiritUnit> _units;

        private HolySpiritAbility _ability;

        public void Setup(HolySpiritAbility ability)
        {
            _ability = ability;

            _units = new List<HolySpiritUnit>();

            float[] rotations = GetOriginRotations(_ability.UnitsData.Count);
            for (int i = 0; i < _ability.UnitsData.Count; ++i)
            {
                HolySpiritUnit unit = Instantiate(_unitPrefab, transform);
                unit.Setup(ability, _ability.UnitsData[i], rotations[i]);
                _units.Add(unit);
            }

            _ability.UnitsAmount.Subscribe(OnUnitsAmountChange);
        }

        private void OnUnitsAmountChange(int diff)
        {
            float[] rotations = GetOriginRotations(_ability.UnitsData.Count);
            for (int i = 0; i < _ability.UnitsData.Count; ++i)
            {
                if (i < _units.Count)
                {
                    _units[i].ResetPositions(rotations[i]);
                    continue;
                }


                HolySpiritUnit unit = Instantiate(_unitPrefab, transform);
                unit.Setup(_ability, _ability.UnitsData[i], rotations[i]);
                _units.Add(unit);
            }
        }

        internal void Tick()
        {
            transform.rotation = Quaternion.identity;
            foreach (var unit in _units)
            {
                unit.Tick();
            }
        }

        private float[] GetOriginRotations(int amount)
        {
            float[] originRotations = new float[amount];
            switch (amount)
            {
                case 1:
                    originRotations[0] = 0;
                    break;

                case 2:
                    originRotations[0] = 0;
                    originRotations[1] = 180f;
                    break;

                case 3:
                    originRotations[0] = 0;
                    originRotations[1] = 120f;
                    originRotations[2] = 240f;
                    break;

                case 4:
                    originRotations[0] = 0;
                    originRotations[1] = 180f;
                    originRotations[2] = 90f;
                    originRotations[2] = 270f;
                    break;
            }
            return originRotations;
        }
    }
}
