using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DataClasses
{
    [Serializable]
    public class FootstepData
    {
        public TileBase tile;
        public AudioClip[] footsteps;
    }
}