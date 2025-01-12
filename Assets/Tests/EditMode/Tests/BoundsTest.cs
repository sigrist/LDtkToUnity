﻿using System.Linq;
using NUnit.Framework;
using UnityEngine;

namespace LDtkUnity.Tests
{
    public class BoundsTest
    {
        [Test]
        public void GetLevelBounds()
        {
            //const string lvlName = "Level";
            
            LdtkJson project = TestJsonLoader.DeserializeProject();
            
            Level level = project.UnityWorlds.First().Levels.FirstOrDefault();
            Assert.NotNull(level, "null level");

            //LayerInstance layer = level.LayerInstances.FirstOrDefault(p => p.IsIntGridLayer);
            //Assert.NotNull(layer);
            
            Rect levelBounds = level.UnityWorldSpaceBounds(WorldLayout.Free, (int)16);

            
            //Debug.Log(levelBounds);
        }
    }
}
