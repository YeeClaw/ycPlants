using System;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.Enums;
using ycPlants.Dust;

namespace ycPlants.Tiles.Plants
{
    public class LeadPlant : ModTile
    {
        private const int frameWidth = 18;

        public override void SetDefaults()
        {
            // plant properties
            Main.tileSolid[Type] = false;
            Main.tileLavaDeath[Type] = false;
            Main.tileWaterDeath[Type] = false;
            Main.tileBlockLight[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoFail[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileCut[Type] = false;

            // adds tile
            TileObjectData.newTile.Width = 1;
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.Origin = new Point16(0, 1);
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.CoordinateHeights = new int[2]
            {
                16,
                16
            };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.DrawYOffset = -2;
            dustType = DustType<Sparkle>();
            TileObjectData.newTile.AnchorValidTiles = new int[]
            {
                TileID.Lead
            };
            TileObjectData.addTile(Type);

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Lead Stalactite");

            AddMapEntry(new Color(49, 70, 84), name);
        }

        public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
        {
            if (i % 2 == 1)
                spriteEffects = SpriteEffects.FlipHorizontally;
        }

    }
}
