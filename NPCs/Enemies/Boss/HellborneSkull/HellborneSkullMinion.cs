using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Bestiary;

namespace Creaturia.NPCs.Enemies.Boss.HellborneSkull
{

	public class HellborneSkullMinion : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hellborne Skull Minion");
		}

		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			NPC.width = 34;
			NPC.height = 66;
			NPC.damage = 45;
			NPC.defense = 18;
			NPC.lifeMax = 1000;
			NPC.HitSound = SoundID.NPCHit22;
			NPC.DeathSound = SoundID.NPCDeath55;
			NPC.value = 60f;
			NPC.knockBackResist = 0.6f;
			NPC.aiStyle = 44;

			NPC.noGravity = true;
			NPC.noTileCollide = true;
			AIType = NPCID.FlyingFish;

		}
       

public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
{
    NPC.lifeMax = 1400;
    NPC.defense = 20;
}
    
public override void AI()
{
    Lighting.AddLight(NPC.Center, Color.BlueViolet.ToVector3() * 2f);
    if (!NPC.AnyNPCs(ModContent.NPCType<TheHellborneSkull>()))
    {
        NPC.active = false;
    }

}
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			database.Entries.Remove(bestiaryEntry);
		}
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
{
    target.AddBuff(BuffID.ShadowFlame, 200);

}
public TheHellborneSkull Boss
{
    get
    {
        return (TheHellborneSkull)Main.npc[(int)NPC.ai[0]].ModNPC; // I wrote this like 2 years ago ok
    }
}



}	
}
