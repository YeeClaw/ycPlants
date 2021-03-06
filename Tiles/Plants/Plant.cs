﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.Enums;

namespace ycPlants.Tiles.Plants
{
    public enum PlantStage : byte
    {
        Planted,
        Growing,
        Grown
    }

    public class Plant : ModTile
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
        }

        public override void RandomUpdate(int i, int j)
        {
            Tile btile = Framing.GetTileSafely(i, j);
            Tile ttile = Framing.GetTileSafely(i, (j - 1));
            PlantStage stage = GetStage(i, j);

            if (stage != PlantStage.Grown)
            {
                if (btile.frameY != 0)
                {
                    btile.frameX += FrameWidth;
                    ttile.frameX += FrameWidth;
                }
            }

            //If in multiplayer, sync the frame change
            if (Main.netMode != NetmodeID.SinglePlayer)
                NetMessage.SendTileSquare(-1, i - 1, j - 1, 3);
        }

        // uses nearest player to the crop instead of local player for multiplayer support
        private static float MultiCompat(int tileX, int tileY)
        {
            float shortestDistance = float.MaxValue;

            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player player = Main.player[i];

                if (player.active && !player.dead && player.HeldItem.type == ItemType<Items.Tools.CelestialSickle>() && player.itemAnimation > 0)
                {
                    float currentDistance = player.DistanceSQ(new Point16(tileX, tileY + 1).ToWorldCoordinates(8, 8));
                    if (currentDistance < shortestDistance)
                    {
                        shortestDistance = currentDistance;
                    }
                }
            }
            return shortestDistance;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            float nearestB = MultiCompat(i, j);
            float nearestT = MultiCompat(i, j - 1);

            // doesn't use GetStage due to coordinate complications
            PlantStage stage = (PlantStage)(frameX / FrameWidth);

            if (nearestB <= 84 * 84 && nearestT <= 84 * 84 && stage == PlantStage.Grown)
            {
                Item.NewItem(i * 16, j * 16, 16, 32, ItemID.FallenStar, 3);
                Item.NewItem(i * 16, j * 16, 16, 32, ItemType<Items.Seeds.Misc.Stardust>(), 2);
            }
            else if (Main.rand.NextBool())
            {
                Item.NewItem(i * 16, j * 16, 16, 32, ItemType<Items.Seeds.Misc.Stardust>(), 1);
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