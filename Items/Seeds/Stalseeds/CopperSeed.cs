using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace ycPlants.Items.Seeds.Stalseeds
{
    class CopperSeed : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Actually worth silver! \n(Grows upside down on copper ore)");
        }

        public override void SetDefaults()
        {
            item.height = 18;
            item.width = 20;
            item.autoReuse = true;
            item.useTurn = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 15;
            item.useTime = 10;
            item.maxStack = 99;
            item.consumable = true;
            item.placeStyle = 0;
            item.createTile = TileType<Tiles.Plants.Stalactites.CopperPlant>();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CopperOre, 3);
            recipe.AddIngredient(ItemID.StoneBlock, 2);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
