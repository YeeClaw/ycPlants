using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using ycPlants.Dust;

namespace ycPlants.Tiles.Plants.Misc
{
    
    public class StarPlant : Plant
    {
        private const int FrameWidth = 18;
        public override void SetDefaults()
        {

            base.SetDefaults();
            
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
            name.SetDefault("Star Crop");

            AddMapEntry(new Color(236, 247, 20), name);
        }
    }
}
