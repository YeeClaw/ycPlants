using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace ycPlants.Items.Seeds
{
    public class Stardust : ModItem
    {
        public override void SetStaticDefaults()
        {
            // item description
            Tooltip.SetDefault("A seed from the heavens. \n(Only harvest at night!)");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 16;
            item.autoReuse = true;
            item.useTurn = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 15;
            item.useTime = 10;
            item.maxStack = 99;
            item.consumable = true;
            item.placeStyle = 0;
            item.createTile = TileType<Tiles.Plants.StarPlant>();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FallenStar, 3);
            recipe.AddIngredient(ItemID.Cloud, 1);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
