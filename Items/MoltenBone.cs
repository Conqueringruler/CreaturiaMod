using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Creaturia.Items
{
	public class MoltenBone : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Molten Bone"); 
			// Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{

			Item.width = 20;
			Item.height = 18;
            Item.value = Item.sellPrice(0, 0, 8, 40);
            Item.rare = ItemRarityID.Pink;
			Item.material = true;
			Item.maxStack = 999;

		}
		
	}
}