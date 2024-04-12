using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.IO;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Creaturia.NPCs.Town;
using Creaturia.NPCs.Enemies;
using Creaturia.NPCs.Enemies.Boss;
using Creaturia.NPCs.Creatures;
using Creaturia.Projectiles.EnemyMelee;
using Terraria.Audio;
using Terraria.ModLoader.Utilities;
using static Terraria.ModLoader.ModContent;
using static Terraria.ModLoader.PlayerDrawLayer;
using Creaturia.Projectiles;
using Creaturia;
using Terraria.GameContent.ItemDropRules;
using Creaturia.NPCs.Enemies.Boss.FishBosses;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using ReLogic.Content;
using Terraria.GameContent.Personalities;


namespace Creaturia.NPCs
{
    public class GlobalTownNPCs : GlobalNPC
    {

        public override void SetStaticDefaults()
        {
            int Ninja = ModContent.NPCType<Ninja>();
            var guideHappiness = NPCHappiness.Get(NPCID.Guide); // Get the key into The Guide's happiness
            var merchantHappines = NPCHappiness.Get(NPCID.Merchant);
            var NurseHappiness = NPCHappiness.Get(NPCID.Nurse);
            var DemoHappiness = NPCHappiness.Get(NPCID.Demolitionist);
            var dyeHappiness = NPCHappiness.Get(NPCID.DyeTrader);
            var anglerHappiness = NPCHappiness.Get(NPCID.Angler);
            var ZooHappiness = NPCHappiness.Get(NPCID.BestiaryGirl);
            var DryadHappiness = NPCHappiness.Get(NPCID.Dryad);
            var PainterHappiness = NPCHappiness.Get(NPCID.Painter);
            var GolferHappiness = NPCHappiness.Get(NPCID.Golfer);
            var armsHappiness = NPCHappiness.Get(NPCID.ArmsDealer);
            var tavernHappiness = NPCHappiness.Get(NPCID.DD2Bartender);
            var stylistHappiness = NPCHappiness.Get(NPCID.Stylist);
            var goblinHappiness = NPCHappiness.Get(NPCID.GoblinTinkerer); // I am NOT going to add happiness for the Ninja for all of them (since in vanilla they each have happiness for only a few others),
            var witchHappiness = NPCHappiness.Get(NPCID.WitchDoctor);     // but I'm having it all here incase I change vanilla happiness or add more NPCs.
            var clothierHappiness = NPCHappiness.Get(NPCID.Clothier);
            var mechanicHappiness = NPCHappiness.Get(NPCID.Mechanic);
            var partyHappiness = NPCHappiness.Get(NPCID.PartyGirl);

            // hardmode
            var WizardHappiness = NPCHappiness.Get(NPCID.Wizard);
            var TaxHappiness = NPCHappiness.Get(NPCID.TaxCollector);
            var TruffleHappiness = NPCHappiness.Get(NPCID.Truffle);
            var PirateHappiness = NPCHappiness.Get(NPCID.Pirate);
            var SteampunkerHappiness = NPCHappiness.Get(NPCID.Steampunker);
            var CyborgHappiness = NPCHappiness.Get(NPCID.Cyborg);
            var SantaHappiness = NPCHappiness.Get(NPCID.SantaClaus);
            var PrincessHappiness = NPCHappiness.Get(NPCID.Princess);

            guideHappiness.SetNPCAffection(Ninja, AffectionLevel.Dislike);
            dyeHappiness.SetNPCAffection(Ninja, AffectionLevel.Like);
        }


        public override void GetChat(NPC npc, ref string chat)
        {
            switch (npc.type) // This is my reminder to START USING SWITCH. QUIT DOING WALLS OF BULLSHIT IF STATEMENTS
            {
                case NPCID.DyeTrader:
                    {
                        if (NPC.AnyNPCs(NPCType<Ninja>()))
                        {
                            if (Main.rand.NextBool(25))
                            {
                                chat = "The Ninja's people are not very similar to mine, yet we are both alike; here in this foreign land we may as well have been from the same place.";
                            }
                            if (Main.rand.NextBool(25))
                            {
                                chat = "I enjoy the Ninja's presence. It reminds me I am not alone as a foreigner of this land.";
                            }
                        }
                    }
                    break;
                case NPCID.Guide:
                    {
                        if (Main.hardMode && NPC.downedMechBossAny && !NPC.downedMoonlord)
                        {
                            if (Main.rand.NextBool(18))
                            {
                                chat = "Long ago, I heard a tale of the so-called 'biome keys' of the Dungeon being crafted from organs of great lake beasts. Perhaps that will, in some way, help you in your journey?";
                            }
                        }
                        if (Main.hardMode && NPC.downedPlantBoss && !NPC.downedGolemBoss)
                        {
                            if (Main.rand.NextBool(8))
                            {
                                chat = "If you're having trouble with the Lihzahrd's Golem, defeating the ancient spirits it stole power from may help even the odds.";
                            }
                        }
                    }
                    break;
            }
        }


    }
}