using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UISystem
{
    public abstract class UIManager : MonoBehaviour
    {
        [SerializeField] protected List<GameObject> _panelPrefabList;
        protected Dictionary<EPanelType, GameObject> _cachedPanelPrefabDict;
        protected Dictionary<string, PanelBase> _instantiatedPanelGameObjDict;
        protected Dictionary<string, PanelBase> _activePanelGameObjDict;

        [Tooltip("The default parent transform of where the panel will be instantiated and attached to.")]
        [field: SerializeField]
        public Transform DefaultRoot { get; private set; }

        protected virtual void Awake()
        {
            _cachedPanelPrefabDict = new Dictionary<EPanelType, GameObject>();
            _instantiatedPanelGameObjDict = new Dictionary<string, PanelBase>();
            _activePanelGameObjDict = new Dictionary<string, PanelBase>();

            CachePanels();

            //DebugCachedPanelPrefabDict();
        }

        private void CachePanels()
        {
            if (_panelPrefabList.Count == 0)
            {
                Debug.LogWarning(
                    "No panel prefab is stored in the panel Prefab List. Please assign panel prefab before cache them.");
                return;
            }

            foreach (var panelPrefab in _panelPrefabList)
            {
                if (panelPrefab.TryGetComponent(out PanelBase panelBase))
                {
                    if (!_cachedPanelPrefabDict.TryAdd(panelBase.Type, panelPrefab))
                    {
                        throw new Exception("Trying to cache a panel with the same panel type enum" +
                                            $"\nPanel enum: {panelPrefab.GetComponent<PanelBase>().Type}. " +
                                            $"\nName of the panel prefab that has the same type: {_cachedPanelPrefabDict[panelBase.Type].name}.");
                    }
                }
                else
                {
                    throw new Exception("Trying to cache a panel prefab with no PanelBase script attached to it." +
                                        $"\nPanelPrefab name: {panelPrefab.name}.");
                }
            }
        }

        public GameObject OpenPanel(EPanelType panelType, string panelID = "", Transform root = null)
        {
            // Check if the panel has been cached.
            // If not, throw an exception indicating the cached prefab cannot be found.
            if (!_cachedPanelPrefabDict.TryGetValue(panelType, out GameObject cachedPanelPrefab))
            {
                throw new KeyNotFoundException("Trying to open a panel with a panel type enum that is not cached." +
                                               $"\n Panel type enum: {panelType}");
            }

            // Check if the panel is already open.
            // If so, return the method instantly.
            if (_activePanelGameObjDict.ContainsKey(panelID))
            {
                Debug.LogWarning("Trying to open a panel that has already been opened in scene." +
                                 $"\n Panel ID: {panelID}");
                return null;
            }

            // Check if the panel has not yet been instantiated
            // Case of if the panel has not been instantiated
            if (_instantiatedPanelGameObjDict.Values.FirstOrDefault(panel => panel.Type == panelType) == null)
            {
                Debug.LogWarning("Trying to open a panel that has not been instantiated." +
                                 $"\n Panel ID: {panelID}");
            }


            if (!_instantiatedPanelGameObjDict.ContainsKey(panelID))
            {
                Debug.LogWarning(
                    "Trying to open a panel that exists in the scene but the passed in parameter panelID is incorrect." +
                    $"Panel type: {panelType}, Panel ID: {panelID}");
                return null;
            }

            _instantiatedPanelGameObjDict[panelID].Open();
            _activePanelGameObjDict.Add(_instantiatedPanelGameObjDict[panelID].name,
                _instantiatedPanelGameObjDict[panelID].GetComponent<PanelBase>());

            return _instantiatedPanelGameObjDict[panelID].gameObject;
        }


        public GameObject OpenPanelFirstTime(EPanelType panelType, Transform root = null)
        {
            // Check if the panel has been cached.
            // If not, throw an exception indicating the cached prefab cannot be found.
            if (!_cachedPanelPrefabDict.TryGetValue(panelType, out GameObject cachedPanelPrefab))
            {
                throw new KeyNotFoundException("Trying to open a panel with a panel type enum that is not cached." +
                                               $"\n Panel type enum: {panelType}");
            }

            PanelBase targetPanelBase =
                _instantiatedPanelGameObjDict.Values.FirstOrDefault(panel => panel.Type == panelType);

            if (!cachedPanelPrefab.GetComponent<PanelBase>().CanHaveMultiple && targetPanelBase != null)
            {
                Debug.LogWarning("Trying to open a panel that cannot have multiple instance in the scene." +
                                 $"Panel type: {panelType}.");
                return null;
            }


            // Check if there is no specified root transform for the instantiation while default instantiation transform is null as well.
            // If so, throw an exception suggesting to set a default instantiation transform for the panel prefab.
            if (root == null)
            {
                if (DefaultRoot == null)
                {
                    throw new NullReferenceException(
                        $"Trying to open a panel at default root transform but the default root transform has not been assigned a value." +
                        $"\nPlease assign a value to the default room transform in the inspector of the panel prefab." +
                        $"\nPanel type: {panelType}");
                }

                root = DefaultRoot;
            }

            GameObject panelGameObject = Instantiate(cachedPanelPrefab, root, false);
            PanelBase panelBase = panelGameObject.GetComponent<PanelBase>();
            _instantiatedPanelGameObjDict.Add(panelGameObject.name, panelBase);
            _activePanelGameObjDict.Add(panelGameObject.name, panelBase);
            panelBase.Init();
            panelBase.Open();

            return panelGameObject;
        }

        public void ClosePanel(EPanelType panelType, string panelID)
        {
            // Check if the panel has been cached.
            // If not, throw an exception indicating the cached prefab cannot be found.
            if (!_cachedPanelPrefabDict.TryGetValue(panelType, out GameObject cachedPanelPrefab))
            {
                throw new KeyNotFoundException("Trying to close a panel with a panel type enum that is not cached." +
                                               $"\n Panel enum: {panelType}");
            }

            if (!cachedPanelPrefab.GetComponent<PanelBase>().CanClose)
            {
                Debug.LogWarning("Trying to close a panel that cannot be closed currently." +
                                 $"\n Panel enum: {panelType}");
                return;
            }

            // Check if the panel is instantiated.
            // If not, return the method instantly.
            if (!_instantiatedPanelGameObjDict.ContainsKey(panelID))
            {
                Debug.LogWarning("Trying to close a panel that is not instantiated." +
                                 $"Panel ID: {panelID}.");
            }
            else
            {
                if (!_activePanelGameObjDict.ContainsKey(panelID))
                {
                    Debug.LogWarning("Trying to close a panel that has already been closed in scene." +
                                     $"\n Panel ID: {panelID}");
                }
                else
                {
                    _activePanelGameObjDict[panelID].Close();
                    _activePanelGameObjDict.Remove(panelID);
                }
            }
        }

        /// <summary>
        /// Close all the panels that can be closed.
        /// </summary>
        public void CloseAllPanels()
        {
            foreach (var panelBase in _activePanelGameObjDict.Values)
            {
                if (panelBase.CanClose)
                {
                    panelBase.Close();
                    _activePanelGameObjDict.Remove(panelBase.gameObject.name);
                }
            }
        }

        public void RemovePanelGameObj(EPanelType panelType, string panelID)
        {
            // Check if the panel has been cached.
            // If not, throw an exception indicating the cached prefab cannot be found.
            if (!_cachedPanelPrefabDict.TryGetValue(panelType, out GameObject cachedPanelPrefab))
            {
                throw new KeyNotFoundException("Trying to remove a panel with a panel type enum that is not cached." +
                                               $"\n Panel enum: {panelType}");
            }

            // Check if the panel is instantiated
            if (!_instantiatedPanelGameObjDict.ContainsKey(panelID))
            {
                Debug.LogError(
                    "Trying to destroy a panel GameObject in scene that neither has an invalid panelID nor is instantiated in the scene." +
                    $"\nPanel ID: {panelID}");
                return;
            }

            // Check if the panel is currently open in the scene.
            // If so, close it first.
            // Then destroy the panel GameObject and remove it from instantiated-panel and cached-panel dictionary

            if (_activePanelGameObjDict.ContainsKey(panelID))
            {
                ClosePanel(panelType, panelID);
            }

            Destroy(_instantiatedPanelGameObjDict[panelID].gameObject);
            _instantiatedPanelGameObjDict.Remove(panelID);
        }

        #region Debug Util

        private void DebugCachedPanelPrefabDict()
        {
            string str = "----------Cached Panel Prefab Dict----------\n";

            foreach (var kyPair in _cachedPanelPrefabDict)
            {
                str += $"{kyPair.Key} --- {kyPair.Value}\n";
            }

            str += "----------Cached Panel Prefab Dict----------";
            Debug.Log(str);
        }

        #endregion
    }
}