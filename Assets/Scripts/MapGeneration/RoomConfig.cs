using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MapGeneration
{
    public enum ERoomAppendablePosition
    {
        Top,
        Bottom,
        Left,
        Right
    }
    
    public class RoomConfig
    {
        public Vector2Int Coordination { get; private set; }
        public int DistanceToStart { get; private set; }

        public List<ERoomAppendablePosition> AppendablePositions { get; private set; }
        
        // public bool AppendableOnTop { get; private set; }
        // public bool AppendableOnBottom { get; private set; }
        // public bool AppendableOnLeft { get; private set; }
        // public bool AppendableOnRight { get; private set; }
        
        private RoomConfig[,] _matrix;

        public RoomConfig(RoomConfig[,] matrix, Vector2Int coord)
        {
            Coordination = coord;
            int xCoord = coord.x;
            int yCoord = coord.y;
            
            _matrix = matrix;
        }

        // public bool RemoveAppendablePosition(ERoomAppendablePosition roomAppendablePosition)
        // {
        //     return AppendablePositions.Remove(roomAppendablePosition);
        // }

        // public void DebugAppendablePositions()
        // {
        //     StringBuilder stringBuilder = new StringBuilder();
        //     foreach (var e in AppendablePositions)
        //     {
        //         stringBuilder.Append(e + " ");
        //     }
        //
        //     Debug.Log(Coordination + ": " + stringBuilder);
        // }
    }
}