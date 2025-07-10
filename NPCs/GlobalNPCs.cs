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
using Creaturia.NPCs.Enemies.Boss.HellborneSkull;
using Creaturia.NPCs.Enemies.GnomeEvent;
using Creaturia.Common;
using Creaturia.Common.Systems;
using Creaturia.Items;
using Creaturia.Items.Weapon;
using Creaturia.Configs;

namespace Creaturia.NPCs
{
    public class GlobalNPCs : GlobalNPC
    {

        public int meleehit;
        int electricityDamageMult = 1;

      
        public override bool InstancePerEntity => true;

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
           if (npc.type == NPCID.DoctorBones)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientDartGunPieces>(), 4, 1, 1));
            }
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            
            if (!Main.hardMode)
            {
                electricityDamageMult = 1; // Might get rid of this idk why I even had it scale in the first place
            }                               // Not going to delete though cause I have plans i cannot share witch you rn cause the haters will sabotage me
            else if (Main.hardMode && !NPC.downedGolemBoss)
            {
          //      electricityDamageMult = 2;
            }
            else if (Main.hardMode && NPC.downedGolemBoss)
            {
           //     electricityDamageMult = 4;
            }
            if (npc.type is not NPCID.GreenJellyfish or NPCID.BlueJellyfish or NPCID.PinkJellyfish or NPCID.BloodJelly or NPCID.MartianDrone or NPCID.MartianEngineer or NPCID.MartianOfficer
                or NPCID.MartianProbe or NPCID.MartianSaucer or NPCID.MartianSaucerCannon or NPCID.MartianSaucerTurret or NPCID.MartianWalker or NPCID.GigaZapper or NPCID.BrainScrambler
                or NPCID.RayGunner)
            {

                if (!ModLoader.TryGetMod("CalamityMod", out Mod Calamity)) // Calamity does their own electricity damage, so I don't want mine to stack on top (even though mine is better)
                {

                    if (npc.HasBuff(BuffID.Electrified)) //  ElectrifiedDamageValue = (int)(5 * (npc.velocity.X == 0 ? 1 : 4) * electricityDamageMult)
                    {
                        if (npc.lifeRegen > 0)
                        {
                            npc.lifeRegen = 0;
                        }
                        npc.lifeRegen -= (int)(2 * ((MathF.Abs(npc.velocity.X) + MathF.Abs(npc.velocity.Y)) > 0.5f ? 12 : 1) * electricityDamageMult);
                        //damage = (int)(1 * ((MathF.Abs(npc.velocity.X) + MathF.Abs(npc.velocity.Y)) > 0.5f ? 4 : 1) * electricityDamageMult);

                        if (Main.rand.NextBool(3))
                        {
                            Dust dust = Dust.NewDustDirect(npc.position + new Vector2(Main.rand.Next(-8, 8), Main.rand.Next(-8, 8)), npc.width, npc.height, DustID.Electric, Main.rand.Next(-4, 4), Main.rand.Next(-4, 4), 0, new Color(255, 0, 133), 0.4f);
                            dust.noGravity = true;
                            dust.fadeIn = 1f;
                            ;
                        }

                    }
                }
            }
        }
        float ProjectileWhoAmI;


        public override void AI(NPC npc)
        {

         
            

            if (npc.type is NPCID.UndeadViking or NPCID.ArmoredViking)
            {
                if (npc.target != null)
                {
                    if (npc.life < npc.lifeMax)
                    {
                        if (Main.player[npc.target].npcTypeNoAggro[npc.type])
                        {
                            Main.player[npc.target].npcTypeNoAggro[npc.type] = false;
                        }
                    }
                //  if (  Main.player[npc.target].npcTypeNoAggro[npc.type];
                }
            }

            if (npc.type == NPCID.QueenSlimeBoss)
                {
              //     meleehit++;
                    if (meleehit > 18)
                    {
                   //     Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, npc.velocity, ModContent.ProjectileType<QueenSlimeHit>(), 45, 1, Main.myPlayer);
                        meleehit = 0;
                    }
                    
                }
            if (npc.type == NPCID.HallowBoss)
            {
            //    meleehit++;
                if (meleehit > 18)
                {
                  //  Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, npc.velocity, ModContent.ProjectileType<EmpressofLightHit>(), 65, 1, Main.myPlayer);
                    meleehit = 0;
                }

            }
            if (npc.type == NPCID.EaterofWorldsBody)
            {
            //    meleehit++;
                if (meleehit > 18)
                {
                   // Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, npc.velocity, ModContent.ProjectileType<EaterofWorldsHit>(), 28, 1, Main.myPlayer);
                    meleehit = 0;
                }

            }
            if (npc.type == NPCID.VileSpitEaterOfWorlds || npc.type == NPCID.VileSpit)
            {
              //  meleehit++;
                if (meleehit > 18) // To Do: need to make the NPC check for the projectile still being active, and if not spawning another one
                {
               //     int npcMeleeProj = Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, npc.velocity, ModContent.ProjectileType<VileSpitHit>(), 25, 1, Main.myPlayer);
                  
                    meleehit = 0;
                }

            }
            if (npc.type == NPCID.BrainofCthulhu)
            {
             //   meleehit++;
                if (meleehit > 18)
                {
                 //   Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, npc.velocity, ModContent.ProjectileType<BrainofCthulhuHit>(), 28, 1, Main.myPlayer);
                    meleehit = 0;
                }

            }
            if (npc.type == NPCID.Plantera)
            {
           //     meleehit++;
                if (meleehit > 18)
                {
                 //   Projectile.NewProjectile(npc.GetSource_FromAI(), npc.Center, npc.velocity, ModContent.ProjectileType<PlanteraHit>(), 60, 1, Main.myPlayer);
                    meleehit = 0;
                }

            }
        }


        public override void OnCaughtBy(NPC npc, Player player, Item item, bool failed)
        {
            base.OnCaughtBy(npc, player, item, failed);
            if (npc.type == NPCID.Duck)
            {
                npc.Transform(NPCType<DuckMaiden>());
            }
        }
       
        public override void SetDefaults(NPC npc)
        {

            if (npc.type == NPCID.Cyborg)
            {
                npc.defense = 20;
                
                npc.lifeMax = 550;
            }
            if (npc.type == NPCID.WitchDoctor)
            {
                npc.lifeMax = 300;
            }
            if (npc.type == NPCID.CorruptBunny || npc.type == NPCID.Corruptor || npc.type == NPCID.EaterofSouls || npc.type == NPCID.EaterofWorldsBody || npc.type == NPCID.EaterofWorldsHead || npc.type == NPCID.EaterofWorldsTail || npc.type == NPCID.DevourerBody || npc.type == NPCID.CorruptSlime || npc.type == NPCID.Slimer || npc.type == NPCID.Slimeling || npc.type == NPCID.DarkMummy || npc.type == NPCID.CorruptGoldfish || npc.type == NPCID.Crimslime || npc.type == NPCID.CrimsonBunny || npc.type == NPCID.CrimsonGoldfish || npc.type == NPCID.Crimera || npc.type == NPCID.BigCrimera || npc.type == NPCID.CrimsonAxe || npc.type == NPCID.BigCrimslime || npc.type == NPCID.Herpling || npc.type == NPCID.Creeper || npc.type == NPCID.BrainofCthulhu || npc.type == NPCID.BloodMummy || npc.type == NPCID.BloodJelly || npc.type == NPCID.BloodFeeder || npc.type == NPCID.BloodCrawler || npc.type == NPCID.BloodCrawlerWall || npc.type == NPCID.FaceMonster || npc.type == NPCID.FloatyGross || npc.type == NPCID.IchorSticker || npc.type == NPCID.BloodCrawlerWall || npc.type == NPCID.DesertGhoulCrimson || npc.type == NPCID.DesertGhoulCorruption || npc.type == NPCID.CursedHammer || npc.type == NPCID.SeekerBody || npc.type == NPCID.Clinger || npc.type == NPCID.BigMimicCorruption || npc.type == NPCID.BigMimicCrimson)
            {
                npc.dontTakeDamageFromHostiles = false;
            }

        }
        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            //base.OnHitByItem(npc, player, item, damage, knockback, crit);
            if (npc.type == ModContent.NPCType<TheHellborneSkull>() || npc.type == ModContent.NPCType<HellborneSkullMinion>() || npc.type == ModContent.NPCType<HellborneGuardian>())
            {
                if (item.type == ItemID.TheHorsemansBlade || item.type == ItemID.ChristmasTreeSword)
                {
                    //damage *= 2;
                    modifiers.FinalDamage *= 2; // NEED TO TEST!
                }
            }
            else
            {
                item.damage = item.OriginalDamage;
            }
        }
       
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {

            
            if (npc.type == ModContent.NPCType<TheHellborneSkull>() || npc.type == ModContent.NPCType<HellborneSkullMinion>() || npc.type == ModContent.NPCType<HellborneGuardian>())
            {
                if (projectile.type == ProjectileID.CandyCorn || projectile.type == ProjectileID.JackOLantern || projectile.type == ProjectileID.FlamingJack ||
                projectile.type == ProjectileID.Stake || projectile.type == ProjectileID.Bat || projectile.type == ProjectileID.PineNeedleFriendly || projectile.type == ProjectileID.PineNeedleHostile 
                || projectile.type == ProjectileID.Raven || projectile.type == ProjectileID.OrnamentFriendly || projectile.type == ProjectileID.OrnamentStar
                || projectile.type == ProjectileID.ClusterSnowmanRocketI || projectile.type == ProjectileID.RocketSnowmanI || projectile.type == ProjectileID.RocketSnowmanIII
                || projectile.type == ProjectileID.MiniNukeSnowmanRocketI || projectile.type == ProjectileID.ScytheWhip || projectile.type == ProjectileID.ScytheWhipProj || projectile.type == ProjectileID.Blizzard)
                {
                    //projectile.damage = 0;
                    
                }
            }  // This is done in GlobalProjectiles now since it refused to work correctly here.
        }
        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {

            // if (projectile.type ==  && npc.type == NPCID.Werewolf)
            //  {
            //      npc.life -= 200; I guess silver bullets and musket balls are the same projectile.
            //  }
            if (!npc.SpawnedFromStatue)
            {
                if (npc.type == NPCID.Bunny || npc.type == NPCID.Duck || npc.type == NPCID.DuckWhite || npc.type == NPCID.BunnySlimed || npc.type == NPCID.BunnyXmas || npc.type == NPCID.Squirrel ||
                npc.type == NPCID.Penguin || npc.type == NPCID.PenguinBlack)
                {
                    if (npc.active)
                    {
                        if (!Main.bloodMoon)
                            if (Main.rand.NextBool(6000))
                            {
                                npc.Transform(NPCType<SkinWalker>());
                                npc.netUpdate = true;
                            }
                        if (Main.bloodMoon)
                        {
                            if (Main.rand.NextBool(1000))
                            {
                                npc.Transform(NPCType<SkinWalker>());
                                npc.netUpdate = true;
                            }
                        }
                    }

                }
            }
            if (!npc.SpawnedFromStatue)
                {
            if (npc.type == NPCID.Bunny || npc.type == NPCID.Duck || npc.type == NPCID.DuckWhite || npc.type == NPCID.Clown || npc.type == NPCID.BunnySlimed || npc.type == NPCID.BunnyXmas || 
                npc.type == NPCID.Squirrel || npc.type == NPCID.Penguin || npc.type == NPCID.PenguinBlack || npc.type == NPCID.Unicorn || npc.type == NPCID.WalkingAntlion || npc.type == NPCID.Owl)
            {
               if (npc.active)
                    {
                    if (Main.rand.NextBool(35000) && npc.active)
                    {
                        npc.Transform(NPCType<SkinWalker>());
                        
                            
                            npc.netUpdate = true;
                    }
                    if (Main.bloodMoon && npc.active)
                    {
                        if (Main.rand.NextBool(18000))
                        {
                            npc.Transform(NPCType<SkinWalker>());
                            npc.netUpdate = true;
                        }
                    }
                }
            }
            }
        }
        public bool GolemFists = ModContent.GetInstance<CreaturiaSettings>().GolemFists.Contains("Enabled");

        public bool NinjaEnabled = ModContent.GetInstance<CreaturiaSettings>().Ninja.Contains("Enabled");
        public override void OnKill(NPC npc)
        {
          
            if (npc.type is NPCID.BoneThrowingSkeleton2 or NPCID.BigHeadacheSkeleton or NPCID.SmallHeadacheSkeleton or NPCID.HeadacheSkeleton)
            {
                if (Main.rand.NextBool(3))
                {
                    Item.NewItem(npc.GetSource_Death(), new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), ModContent.ItemType<AncientStoneShortsword>(), 1);
                }
            }
            if (!npc.SpawnedFromStatue)
            {
                if (npc.type == NPCID.Bunny || npc.type == NPCID.Duck || npc.type == NPCID.DuckWhite || npc.type == NPCID.Clown || npc.type == NPCID.BunnySlimed || npc.type == NPCID.BunnyXmas
                    || npc.type == NPCID.Squirrel || npc.type == NPCID.Penguin || npc.type == NPCID.PenguinBlack || npc.type == NPCID.Unicorn || npc.type == NPCID.WalkingAntlion)
                {
                    if (npc.active)
                    {
                    if (Main.rand.NextBool(10000))
                    {
                        NPC.NewNPC(npc.GetSource_Death(), (int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<SkinWalker>());
                    }
                    if (Main.bloodMoon)
                    {
                        if (Main.rand.NextBool(4500))
                        {
                            npc.Transform(NPCType<SkinWalker>());
                            npc.netUpdate = true;
                        }
                    }
                }
            }
            }
            if (npc.type == NPCID.KingSlime && npc.AnyInteractions())
            {
                if (NinjaEnabled)
                {
                if (!NPC.AnyNPCs(ModContent.NPCType<Ninja>()))
                {
                    NPC.NewNPC(npc.GetSource_Death(), (int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<Ninja>());
                }
                }

        }
            if (npc.type == NPCID.GolemFistLeft && npc.AnyInteractions())
            {
                if (GolemFists)
                {
                NPC.NewNPC(npc.GetSource_Death(), (int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<GolemFistL>());
            }
        }

            if (npc.type == NPCID.GolemFistRight && npc.AnyInteractions())
            {
                if (GolemFists)
                {
                    NPC.NewNPC(npc.GetSource_Death(), (int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<GolemFistR>());
                }
            }
          if (npc.type is NPCID.GolemFistLeft or NPCID.GolemFistRight)
            {
                Main.BestiaryTracker.Kills.RegisterKill(npc);
            }
        }
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {/*
            if (npc.type == NPCID.Golem)
            {
                Color color = new Color(255, 255, 255, 0);
                SpriteEffects spriteEffects = SpriteEffects.None;
                Texture2D texture = Mod.Assets.Request<Texture2D>("NPCs/Enemies/golemhellbornecrest").Value;
                spriteBatch.Draw(texture, npc.Center - screenPos,
            npc.frame, color, npc.rotation, // reminder for later; the sprite is there, just under the other stuff
            new Vector2(TextureAssets.Npc[npc.type].Value.Width * 0.5f, TextureAssets.Npc[npc.type].Value.Height * 0.095f), npc.scale, spriteEffects, 0f);

                

            }
            */
            return base.PreDraw(npc, spriteBatch, screenPos, drawColor);
        }
      //  bool EnableHummingbirdBestiary = false;
        public override void SetBestiary(NPC npc, BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
        /*    
            if (npc.type == ModContent.NPCType<HummingBird1>())
            {
                if (bestiaryEntry.UIInfoProvider.GetEntryUICollectionInfo().UnlockState == BestiaryEntryUnlockState.CanShowDropsWithDropRates_4)
                {
                    EnableHummingbirdBestiary = true;
                }
            }
            if (npc.type == ModContent.NPCType<HummingbirdBestiary>() && EnableHummingbirdBestiary)
            {
                bestiaryEntry.UIInfoProvider
            } */
        }
        /*  public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
          {


             if (npc.type == NPCID.Golem)
              {
                  Color color = new Color(255, 255, 255, 0);
                  SpriteEffects spriteEffects = SpriteEffects.None;
                  Texture2D texture = Mod.Assets.Request<Texture2D>("NPCs/Enemies/golemhellbornecrest").Value;
                  spriteBatch.Draw(texture, npc.Center - screenPos,
              npc.frame, color, npc.rotation, // reminder for later; the sprite is there, just under the other stuff
              new Vector2(TextureAssets.Npc[npc.type].Value.Width * 0.5f, TextureAssets.Npc[npc.type].Value.Height * 0.098f), npc.scale, spriteEffects, 0f);



              } 
          } */


    }
}
        



