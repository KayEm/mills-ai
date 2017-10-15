﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Mills.Models
{
    /// <summary>
    /// The board.
    /// </summary>
    public class BoardModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="players">Players in the game.</param>
        /// <param name="points">Points for the board.</param>
        public BoardModel(List<PlayerModel> players, List<PointModel> points)
        {
            Players = players;
            Points = points;
        }

        /// <summary>
        /// Collection of points on the board.
        /// </summary>
        public List<PointModel> Points { get; private set; }

        /// <summary>
        /// Players of the game.
        /// </summary>
        public List<PlayerModel> Players { get; private set; }

        /// <summary>
        /// Player in current turn.
        /// </summary>
        public PlayerModel CurrentPlayer { get; set; }

        /// <summary>
        /// Action raised when new piece is placed on the board.
        /// </summary>
        public event Action<PointModel> NewPiecePlaced;

        /// <summary>
        /// Place a new piece on the board.
        /// </summary>
        /// <param name="piece">Piece to be placed.</param>
        /// <param name="point">Point on which to place the piece.</param>
        public void PlaceNewPiece(PieceModel piece, PointModel point)
        {
            point.Piece = piece;

            NewPiecePlaced(point);
        }

        /// <summary>
        /// Checks if a new piece can be placed on the board.
        /// </summary>
        /// <returns>True, if the new piece can be placed; otherwise false.</returns>
        public bool CanPlaceNewPiece()
        {
            return Points.Count(p => p.Piece != null && p.Piece.Color == CurrentPlayer.Color) < 9;
        }

        /// <summary>
        /// Checks if the point is empty.
        /// </summary>
        /// <param name="position">Position to to be checked.</param>
        /// <returns>True and the corresponding point, if empty point is found at that position.</returns>
        public Tuple<bool, PointModel> IsPointEmpty(Point position)
        {
            var point = Points.Where(p  => p.Bounds.Contains(position)).FirstOrDefault();

            if (point != null)
            {
                return new Tuple<bool, PointModel>(true, point);
            }

            return new Tuple<bool, PointModel>(false, null);
        }
    }
}