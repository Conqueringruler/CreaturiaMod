using Creaturia.NPCs.Enemies.Boss.FishBosses;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Creaturia.NPCs.Creatures;

namespace Creaturia.Items
{
	public class SummonFallen : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Summon Fallen Fairy"); 
			// Tooltip.SetDefault("'Hey, this is for the devs only!'");
		}

		public override void SetDefaults()
		{

			Item.width = 20;
			Item.height = 18;
			Item.value = 840;
			Item.rare = ItemRarityID.Pink;
			Item.maxStack = 999;
            Item.useAnimation = 5;
            Item.useTime = 5;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item1;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
			

        }
        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                // If the player using the item is the client
                // (explicitely excluded serverside here)
                

                int npctype = ModContent.NPCType<FallenPixie>();
                //NPC.NewNPC(player.GetSource_FromAI(), (int)player.Right.X, (int)player.Right.Y, ModContent.NPCType<FallenPixie>(), 0, player.whoAmI);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    // If the player is not in multiplayer, spawn directly
                    NPC.SpawnOnPlayer(player.whoAmI, npctype);
                }
                else
                {
                     //If the player is in multiplayer, request a spawn
                     //This will only work if NPCID.Sets.MPAllowedEnemies[type] is true, which we set in MinionBossBody
                    NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: npctype);
                }
            }

            return true;
        }
    }
}