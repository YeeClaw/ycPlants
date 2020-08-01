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
    // enum for different stages of plant growth
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
            Main.tileCut[Type] = true;
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
            dustType = DustType<Sparkle>();
            TileObjectData.newTile.AnchorValidTiles = new int[]
            {
                TileID.Cloud,
                TileID.RainCloud,
                TileID.SnowCloud
            };
            TileObjectData.addTile(Type);

            // map visibility
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Star crop");

            AddMapEntry(new Color(236, 247, 20), name);
        }

        public override void RandomUpdate(int i, int j)
        {
            Tile Btile = Framing.GetTileSafely(i, j); //Grabs specific tile instance for the bottom frame
            Tile Ttile = Framing.GetTileSafely(i, (j - 1)); // Only gets the top tile
            PlantStage stage = GetStage(i, j);

            if (stage != PlantStage.Grown) // Stops frame instance from going past the actual sprite
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

        // dust for the crop
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = Main.rand.Next(1, 3);
        }

        // changes direction depending on where the tile is placed in world coordinates
        public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
        {
            if (i % 2 == 1)
                spriteEffects = SpriteEffects.FlipHorizontally;
        }

        // adds Stardust and Fallen stars as drop items
        public override bool Drop(int i, int j)
        {
            PlantStage stage = GetStage(i, j);
            if (stage == PlantStage.Grown)
            {
                Item.NewItem(new Vector2(i, j).ToWorldCoordinates(), ItemType<Items.Seeds.Stardust>(), 2);
                Item.NewItem(new Vector2(i, j).ToWorldCoordinates(), ItemID.FallenStar);
            }

            return false;
        }

        // grabs plant stage
        private PlantStage GetStage(int i, int j)
        {
            Tile tile = Framing.GetTileSafely(i, j);
            return (PlantStage)(tile.frameX / FrameWidth);
        }
    }
}
