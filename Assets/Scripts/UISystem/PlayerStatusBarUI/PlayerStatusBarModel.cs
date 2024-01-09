using System;
using System.Collections.Generic;
using System.Linq;
using BuffSystem.Common;

namespace UISystem
{
    public class PlayerStatusBarModel : UIModel
    {
        public event EventHandler<AddStatusEventArgs> OnAddStatus;
        public event EventHandler<RemoveStatusEventArgs> OnRemoveStatus;
        
        /// <summary>
        /// The status that the player has in descending order of display priority.
        /// </summary>
        private SortedList<int, Buff> _status; //TODO: make the list in BuffHandler that stores all the status also a SortedList to save memory.
        /// <summary>
        /// The status that the player has and is displaying.
        /// </summary>
        private List<Buff> _displayingStatus;
        
        private int _maxDisplayAmount;
        
        private BuffHandler _playerBuffHandler;
        
        public PlayerStatusBarModel(
            PlayerStatusBarController controller, 
            int maxDisplayAmount) : base(EUIType.PanelPlayerBuff, controller)
        {
            Controller = controller;
            _maxDisplayAmount = maxDisplayAmount;
            
            _displayingStatus = new List<Buff>();
            _status = new SortedList<int, Buff>();
            
            _playerBuffHandler = PlayerStats.Instance.BuffHandler;
            if (_playerBuffHandler == null)
            {
                throw new Exception(
                    "PlayerBuffHandler is null in PanelPlayerBuff. " +
                    "This could be caused by inappropriate script execution order: PanelPlayerBuff.Init() is called before Player.Awake().");
            }

            _playerBuffHandler.AddBuffCallback += AddStatus;
            _playerBuffHandler.RemoveBuffCallback += RemoveStatus;
        }

        public void AddStatus(Buff status)
        {
            int priority;
            if (status is not IDisplayable)
            {
                priority = int.MinValue;
            }
            else
            {
                priority = ((IDisplayable)status).Priority;
            }

            _status.Add(priority, status);

            // Broadcast the event.
            List<Buff> statusToDisplay = new List<Buff>(_maxDisplayAmount);
            for (int i = 0; i < _maxDisplayAmount; i++)
            {
                statusToDisplay.Add(_status.Values.ElementAt(i));
            }

            AddStatusEventArgs args = new AddStatusEventArgs(status, statusToDisplay);
            OnAddStatus?.Invoke(this, args);
        }

        public void RemoveStatus(Buff status)
        {
            _status.RemoveAt(_status.IndexOfValue(status));
            
            // Broadcast the event.
            List<Buff> statusToDisplay = new List<Buff>(_maxDisplayAmount);
            for (int i = 0; i < _maxDisplayAmount; i++)
            {
                statusToDisplay.Add(_status.Values.ElementAt(i));
            }

            RemoveStatusEventArgs args = new RemoveStatusEventArgs(status, statusToDisplay);
            OnRemoveStatus?.Invoke(this,args);
        }
    }
}