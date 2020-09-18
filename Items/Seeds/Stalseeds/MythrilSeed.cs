using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace ycPlants.Items.Seeds
{
    class MythrilSeed : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("You shouldn't use this to protect yourself! \n(Grows upside down on mythril ore)");
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
            item.createTile = TileType<Tiles.Plants.MythrilPlant>();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MythrilOre, 5);
            recipe.AddIngredient(ItemID.StoneBlock, 2);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
