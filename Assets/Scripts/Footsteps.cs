using System.Collections.Generic;
using System.Linq;
using DataClasses;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(AudioSource))]
public class Footsteps : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private FootstepData[] footstepTable;
    [SerializeField] private Transform feet;

    private AudioSource _audioSource;
    private Dictionary<TileBase, AudioClip[]> _footstepMap;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _footstepMap = footstepTable.ToDictionary(k => k.tile, v => v.footsteps);
    }

    public void PlayFootstep()
    {
        Vector3Int cellPosition = tilemap.WorldToCell(feet.position);
        TileBase tile = tilemap.GetTile(cellPosition);
        bool success = _footstepMap.TryGetValue(tile, out AudioClip[] footsteps);

        if (!success || footsteps.Length == 0) return;
        AudioClip footstep = footsteps[Random.Range(0, footsteps.Length)];
        _audioSource.PlayOneShot(footstep);
    }
}
