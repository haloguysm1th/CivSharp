﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CivSharp.Core;
using CivSharp.Systems;
using RLNET;
using RogueSharp;

namespace CivSharp.Tiles
{
    class Plains : Tile
    {
        public Plains(int x, int y, ICell cell) : 
            base(x, y, false, true, 0, 0, 0, null,
                GraphicsItem.Plains, Colors.GrassPrimiary,Colors.GrassSecondary , cell)
        {
        }
    }
}
