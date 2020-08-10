using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using Terraria.ModLoader;

namespace ycPlants.Items.Tools
{
    public class CelestialSickle : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Used for harvesting many celestial crops.");
        }

        public override void SetDefaults()
        {
            item.damage = 9;
            item.melee = true;
            item.width = 40;
            item.height = 40;
            item.knockBack = 2.25f;
            item.useTime = 24;
            item.pick = 1;
            item.useAnimation = 24;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Sickle, 1);
            recipe.AddIngredient(ItemID.ManaCrystal, 2);
            recipe.AddIngredient(ItemID.FallenStar, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
