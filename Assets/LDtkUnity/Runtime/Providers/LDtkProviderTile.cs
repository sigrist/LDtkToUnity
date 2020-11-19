﻿using System.Collections.Generic;
using LDtkUnity.Runtime.UnityAssets.IntGridValue;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LDtkUnity.Runtime.Providers
{
    public static class LDtkProviderTile
    {
        private static Dictionary<string, Tile> _cachedTiles;

#if UNITY_2019_2_OR_NEWER
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
#endif
        public static void Dispose()
        {
            _cachedTiles = null;
        }
        public static void Init()
        {
            _cachedTiles = new Dictionary<string, Tile>();
        }

        public static Tile GetTile(LDtkIntGridValueAsset asset, Color color)
        {
            string name = asset.name;
        
            if (_cachedTiles.ContainsKey(name))
            {
                return _cachedTiles[name];
            }

            Tile newTile = MakeTile(asset.ReferencedAsset, color);
            _cachedTiles.Add(name, newTile);
            return newTile;
        }

        private static Tile MakeTile(Sprite sprite, Color color)
        {
            Tile tile = ScriptableObject.CreateInstance<Tile>();
            tile.colliderType = Tile.ColliderType.Sprite;
            tile.sprite = sprite;
            tile.color = color;
            return tile;
        }
    }
}