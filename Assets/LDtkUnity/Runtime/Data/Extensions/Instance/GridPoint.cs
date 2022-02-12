﻿using Newtonsoft.Json;
using UnityEngine;

namespace LDtkUnity
{
    public partial class GridPoint
    {
        /// <value>
        /// Grid-based coordinate
        /// </value>
        [JsonIgnore] public Vector2Int UnityCoord => new Vector2Int(Cx, Cy);
        
        internal static GridPoint FromJson(string json)
        {
            return JsonConvert.DeserializeObject<GridPoint>(json, Converter.Settings);
        }
    }

    //this is here because it's not generated by quick-type.
    /// <summary>
    /// This object is just a grid-based coordinate used in Field values.
    /// </summary>
    public partial class GridPoint
    {
        /// <value>
        /// X grid-based coordinate
        /// </value>
        [JsonProperty("cx")] public int Cx { get; set; }

        /// <value>
        /// Y grid-based coordinate
        /// </value>
        [JsonProperty("cy")] public int Cy { get; set; }
    }
}