using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;


namespace ycPlants.Tiles.StarPlants
{
    public class StarPlants : ModTile
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
