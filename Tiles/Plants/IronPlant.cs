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
    public class IronPlant : ModTile
    {
        private const int FrameWidth = 18;

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
            TileObjectData.newTile.AnchorValidTiles = new int[]
            {
                TileID.Iron
            };
            TileObjectData.addTile(Type);

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Iron Stalactite");

            AddMapEntry(new Color(189, 130, 81), name);
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

            if (Main.netMode != NetmodeID.SinglePlayer)
                NetMessage.SendTileSquare(-1, i - 1, j - 1, 3);
        }

        float MultiCompat(int tileX, int tileY)
        {
            float shortestDistance = float.MaxValue;

            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player player = Main.player[i];

                if (player.active && !player.dead && player.HeldItem.pick > 0 && player.itemAnimation > 0)
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

            PlantStage stage = (PlantStage)(frameX / FrameWidth);

            if (nearestB <= 106 * 106 && nearestT <= 106 * 106 && stage == PlantStage.Grown)
            {
                Item.NewItem(i * 16, j * 16, 16, 32, ItemID.IronOre, 3);
                Item.NewItem(i * 16, j * 16, 16, 32, ItemType<Items.Seeds.IronSeed>(), 2);
            }
            else if (Main.rand.NextBool())
            {
                Item.NewItem(i * 16, j * 16, 16, 32, ItemType<Items.Seeds.IronSeed>(), 1);
            }

        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.4f;
            g = 0.32f;
            b = 0.2f;
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
