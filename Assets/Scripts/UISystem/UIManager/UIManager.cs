using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using WeaponSystem;

namespace UISystem
{
    public abstract class UIManager : MonoBehaviour
    {
        [Tooltip("List of UI prefabs that will be cached but not opened when Awake() is called.")] [SerializeField]
        protected List<GameObject> uiPrefabsToCache;

        [Tooltip("List of UI prefabs that will be cached and opened when Awake() is called.")] [SerializeField]
        protected List<GameObject> uiPrefabsToInstantiate;

        [Tooltip("List of UI prefabs that will be cached and opened when Awake() is called.")] [SerializeField]
        protected List<GameObject> uiPrefabsToOpen;

        [Tooltip("The default parent transform of where the panel will be instantiated and attached to.")]
        [field: SerializeField]
        public Transform DefaultRoot { get; private set; }

        protected Dictionary<EUIType, GameObject> cachedUIDict;
        protected Dictionary<EUIType, List<UIController>> instantiatedUIDict;
        protected Dictionary<EUIType, List<UIController>> activeUIDict;
        

        protected virtual void Awake()
        {
            cachedUIDict = new Dictionary<EUIType, GameObject>();
            instantiatedUIDict = new Dictionary<EUIType, List<UIController>>();
            activeUIDict = new Dictionary<EUIType, List<UIController>>();

            // Check if any two of the three list of UI prefab related have overlaps, if do, throw an exception.

            if (uiPrefabsToCache.Intersect(uiPrefabsToInstantiate).Any())
            {
                throw new Exception(
                    $"The lists {nameof(uiPrefabsToCache)} and {nameof(uiPrefabsToCache)} have overlaps. Please make sure there is no overlaps.");
            }

            if (uiPrefabsToCache.Intersect(uiPrefabsToOpen).Any())
            {
                throw new Exception(
                    $"The lists {nameof(uiPrefabsToCache)} and {nameof(uiPrefabsToOpen)} have overlaps. Please make sure there is no overlaps.");
            }

            if (uiPrefabsToInstantiate.Intersect(uiPrefabsToOpen).Any())
            {
                throw new Exception(
                    $"The lists {nameof(uiPrefabsToInstantiate)} and {nameof(uiPrefabsToOpen)} have overlaps. Please make sure there is no overlaps.");
            }


            // All the UI prefabs that needs to be processed.
            List<GameObject> uiPrefabs = uiPrefabsToCache.Union(uiPrefabsToInstantiate).Union(uiPrefabsToOpen).ToList();

            // Cache all the UIs.
            foreach (var uiPrefab in uiPrefabs)
            {
                if (uiPrefab.TryGetComponent(out UIController controller))
                {
                    //Debug.Log(controller.Model == null);
                    cachedUIDict.Add(controller.Type, uiPrefab);
                }
                else
                {
                    throw new MissingComponentException(
                        $"The UI prefab {uiPrefab.name} is missing the component of PanelBase to properly cache.");
                }
            }

            // Instantiate the UIs that is in the list of UIs prefabs to instantiate and to open.
            // Then, close the panel that is not listed in the list of UIs to open but only to instantiate.
            foreach (var uiPrefab in uiPrefabsToInstantiate.Union(uiPrefabsToOpen))
            {
                UIController uiController =
                    CreateUI(uiPrefab.GetComponent<UIController>().Type).GetComponent<UIController>();
                
                if (!uiPrefabsToOpen.Contains(uiPrefab))
                {
                    CloseUI(uiController.Type, uiController.Model.ID);
                }
            }

            //DebugCachedPanelPrefabDict();
        }

        public bool OpenUI(EUIType uiType, int uiID, Transform root = null)
        {
            // Check if the panel has been cached.
            // If not, throw an exception indicating the cached prefab cannot be found.
            if (!cachedUIDict.TryGetValue(uiType, out GameObject cachedPanelPrefab))
            {
                throw new KeyNotFoundException("Trying to open a panel with a panel type enum that is not cached." +
                                               $"\n Panel type enum: {uiType}.");
            }

            // Check if the panel is already open.
            if (activeUIDict.ContainsKey(uiType) &&
                activeUIDict[uiType].FirstOrDefault(controller => controller.Model.ID == uiID) != null)
            {
                Debug.LogError("Trying to open a panel that has already been opened in scene." +
                               $"\n Panel UIType: {uiType}, Panel ID: {uiID}.");
                return false;
            }

            // Check if the panel has not yet been instantiated
            if (!instantiatedUIDict.ContainsKey(uiType))
            {
                Debug.LogError("Trying to open a panel that has not been instantiated." +
                               $"\n Panel UIType: {uiType}.");
                return false;
            }

            // The controller that will be used to open the UI.
            UIController targetController =
                instantiatedUIDict[uiType].FirstOrDefault(controller => controller.Model.ID == uiID);

            // Check if the panel has been instantiated but the ID of the UI to open does not exist.
            if (targetController == null)
            {
                Debug.LogError("Trying to open a panel that has been instantiated but the ID is does not exist." +
                               $"\n Panel UIType: {uiType}, Panel ID: {uiID}." +
                               $"Please check if the parameter ID is correct.");

                return false;
            }

            targetController.Open();
            activeUIDict[uiType].Add(targetController);

            return true;
        }


        public GameObject CreateUI(EUIType uiType, Transform root = null)
        {
            if (uiType == EUIType.Component)
            { 
                Debug.LogError("A UI of type Component cannot be created. It has to come with a parent UI that contains the component.");
            }
            
            // Check if the panel has been cached.
            // If not, throw an exception indicating the cached prefab cannot be found.
            if (!cachedUIDict.TryGetValue(uiType, out GameObject cachedUIPrefab))
            {
                throw new KeyNotFoundException("Trying to open a panel with a panel type enum that is not cached." +
                                               $"\n Panel type enum: {uiType}");
            }

            // Check if the UI to open for the first time have had instance in the scene and is not allowed to have multiple instances.
            if (instantiatedUIDict.TryGetValue(uiType, out List<UIController> controllers))
            {
                UIController targetController =
                    controllers.FirstOrDefault(controller => controller.Model.Type == uiType);
                
                if (!cachedUIPrefab.GetComponent<UIController>().CanHaveMultiple && targetController != null)
                {
                    Debug.LogWarning(
                        "Trying to open a panel that cannot have multiple instance in the scene, and there is already instance of it in the scene." +
                        $"Panel type: {uiType}.");
                    return null;
                }
            }

            // Check if there is no specified root transform for the instantiation while default instantiation transform is null as well.
            if (root == null)
            {
                if (DefaultRoot == null)
                {
                    throw new NullReferenceException(
                        $"Trying to open a panel at default root transform but the default root transform has not been assigned a value." +
                        $"\nPlease assign a value to the default room transform in the inspector of the panel prefab." +
                        $"\nPanel type: {uiType}.");
                }

                root = DefaultRoot;
            }
            
            GameObject panelGameObject = Instantiate(cachedUIPrefab, root, false);
            UIController controller = panelGameObject.GetComponent<UIController>();
            instantiatedUIDict.Add(uiType, new List<UIController>());
            instantiatedUIDict[uiType].Add(controller);
            activeUIDict.Add(uiType, new List<UIController>());
            activeUIDict[uiType].Add(controller);

            controller.Open();

            return panelGameObject;
        }

        public bool CloseUI(EUIType uiType, int uiID)
        {
            // Check if the UI has been cached.
            if (!cachedUIDict.TryGetValue(uiType, out GameObject cachedUIPrefab))
            {
                throw new KeyNotFoundException("Trying to close a UI with a UI type enum that is not cached." +
                                               $"\n UI type: {uiType}.");
            }

            // Check if the UI is allowed to close.
            if (!cachedUIPrefab.GetComponent<UIController>().CanClose)
            {
                Debug.LogWarning("Trying to close a UI that cannot be closed currently." +
                                 $"\n UI type: {uiType}");
                return false;
            }

            // Check if the UI is instantiated.
            if (!instantiatedUIDict.ContainsKey(uiType))
            {
                throw new Exception("Trying to close a UI that is not instantiated." +
                                    $"UI type: {uiType}.");
            }

            UIController targetController =
                activeUIDict[uiType].FirstOrDefault(controller => controller.Model.ID == uiID);

            // Check if the UI is instantiated but the parameter uiID does not exist.
            if (targetController == null)
            {
                Debug.LogWarning("Trying to close a UI that has already been closed in scene." +
                                 $"\n UI ID: {uiID}");
                return false;
            }


            targetController.Close();
            activeUIDict[uiType].Remove(targetController);

            return true;
        }

        /// <summary>
        /// Close all the panels that can be closed.
        /// </summary>
        public void CloseAllUIs()
        {
            foreach (var controllers in activeUIDict.Values)
            {
                foreach (var controller in controllers)
                {
                    if (controller.CanClose)
                    {
                        controller.Close();
                        controllers.Remove(controller);
                    }
                }
            }
        }

        #region Debug Util

        private void DebugCachedPanelPrefabDict()
        {
            string str = "----------Cached Panel Prefab Dict----------\n";

            foreach (var kyPair in cachedUIDict)
            {
                str += $"{kyPair.Key} --- {kyPair.Value}\n";
            }

            str += "----------Cached Panel Prefab Dict----------";
            Debug.Log(str);
        }

        #endregion
    }
}
    