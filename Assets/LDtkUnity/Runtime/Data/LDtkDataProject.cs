﻿// ReSharper disable InconsistentNaming

using LDtkUnity.Runtime.Data.Definition;
using LDtkUnity.Runtime.Data.Level;
using LDtkUnity.Runtime.Tools;

namespace LDtkUnity.Runtime.Data
{
    //https://github.com/deepnight/ldtk/blob/master/JSON_DOC.md#ldtk-json-root
    public struct LDtkDataProject
    {
        /// <summary>
        /// File format version
        /// </summary>
        public string jsonVersion;

        /// <summary>
        /// Default X pivot (0 to 1) for new entities
        /// </summary>
        public float defaultPivotX;
        
        /// <summary>
        /// Default Y pivot (0 to 1) for new entities
        /// </summary>
        public float defaultPivotY;
        
        /// <summary>
        /// Default grid size for new layers
        /// </summary>
        public int defaultGridSize;

        /// <summary>
        /// Project background color. Hexadecimal string using "#rrggbb" format
        /// </summary>
        public string bgColor;
        
        /// <summary>
        /// If TRUE, the Json is partially minified (no indentation, nor line breaks)
        /// </summary>
        public bool minifyJson;        
        
        /// <summary>
        /// If TRUE, a Tiled compatible file will also be generated along with the LDtk JSON file.
        /// </summary>
        public bool exportTiled;
        
        /// <summary>
        /// A structure containing all the definitions of this project
        /// </summary>
        public LDtkDefinitions defs;
        
        /// <summary>
        /// Array of Level
        /// </summary>
        public LDtkDataLevel[] levels;

        public LDtkDataLevel GetLevelByUid(int uid) => LDtkToolUid.GetDefinitionByUid(uid, levels, lvl => lvl.uid);
    }
}