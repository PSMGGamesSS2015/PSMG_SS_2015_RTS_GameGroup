using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Config;
using Assets.Scripts.Controllers.Characters.Imps;
using Assets.Scripts.Controllers.Characters.Imps.SubServices;
using Assets.Scripts.Controllers.Objects;
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
        private Vector3 spawnPosition;

        public List<ImpController> Imps;

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
            Imps = new List<ImpController>();
            listeners = new List<IMpManagerListener>();
        }

        public void SetLevel(Level level)
        {
            currentImps = 0;
            config = level.CurrentLevelConfig;
            professions = new int[9];
            spawnPosition = level.Start.transform.position;
        }

        // TODO refactor
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
            var imp = (GameObject) Instantiate(ImpPrefab, spawnPosition, Quaternion.identity);
            var impController = imp.GetComponent<ImpController>();
            impController.RegisterListener(this);
            impController.gameObject.GetComponent<ImpSpriteManagerService>().MoveToSortingLayerPosition(currentImps);
            currentImps++;

            Imps.Add(impController);

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
            foreach (var imp in Imps)
            {
                imp.gameObject.GetComponent<ImpUIService>().Selection.Hide();
            }
        }

        private void SelectImp(ImpController imp)
        {
            impSelected = imp;
            HideSelectionOfAllImps();
            DisplaySelectionOfSelectedImp();
            imp.GetComponent<ImpAudioService>().PlaySelectionSound();
        }

        void ImpController.IImpControllerListener.OnImpHurt(ImpController impController)
        {
            UIManager.Instance.UIImpOutOfSightService.OnImpHurt(impController);
            var type = impController.GetComponent<ImpTrainingService>().Type;
            if (type != ImpType.Unemployed)
            {
                professions[(int) type]--;
            }
            
            NotifyMaxProfessions();

            Imps.Remove(impController);

            currentImps--;
            impController.UnregisterListener(this);
        }

        void InputManager.IInputManagerListener.OnProfessionSelected(ImpType profession)
        {
            SelectProfession(profession);
        }

        void InputManager.IInputManagerListener.OnSelectNextImp()
        {
            if (impSelected == null)
            {
                if (Imps.Count != 0)
                {
                    SelectImp(Imps[Random.Range(0, Imps.Count - 1)]);
                }
            }
            else
            {
                var indexOfCurrentImp = Imps.IndexOf(impSelected);
                int indexOfNextImp;
                if (indexOfCurrentImp >= Imps.Count - 1)
                {
                    indexOfNextImp = 0;
                }
                else
                {
                    indexOfNextImp = indexOfCurrentImp + 1;
                }
                SelectImp(Imps[indexOfNextImp]);
            }
        }

        void InputManager.IInputManagerListener.OnSelectNextUnemployedImp()
        {
            if (impSelected == null)
            {
                foreach (var ic in Imps.Where(ic => ic.GetComponent<ImpTrainingService>().Type == ImpType.Unemployed))
                {
                    SelectImp(ic);
                    return;
                }
            }
            else
            {
                for (var i = Imps.IndexOf(impSelected); i < Imps.IndexOf(impSelected) + Imps.Count; i++)
                {
                    var y = i;
                    if (i >= Imps.Count)
                    {
                        y = i - Imps.Count;
                    }
                    if (Imps[y].GetComponent<ImpTrainingService>().Type != ImpType.Unemployed) continue;
                    if (Imps[y].GetInstanceID() == impSelected.GetInstanceID()) continue;
                    SelectImp(Imps[y]);
                    return;
                }
            }
        }

        void LevelManager.ILevelManagerListener.OnLevelStarted(Level level)
        {
            SetLevel(level);
        }

        #endregion

        public void OnUntrain(ImpController impController)
        {
            UpdateMaxProfessions(impController);
        }

        void ImpController.IImpControllerListener.OnCheckpointReached(CheckPointController checkPointController)
        {
            spawnPosition = checkPointController.transform.position;
        }
    }
}