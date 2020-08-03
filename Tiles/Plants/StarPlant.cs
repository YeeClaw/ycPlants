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
    public enum PlantStage : byte
    {
        Planted,
        Growing,
        Grown
    }

    public class StarPlant : ModTile
    {
        private const int FrameWidth = 18;
        public override void SetDefaults()
        {
            // plant properties
            Main.tileSolid[Type] = false;
            Main.tileLavaDeath[Type] = true;
            Main.tileWaterDeath[Type] = true;
            Main.tileBlockLight[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoFail[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileCut[Type] = true;

            // adds tile
            TileObjectData.newTile.Width = 1;
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.Origin = new Point16(0, 1);
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.CoordinateHeights = new int[2]
            {
                16,
                16
            };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.LavaDeath = true;
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.newTile.StyleHorizontal = true;
            dustType = DustType<Sparkle>();
            TileObjectData.newTile.AnchorValidTiles = new int[]
            {
                TileID.Cloud,
                TileID.RainCloud,
                TileID.SnowCloud
            };
            TileObjectData.addTile(Type);

            dustType = DustType<Sparkle>();
            soundType = SoundID.Grass;
            soundStyle = 1;

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Star crop");

            AddMapEntry(new Color(236, 247, 20), name);
        }

        public override void RandomUpdate(int i, int j)
        {
            Tile Btile = Framing.GetTileSafely(i, j);
            Tile Ttile = Framing.GetTileSafely(i, (j - 1));
            PlantStage stage = GetStage(i, j);

            if (stage != PlantStage.Grown)
            {
                if (Btile.frameY != 0)
                {
                    Btile.frameX += FrameWidth;
                    Ttile.frameX += FrameWidth;
                }
            }

            //If in multiplayer, sync the frame change
            if (Main.netMode != NetmodeID.SinglePlayer)
                NetMessage.SendTileSquare(-1, i, j, 1);
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            // doesn't use GetStage due to coordinate complications
            PlantStage stage = (PlantStage)(frameX / FrameWidth);

            if (Main.LocalPlayer.HeldItem.type == ItemID.Sickle && stage == PlantStage.Grown)
            {
                Item.NewItem(i * 16, j * 16, 16, 32, ItemType<Items.Seeds.Stardust>(), 2);
                Item.NewItem(i * 16, j * 16, 16, 32, ItemID.FallenStar, 1);
            }
            else
            {
                Random rnd = new Random();
                int stardrop = rnd.Next(1, 11);

                if (stardrop % 2 == 0)
                {
                    Item.NewItem(i * 16, j * 16, 16, 32, ItemType<Items.Seeds.Stardust>(), 1);
                }
            }
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.5f;
            g = 0.42f;
            b = 0.05f;
        }

        public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
        {
            if (i % 2 == 1)
                spriteEffects = SpriteEffects.FlipHorizontally;
        }

        private PlantStage GetStage(int i, int j)
        {
            Tile tile = Framing.GetTileSafely(i, j);
            return (PlantStage)(tile.frameX / FrameWidth);
        }
    }
}
