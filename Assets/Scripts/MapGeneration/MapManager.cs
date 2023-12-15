using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = System.Random;

namespace MapGeneration
{
    public class MapManager : MonoBehaviour
    {
        public int RoomNum;
        public int Dimension;


        public List<RoomConfig> AllRooms { get; private set; }

        private RoomConfig[,] _matrix;
        private HashSet<Vector2Int> _appendablePositions;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Generate(RoomNum, Dimension);
                print(DebugMapMatrix());
            }
        }

        public void Generate(int roomNum, int dimension)
        {
            AllRooms = new List<RoomConfig>(roomNum);
            _matrix = new RoomConfig[dimension, dimension];
            _appendablePositions = new HashSet<Vector2Int>();
            Random random = new Random();

            // Pick a random position in the 2D array as the start
            int xCoord = random.Next(dimension);
            int yCoord = random.Next(dimension);
            Vector2Int startRoomCoord = new Vector2Int(xCoord, yCoord);
            RoomConfig startRoom = new RoomConfig(_matrix, startRoomCoord);
            _matrix[yCoord, xCoord] = startRoom;
            UpdateAppendablePosition(startRoomCoord);

            AllRooms.Add(startRoom);

            // Run a for loop of roomNum - 1 to generate the rest of the room
            for (int i = 1; i < roomNum; i++)
            {
                // Choose an appendable position as the new room
                Vector2Int newRoomCoord = _appendablePositions.ElementAt(random.Next(_appendablePositions.Count));
                RoomConfig newRoom = new RoomConfig(_matrix, newRoomCoord);

                UpdateAppendablePosition(newRoomCoord);

                _matrix[newRoomCoord.y, newRoomCoord.x] = newRoom;
                AllRooms.Add(newRoom);
            }
        }

        public string DebugMapMatrix()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int row = 0; row < _matrix.GetLength(0); row++)
            {
                for (int col = 0; col < _matrix.GetLength(1); col++)
                {
                    if (_matrix[row, col] == null)
                    {
                        stringBuilder.Append("0 ");
                    }
                    else
                    {
                        stringBuilder.Append("1 ");
                    }
                }

                stringBuilder.Append("\n");
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Check the four adjacent positions of a coordination. If the adjacent position is not
        /// occupied, add the position to the appendable position set.
        /// </summary>
        /// <param name="coord">The coordination to check if adjacent position is appendable. </param>
        private void UpdateAppendablePosition(Vector2Int coord)
        {
            int xCoord = coord.x;
            int yCoord = coord.y;

            _appendablePositions.Remove(coord);

            if (yCoord + 1 < _matrix.GetLength(0) && _matrix[yCoord + 1, xCoord] == null)
            {
                _appendablePositions.Add(coord + Vector2Int.up);
            }

            if (yCoord > 0 && _matrix[yCoord - 1, xCoord] == null)
            {
                _appendablePositions.Add(coord + Vector2Int.down);
            }

            if (xCoord + 1 < _matrix.GetLength(1) && _matrix[yCoord, xCoord + 1] == null)
            {
                _appendablePositions.Add(coord + Vector2Int.right);
            }

            if (xCoord > 0 && _matrix[yCoord, xCoord - 1] == null)
            {
                _appendablePositions.Add(coord + Vector2Int.left);
            }
        }
    }
}