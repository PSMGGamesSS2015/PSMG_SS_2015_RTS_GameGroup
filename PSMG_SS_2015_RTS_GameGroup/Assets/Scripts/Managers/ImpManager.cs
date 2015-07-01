using System.Collections.Generic;
using Assets.Scripts.Config;
using Assets.Scripts.Controllers.Characters.Imps;
using Assets.Scripts.Controllers.Characters.Imps.SubServices;
using Assets.Scripts.ParameterObjects;
using Assets.Scripts.Types;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    /// <summary>
    /// The ImpManager is a subcomponent of the GameManager and manages the
    /// logic behind the player-controlled imps in a level. For example,
    /// it spawns imps and gets notified when an imp is selected by the player.
    /// </summary>
    public class ImpManager : MonoBehaviour, ImpController.IImpControllerListener, LevelManager.ILevelManagerListener,
        InputManager.IInputManagerListener
    {
        private LevelConfig config;
        private GameObject start;

        private List<ImpController> imps;

        private float spawnCounter;
        private int currentImps;
        private int[] professions;

        private ImpController impSelected;

        public GameObject ImpPrefab;

        private List<IMpManagerListener> listeners;

        public static ImpManager Instance;

        public interface IMpManagerListener
        {
            void OnUpdateMaxProfessions(int[] professions);
        }

        public void RegisterListener(IMpManagerListener listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(IMpManagerListener listener)
        {
            listeners.Remove(listener);
        }

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }

            currentImps = 0;
            imps = new List<ImpController>();
            listeners = new List<IMpManagerListener>();
        }

        public void SetLevelConfig(LevelConfig config, GameObject start)
        {
            currentImps = 0;
            this.config = config;
            this.start = start;
            professions = new int[9];
        }

        private void SelectProfession(ImpType profession)
        {
            if (impSelected == null)
            {
                Debug.Log("No imp selected");
            }
            else
            {
                if (!impSelected.GetComponent<ImpTrainingService>().IsTrainable)
                {
                    Debug.Log("This imp is currently not trainable");
                }
                else
                {
                    if (impSelected.GetComponent<ImpTrainingService>().Type == profession)
                    {
                        Debug.Log("The selected imp already has that profession.");
                    }
                    else
                    {
                        if (profession != ImpType.Unemployed)
                        {
                            if (IsTrainingLimitReached(profession))
                            {
                                UpdateMaxProfessions(profession);
                                impSelected.GetComponent<ImpTrainingService>().Train(profession);
                            }
                            else
                            {
                                Debug.Log("You cannot train anymore imps of that profession.");
                            }
                        }
                        else
                        {
                            UpdateMaxProfessions();
                            impSelected.GetComponent<ImpTrainingService>().Train(profession);
                        }
                    }
                }
            }
        }

        public int[] GetProfessions()
        {
            return professions;
        }

        public int[] GetProfessionsMax()
        {
            return config.MaxProfessions;
        }

        private bool IsTrainingLimitReached(ImpType profession)
        {
            return professions[(int) profession] < config.MaxProfessions[(int) profession];
        }

        private void UpdateMaxProfessions(ImpType profession)
        {
            UpdateMaxProfessions();
            professions[(int) profession]++;
            listeners.ForEach(l => l.OnUpdateMaxProfessions(professions));
        }

        private void UpdateMaxProfessions()
        {
            if (impSelected.GetComponent<ImpTrainingService>().Type == ImpType.Unemployed ||
                professions[(int) impSelected.GetComponent<ImpTrainingService>().Type] <= 0) return;
            professions[(int) impSelected.GetComponent<ImpTrainingService>().Type]--;
            listeners.ForEach(l => l.OnUpdateMaxProfessions(professions));
        }

        private void UpdateMaxProfessions(ImpController imp)
        {
            if (imp.GetComponent<ImpTrainingService>().Type != ImpType.Unemployed &&
                professions[(int) imp.GetComponent<ImpTrainingService>().Type] > 0)
            {
                professions[(int) imp.GetComponent<ImpTrainingService>().Type]--;
                listeners.ForEach(l => l.OnUpdateMaxProfessions(professions));
            }
            imp.GetComponent<ImpTrainingService>().Train(ImpType.Unemployed);
        }

        public void NotifyMaxProfessions()
        {
            listeners.ForEach(l => l.OnUpdateMaxProfessions(professions));
        }

        public void SpawnImps()
        {
            if (currentImps == 0)
            {
                SpawnImp();
            }
            else if (IsMaxImpsReached() && IsSpawnTimeCooledDown())
            {
                SpawnImp();
            }
            else
            {
                spawnCounter += Time.deltaTime;
            }
        }

        private bool IsMaxImpsReached()
        {
            return currentImps < config.MaxImps;
        }

        private bool IsSpawnTimeCooledDown()
        {
            return spawnCounter >= config.SpawnInterval;
        }

        private void SpawnImp()
        {
            var spawnPosition = start.transform.position;
            var imp = (GameObject) Instantiate(ImpPrefab, spawnPosition, Quaternion.identity);
            var impController = imp.GetComponent<ImpController>();
            impController.RegisterListener(this);
            impController.gameObject.GetComponent<ImpAnimationHelper>().MoveToSortingLayerPosition(currentImps);
            currentImps++;

            imps.Add(impController);

            spawnCounter = 0f;
        }

        #region interface implementation

        void ImpController.IImpControllerListener.OnImpSelected(ImpController impController)
        {
            SelectImp(impController);
        }

        private void DisplaySelectionOfSelectedImp()
        {
            impSelected.gameObject.GetComponent<ImpUIService>().Selection.Display();
        }

        private void HideSelectionOfAllImps()
        {
            foreach (var imp in imps)
            {
                imp.gameObject.GetComponent<ImpUIService>().Selection.Hide();
            }
        }

        private void SelectImp(ImpController imp)
        {
            impSelected = imp;
            HideSelectionOfAllImps();
            DisplaySelectionOfSelectedImp();
        }

        void ImpController.IImpControllerListener.OnImpHurt(ImpController impController)
        {
            imps.Remove(impController);
            currentImps--;
            impController.UnregisterListener(this);
        }

        void InputManager.IInputManagerListener.OnDisplayImpLabels()
        {
            foreach (var imp in imps)
            {
                imp.GetComponent<ImpUIService>().DisplayLabel();
            }
        }

        void InputManager.IInputManagerListener.OnProfessionSelected(ImpType profession)
        {
            SelectProfession(profession);
        }

        void InputManager.IInputManagerListener.OnSelectNextImp()
        {
            if (impSelected == null)
            {
                if (imps.Count != 0)
                {
                    SelectImp(imps[Random.Range(0, imps.Count - 1)]);
                }
            }
            else
            {
                var indexOfCurrentImp = imps.IndexOf(impSelected);
                int indexOfNextImp;
                if (indexOfCurrentImp >= imps.Count - 1)
                {
                    indexOfNextImp = 0;
                }
                else
                {
                    indexOfNextImp = indexOfCurrentImp + 1;
                }
                SelectImp(imps[indexOfNextImp]);
            }
        }

        void LevelManager.ILevelManagerListener.OnLevelStarted(Level level)
        {
            SetLevelConfig(level.CurrentLevelConfig, level.Start);
        }

        #endregion

        public void OnUntrain(ImpController impController)
        {
            UpdateMaxProfessions(impController);
        }

        void InputManager.IInputManagerListener.OnDismissImpLabels()
        {
            foreach (var imp in imps)
            {
                imp.GetComponent<ImpUIService>().DismissLabel();
            }
        }
    }
}