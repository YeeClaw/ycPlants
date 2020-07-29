using System;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace ycPlants.Tiles.Plants
{
    public class s1StarPlant : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileLavaDeath[Type] = true;
            Main.tileWaterDeath[Type] = true;
            Main.tileBlockLight[Type] = false;
        }
    }
}
