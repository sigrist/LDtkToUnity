﻿using UnityEditor.AssetImporters;
using UnityEngine;

namespace LDtkUnity.Editor
{
    internal class LDtkBackgroundArtifactFactory : LDtkArtifactFactory
    {
        private readonly LDtkTextureSpriteSlicer _slicer;

        public LDtkBackgroundArtifactFactory(AssetImportContext ctx, LDtkArtifactAssets artifacts, string assetName, Texture2D srcTex, int pixelsPerUnit, Level lvl) : base(ctx, artifacts, assetName)
        {
            Rect rect = lvl.BgPos.UnityCropRect;
            rect.position = LDtkCoordConverter.LevelBackgroundImageSliceCoord(rect.position, srcTex.height, rect.height);
            _slicer = new LDtkTextureSpriteSlicer(srcTex, rect, pixelsPerUnit, Vector2.up);
        }
        
        public bool TryCreateBackground() => TryCreateAsset(Artifacts.HasIndexedBackground, CreateBackground);

        private Sprite CreateBackground()
        {
            Sprite sprite = _slicer.Slice();

            if (sprite == null)
            {
                LDtkDebug.LogError("LDtk: Couldn't retrieve a sliced sprite for background");
                return null;
            }
            
            sprite.name = AssetName;
            return sprite;
        }

        protected override bool AddArtifactAction(Object obj)
        {
            return Artifacts.AddBackground((Sprite)obj);
        }
    }
}