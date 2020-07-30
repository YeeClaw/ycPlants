using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ycPlants.Items.Seeds
{
    public class Stardust : ModItem
    {
        public override void SetStaticDefaults()
        {
            // item description
            Tooltip.SetDefault("A seed from the heavens!");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 16;
            item.createTile = mod.TileType("StarPlant");
        }
    }
}
