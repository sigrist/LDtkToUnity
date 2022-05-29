﻿using UnityEditor.AssetImporters;
using UnityEngine;

namespace LDtkUnity.Editor
{
    internal abstract class LDtkArtifactFactory
    {
        private readonly AssetImportContext _ctx;
        
        protected readonly string AssetName;
        protected readonly LDtkArtifactAssets Artifacts;

        protected LDtkArtifactFactory(AssetImportContext ctx, LDtkArtifactAssets assets, string assetName)
        {
            _ctx = ctx;
            Artifacts = assets;
            AssetName = assetName;
        }
        
        protected delegate Object AssetCreator();
        protected delegate bool HasIt(string assetName);
        protected bool TryCreateAsset(HasIt hasIt, AssetCreator creator)
        {
            if (Artifacts == null)
            {
                LDtkDebug.LogError("Null artifact assets. This needs to be instanced");
                return false;
            }

            if (hasIt.Invoke(AssetName))
            {
                LDtkDebug.Log("Already had this object cached");
                return false;
            }
            
            Object tile = creator.Invoke();
            AddArtifact(tile);
            return true;
        }
        
        protected abstract bool AddArtifactAction(Object obj);
        private void AddArtifact(Object obj)
        {
            if (obj == null)
            {
                LDtkDebug.LogError("Could not create and add artifact; was null. It will not be available in the importer");
                return;
            }
            
            if (AddArtifactAction(obj))
            {
                _ctx.AddObjectToAsset(obj.name, obj);
            }
        }
    }
}