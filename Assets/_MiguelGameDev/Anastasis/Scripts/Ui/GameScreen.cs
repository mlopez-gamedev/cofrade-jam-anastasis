using UnityEngine;

namespace MiguelGameDev.Anastasis
{
    public class GameScreen : MonoBehaviour
    {
        [SerializeField] IntegerText _levelText;
        [SerializeField] IntegerText _enemiesKilledText;
        [SerializeField] IntegerProgressText _bossesKilledText;
        [SerializeField] Map _minimizedMap;
        [SerializeField] Map _maximizedMap;

        private Map _currentMap;

        public void Setup(Transform player, PlayerGoals playerGoals, IntegerAttribute playerLevel, IntegerAttribute enemiesKilled, IntegerAttribute bossesKilled, IntegerAttribute totalBosses)
        {
            _levelText.Setup(I2.Loc.LocalizationManager.GetTranslation("Game/Level"), playerLevel);
            _enemiesKilledText.Setup(I2.Loc.LocalizationManager.GetTranslation("Game/Kills"), enemiesKilled);
            _bossesKilledText.Setup(I2.Loc.LocalizationManager.GetTranslation("Game/Bosses"), bossesKilled, totalBosses);

            _minimizedMap.Setup(player, playerGoals);
            _maximizedMap.Setup(player, playerGoals);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            Minimize();
        }

        private void Update()
        {
            if (_currentMap == null)
            {
                return;
            }
            CheckInput();
            _currentMap.Tick();
        }

        private void CheckInput()
        {
            if (Input.GetButtonDown("Map"))
            {
                Maximize();
            }
            else if (Input.GetButtonUp("Map"))
            {
                Minimize();
            }
        }

        private void Maximize()
        {
            _minimizedMap.gameObject.SetActive(false);
            _maximizedMap.gameObject.SetActive(true);

            _currentMap = _maximizedMap;
        }

        private void Minimize()
        {
            _maximizedMap.gameObject.SetActive(false);
            _minimizedMap.gameObject.SetActive(true);

            _currentMap = _minimizedMap;
        }
    }
}
