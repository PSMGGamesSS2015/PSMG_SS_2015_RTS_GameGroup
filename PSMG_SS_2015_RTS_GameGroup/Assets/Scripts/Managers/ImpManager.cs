using System.Collections.Generic;
using Assets.Scripts.Config;
using Assets.Scripts.Controllers.Characters.Imps;
using Assets.Scripts.Types;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    /// <summary>
    /// The ImpManager is a subcomponent of the GameManager and manages the
    /// logic behind the player-controlled imps in a level. For example,
    /// it spawns imps and gets notified when an imp is selected by the player.
    /// </summary>

    public class ImpManager : MonoBehaviour, ImpController.IImpControllerListener, LevelManager.ILevelManagerListener, InputManager.IInputManagerListener {

        private LevelConfig config;
        private GameObject start;

        private List<ImpController> imps;

        private float spawnCounter;
        private int currentImps;
        private int[] professions;

        private ImpController impSelected;

        public GameObject ImpPrefab;

        private List<IMpManagerListener> listeners;

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
                if (!impSelected.ImpTrainingService.IsTrainable)
                {
                    Debug.Log("This imp is currently not trainable");
                }
                else
                {
                    if (impSelected.ImpTrainingService.Type == profession)
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
                                impSelected.ImpTrainingService.Train(profession);
                            }
                            else
                            {
                                Debug.Log("You cannot train anymore imps of that profession.");
                            }
                        }
                        else
                        {
                            UpdateMaxProfessions();
                            impSelected.ImpTrainingService.Train(profession);
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
            return professions[(int)profession] < config.MaxProfessions[(int)profession];
        }

        private void UpdateMaxProfessions(ImpType profession)
        {
            UpdateMaxProfessions();
            professions[(int)profession]++;
            foreach (IMpManagerListener listener in listeners)
            {
                listener.OnUpdateMaxProfessions(professions);
            }
        }

        private void UpdateMaxProfessions()
        {
            if (impSelected.ImpTrainingService.Type != ImpType.Unemployed &&
                professions[(int)impSelected.ImpTrainingService.Type] > 0)
            {
                professions[(int)impSelected.ImpTrainingService.Type]--;
                foreach (IMpManagerListener listener in listeners)
                {
                    listener.OnUpdateMaxProfessions(professions);
                }
            }
        
        }

        private void UpdateMaxProfessions(ImpController imp)
        {
            if (imp.ImpTrainingService.Type != ImpType.Unemployed &&
                professions[(int)imp.ImpTrainingService.Type] > 0)
            {
                professions[(int)imp.ImpTrainingService.Type]--;
                foreach (IMpManagerListener listener in listeners)
                {
                    listener.OnUpdateMaxProfessions(professions);
                }
            }
            imp.ImpTrainingService.Train(ImpType.Unemployed);
        
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
            var imp = (GameObject)Instantiate(ImpPrefab, spawnPosition, Quaternion.identity);
            var impController = imp.GetComponent<ImpController>();
            impController.RegisterListener(this);
            impController.MoveToSortingLayerPosition(currentImps);
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
            impSelected.Selection.Display();
        }

        private void HideSelectionOfAllImps()
        {
            foreach (var imp in imps)
            {
                imp.Selection.Hide();
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
                imp.ImpUIService.DisplayLabel();
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

        void LevelManager.ILevelManagerListener.OnLevelStarted(LevelConfig config, GameObject start)
        {
            SetLevelConfig(config, start);
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
                imp.ImpUIService.DismissLabel();
            }
        }

    }
}