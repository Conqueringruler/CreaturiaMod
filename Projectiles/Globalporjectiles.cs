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
using Terraria.ModLoader.Assets;
using Creaturia.NPCs.Enemies.Boss.FishBosses;
using Creaturia.NPCs.Enemies.Boss.HellborneSkull;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Creaturia.NPCs.Creatures;

namespace Creaturia.Projectiles
{
    public class Globalporjectiles : GlobalProjectile
    {
        public bool OnStart = true;
        public bool EmpoweredByPumpking = false;
        public bool EmpoweredByFrostQueen = false;

        public override bool InstancePerEntity => true; // This is a god-awful solution. I need to figure out how to do this in GolemFist alone

     /*   int[] HallowedList = new int[] {NPCID.Unicorn, NPCID.Pixie, NPCID.SandsharkHallow, NPCID.HallowBoss, NPCID.QueenSlimeBoss, NPCID.QueenSlimeMinionBlue,
                                        NPCID.QueenSlimeMinionPink, NPCID.Gastropod, NPCID.LightMummy, NPCID.RainbowSlime, NPCID.FlyingFish,
                                    NPCID.EmpressButterfly, NPCID.DesertGhoulHallow,
                                    NPCID.PigronHallow, NPCID.BigMimicHallow, NPCID.EnchantedSword, NPCID.IlluminantSlime, NPCID.IlluminantBat, NPCID.ChaosElemental, ModContent.NPCType<FallenPixie>(),
                                    ModContent.NPCType<GreatPixie>(), ModContent.NPCType<RainbowFish>()}; */
     // ^^^^ Was going to make it so thrown waters would heal the enemies of that type, so stuff like Holy Water would heal Hallow enemies. 

        public override void SetDefaults(Projectile projectile)
        {
            if (projectile.type == ProjectileID.GolemFist)
            {
                ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
                ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            }
        }
        public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
        {
            if (projectile.type == ProjectileID.HolyWater)
            {
                
                if (NPC.AnyNPCs(ModContent.NPCType<FallenPixie>()))
                {
                    for (int i = 0; i < Main.npc.Length; i++)
                    {
                        NPC CheckIfFairy = Main.npc[i];
                        if (CheckIfFairy.type == ModContent.NPCType<FallenPixie>()) // Probably doesn't work because this can't all run in one tick before the projectile's death. Idk though
                        {
                            if (Vector2.Distance(CheckIfFairy.Center, projectile.Center) < 6f)
                            {
                                projectile.netUpdate = true;
                                if (Main.netMode == NetmodeID.SinglePlayer)
                                {
                                    CheckIfFairy.lifeMax = 15;
                                     CheckIfFairy.life = 15;
                                    CheckIfFairy.defense = 10;
                                }
                                  //  if (Main.netMode == NetmodeID.Server)
                               // {
                                    ModPacket packet = Mod.GetPacket(); // use this instead of other
                                    packet.Write((byte)Creaturia.MessageType.FallenPixieMsg); // id
                                    packet.Write(CheckIfFairy.whoAmI); // NPC identity
                                    packet.Write((bool)true);
                                   // packet.Write((byte)15);
                                    //packet.Write((bool)true);
                                    packet.Send();
                                    // WishesChosen
                                                           //CheckIfFairy.netUpdate = true; 
                               // }
                                
                            }
                        }
                    }
                }
            }
            return base.OnTileCollide(projectile, oldVelocity);
        }
        public override void AI(Projectile projectile)
        {
            if (projectile.type == ProjectileID.CursedDartFlame)
            {
                if (projectile.owner.GetType() == ModContent.NPCType<DunklerFish>().GetType())
                {
                    projectile.hostile = true;
                    projectile.friendly = false;
                    projectile.scale = 1.5f;
                }
            }
            if (projectile.type == ProjectileID.ThornHook)
            {
                if (projectile.velocity == Vector2.Zero)
                {
                    if (Math.Abs(Main.player[Main.myPlayer].velocity.X) > 0 && Math.Abs(Main.player[Main.myPlayer].velocity.Y) > 0)
                    {
                        if (!Main.player[Main.myPlayer].HasBuff(BuffID.WeaponImbueConfetti) || !Main.player[Main.myPlayer].HasBuff(BuffID.WeaponImbueCursedFlames)
                            ||! Main.player[Main.myPlayer].HasBuff(BuffID.WeaponImbueFire) || !Main.player[Main.myPlayer].HasBuff(BuffID.WeaponImbueGold) ||
                            !Main.player[Main.myPlayer].HasBuff(BuffID.WeaponImbueIchor) || !Main.player[Main.myPlayer].HasBuff(BuffID.WeaponImbueNanites) ||
                            !Main.player[Main.myPlayer].HasBuff(BuffID.WeaponImbuePoison) || !Main.player[Main.myPlayer].HasBuff(BuffID.WeaponImbueVenom))
                        {
                            Main.player[Main.myPlayer].AddBuff(BuffID.WeaponImbuePoison, 2); // Preferably I want it to be that enemies attacking users w/ thorn hooks get poisoned when hitting the player
                        }
                           
                    }

                    Vector2 position = projectile.Center;
                    if (Math.Abs(projectile.velocity.X) > 0 || Math.Abs(projectile.velocity.Y) > 0)
                    {
                        if (Main.rand.NextBool(2))
                        {
                            // Need to 
                            var dust = Dust.NewDustDirect(projectile.Center, projectile.width + Main.rand.Next(-5, 5), projectile.height + Main.rand.Next(-5, 5), DustID.Water, projectile.velocity.X, projectile.velocity.Y, 100, Color.Green, 1);
                            dust.velocity.Y /= 20;
                            dust.color = new Color(180, 180, 180);
                            dust.noGravity = true;
                        }

                    }
                }
            }
            
            if (projectile.type == ProjectileID.GolemFist)
            {


                if (OnStart == true)
                {
                    OnStart = false;

                    if (Main.rand.NextBool(5))
                    {
                        if (Main.rand.NextBool(2))
                        {
                            EmpoweredByPumpking = true;
                        }
                        else
                        {
                            EmpoweredByFrostQueen = true;
                        }
                    }

                }
                if (EmpoweredByPumpking)
                {
                    Lighting.AddLight(projectile.Center, Color.DarkOrange.ToVector3() * 0.4f);
                    Dust dust = Dust.NewDustDirect(projectile.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), projectile.width, projectile.height, DustID.Firefly, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.Orange, 1.5f);
                }
                if (EmpoweredByFrostQueen)
                {
                    Lighting.AddLight(projectile.Center, Color.PowderBlue.ToVector3() * 0.4f);
                    Dust dust = Dust.NewDustDirect(projectile.position + new Vector2(Main.rand.Next(-20, 20), Main.rand.Next(-20, 20)), projectile.width, projectile.height, DustID.Ice, Main.rand.Next(-0, 0), Main.rand.Next(-0, 0), default, Color.LightBlue, 1f);
                }
            }
        }

        public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (projectile.type == ProjectileID.HolyWater)
            {
                if (target.type == ModContent.NPCType<FallenPixie>() || target.type is NPCID.Pixie || target.type == ModContent.NPCType<GreatPixie>())
                {
                    if (target.type == ModContent.NPCType<FallenPixie>())
                    {
                        target.lifeMax = 15;
                        target.life = 15;
                        target.defense = 15;
                    }
                    modifiers.FinalDamage *= 0;
                    target.HealEffect(1, true);
                    target.life += 1;

                    modifiers.DisableCrit();
                }
                
            }
            if (projectile.type is ProjectileID.CandyCorn or ProjectileID.JackOLantern or ProjectileID.FlamingJack or ProjectileID.Stake
               or ProjectileID.Bat or ProjectileID.PineNeedleFriendly or ProjectileID.PineNeedleHostile
            or ProjectileID.Raven or ProjectileID.OrnamentFriendly or ProjectileID.OrnamentStar
             or ProjectileID.ClusterSnowmanRocketI or ProjectileID.RocketSnowmanI or ProjectileID.RocketSnowmanIII
             or ProjectileID.MiniNukeSnowmanRocketI or ProjectileID.ScytheWhip or ProjectileID.ScytheWhipProj or ProjectileID.Blizzard)
            {
                if (target.type == ModContent.NPCType<TheHellborneSkull>() || target.type == ModContent.NPCType<HellborneSkullMinion>() || target.type == ModContent.NPCType<HellborneGuardian>()
                    || target.type == NPCID.Golem || target.type == NPCID.GolemFistLeft || target.type == NPCID.GolemFistRight || target.type == NPCID.GolemHead || target.type == NPCID.GolemHeadFree)
                {
                    //projectile.damage *= 2;
                    modifiers.FinalDamage *= 2;
                    projectile.netUpdate = true;
                    target.netUpdate = true;
                }
            }
            if (projectile.type == ProjectileID.GolemFist)
            {
                if (EmpoweredByPumpking)
                {
                    modifiers.SetCrit();
                    target.AddBuff(BuffID.Oiled, 320, false);
                    target.AddBuff(BuffID.OnFire, 320, false);
                    Projectile.NewProjectile(Projectile.GetSource_None(), projectile.position, projectile.velocity, ProjectileID.SolarWhipSwordExplosion, 120, 1f, projectile.owner);
                }

                if (EmpoweredByFrostQueen)
                {
                    modifiers.SetCrit();
                    target.AddBuff(BuffID.Frostburn2, 320, false);

                    Projectile.NewProjectile(Projectile.GetSource_None(), projectile.position, projectile.velocity / 8 + new Vector2(Main.rand.Next(-3, 3), Main.rand.Next(0, 5)), ProjectileID.NorthPoleSnowflake, 40, 1f, projectile.owner);
                    Projectile.NewProjectile(Projectile.GetSource_None(), projectile.position, projectile.velocity / 8 + new Vector2(Main.rand.Next(-3, 3), Main.rand.Next(0, 5)), ProjectileID.NorthPoleSnowflake, 40, 1f, projectile.owner);
                    Projectile.NewProjectile(Projectile.GetSource_None(), projectile.position, projectile.velocity / 8 + new Vector2(Main.rand.Next(-3, 3), Main.rand.Next(0, 5)), ProjectileID.NorthPoleSnowflake, 40, 1f, projectile.owner);
                    Projectile.NewProjectile(Projectile.GetSource_None(), projectile.position, projectile.velocity / 8 + new Vector2(Main.rand.Next(-3, 3), Main.rand.Next(0, 5)), ProjectileID.NorthPoleSnowflake, 40, 1f, projectile.owner);
                    // Projectile.NewProjectile(Projectile.GetSource_None(), projectile.position, projectile.velocity / 5, ProjectileID., default, 1f, projectile.owner);
                }
            }
        }
        public override bool PreDraw(Projectile projectile, ref Color lightColor)
        {

            if (projectile.type == ProjectileID.GolemFist)
            {
                if (EmpoweredByPumpking)
                {
                    Texture2D texture = TextureAssets.Projectile[projectile.type].Value;

                    SpriteEffects spriteEffects = SpriteEffects.None; // Keep in mind if I want to do an NPC I need to adjust for it being in the bestiary like this; Vector2 screenPos = npc.IsABestiaryIconDummy ? Vector2.Zero : Main.screenPosition;
                    Vector2 screenPos = Main.screenPosition;

                    for (int i = 0; i < projectile.oldPos.Length; i++)
                    {
                        Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[projectile.type].Value.Width * 0.5f, projectile.height * 0.5f);
                        Vector2 drawPosition = projectile.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY); // gfxOffY is some math shit that fixes slope collision drawing

                        Color color = new Color(252, 190, 30, 80) * ((float)(projectile.oldPos.Length - i) / (float)projectile.oldPos.Length);
                        // new Color(252, 190, 30, 100) * (0.7f + 0.4f * ((255 - projectile.alpha) / 255f)) This was the color before switching
                        Main.spriteBatch.Draw(TextureAssets.Projectile[projectile.type].Value, drawPosition, new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, projectile.frame, texture.Width, texture.Height)),
                                    color, projectile.rotation, drawOrigin, projectile.scale * 1.1f, spriteEffects, 0f);
                    }
                    return false;
                }
                if (EmpoweredByFrostQueen)
                {
                    Texture2D texture = TextureAssets.Projectile[projectile.type].Value;

                    SpriteEffects spriteEffects = SpriteEffects.None; // Keep in mind if I want to do an NPC I need to adjust for it being in the bestiary like this; Vector2 screenPos = npc.IsABestiaryIconDummy ? Vector2.Zero : Main.screenPosition;
                    Vector2 screenPos = Main.screenPosition;

                    for (int i = 0; i < projectile.oldPos.Length; i++)
                    {
                        Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[projectile.type].Value.Width * 0.5f, projectile.height * 0.5f);
                        Vector2 drawPosition = projectile.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY); // gfxOffY is some math shit that fixes slope collision drawing

                        Color color = new Color(5, 20, 240, 55) * ((float)(projectile.oldPos.Length - i) / (float)projectile.oldPos.Length);
                        // new Color(252, 190, 30, 100) * (0.7f + 0.4f * ((255 - projectile.alpha) / 255f)) This was the color before switching
                        Main.spriteBatch.Draw(TextureAssets.Projectile[projectile.type].Value, drawPosition, new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, projectile.frame, texture.Width, texture.Height)),
                                    color, projectile.rotation, drawOrigin, projectile.scale * 1.1f, spriteEffects, 0f);
                    }
                    return false;
                } // these both do the same thing rn but I'm gonna keep them seperate incase I make any specific changes for each

            }
            return true;
        }





        public override bool? CanHitNPC(Projectile projectile, NPC target)
        {
            /*
            if (projectile.type is ProjectileID.QueenSlimeSmash or ProjectileID.QueenSlimeGelAttack or ProjectileID.QueenSlimeMinionBlueSpike or ProjectileID.QueenSlimeMinionPinkBall 
               or ProjectileID.PinkLaser)
            {
                if (target.type is NPCID.EaterofWorldsBody or NPCID.EaterofWorldsHead or NPCID.EaterofWorldsTail or NPCID.BrainofCthulhu or NPCID.Plantera)
                {
                    return true;
                }

                if (target.type is NPCID.Unicorn or NPCID.Pixie or NPCID.SandsharkHallow or NPCID.HallowBoss or NPCID.QueenSlimeBoss or NPCID.QueenSlimeMinionBlue or NPCID.QueenSlimeMinionPink or NPCID.Gastropod or NPCID.LightMummy or NPCID.RainbowSlime or NPCID.FlyingFish || target.type == ModContent.NPCType<RainbowFish>() || target.boss || target.type is NPCID.Probe or NPCID.TheDestroyerBody or NPCID.TheDestroyerTail or NPCID.TheDestroyer or NPCID.EmpressButterfly)
                {
                    return false;
                }
                else if (target.boss != true)
                {
                    return true;
                }
                else if (target.aiStyle == 1 && target.type != NPCID.Slimeling && target.type != NPCID.CorruptSlime && target.type != NPCID.Slimer && target.type != NPCID.Slimer2 && target.type != NPCID.Crimslime && target.type != NPCID.BigCrimslime)
                {
                    return false;
                }

                if (target.type is NPCID.EaterofWorldsBody or NPCID.EaterofWorldsHead or NPCID.EaterofWorldsTail or NPCID.BrainofCthulhu or NPCID.Plantera)
                {
                    return true;
                }
                if (target.ModNPC != null)
                {
                    return false;
                }

               
            }
            if (projectile.type is ProjectileID.FairyQueenSunDance or ProjectileID.FairyQueenLance or ProjectileID.FairyQueenHymn or ProjectileID.HallowBossDeathAurora or ProjectileID.HallowBossLastingRainbow
               or ProjectileID.HallowBossRainbowStreak or ProjectileID.HallowBossSplitShotCore or ProjectileID.HallowBossLastingRainbow)
            {
                if (target.type is NPCID.EaterofWorldsBody or NPCID.EaterofWorldsHead or NPCID.EaterofWorldsTail or NPCID.BrainofCthulhu)
                {
                    return true;
                }
                if (target.type is NPCID.Unicorn or NPCID.Pixie or NPCID.SandsharkHallow or NPCID.HallowBoss or NPCID.QueenSlimeBoss or NPCID.QueenSlimeMinionBlue or NPCID.QueenSlimeMinionPink or NPCID.Gastropod or NPCID.LightMummy or NPCID.RainbowSlime or NPCID.FlyingFish || target.type == ModContent.NPCType<RainbowFish>() || target.boss)
                {
                    return false;
                }
                else if (target.boss != true)
                {
                    return true;
                }
                if (target.type is NPCID.EaterofWorldsBody or NPCID.EaterofWorldsHead or NPCID.EaterofWorldsTail or NPCID.BrainofCthulhu or NPCID.Plantera)
                {
                    return true;
                }
                if (target.ModNPC != null)
                {
                    return false;
                }
            }
            // END OF HALLOW, START OF EVILS

            if (projectile.type == ProjectileID.CursedFlameHostile || projectile.type == ProjectileID.GoldenShowerHostile)
            {
                if (target.type is NPCID.CorruptBunny or NPCID.Corruptor or NPCID.EaterofSouls or NPCID.EaterofWorldsBody or NPCID.EaterofWorldsHead or NPCID.EaterofWorldsTail or NPCID.DevourerBody
                    or NPCID.CorruptSlime or NPCID.Slimer or NPCID.Slimeling or NPCID.DarkMummy or NPCID.CorruptGoldfish or NPCID.Crimslime or NPCID.CrimsonBunny or NPCID.CrimsonGoldfish or NPCID.Crimera
                     or NPCID.BigCrimera or NPCID.CrimsonAxe or NPCID.BigCrimslime or NPCID.Herpling or NPCID.Creeper or NPCID.BrainofCthulhu or NPCID.BloodMummy or NPCID.BloodJelly
                      or NPCID.BloodFeeder or NPCID.BloodCrawler or NPCID.BloodCrawlerWall or NPCID.FaceMonster or NPCID.FloatyGross or NPCID.IchorSticker or NPCID.BloodCrawlerWall
                       or NPCID.DesertGhoulCrimson or NPCID.DesertGhoulCorruption or NPCID.CursedHammer or NPCID.SeekerBody or NPCID.Clinger or NPCID.BigMimicCorruption or NPCID.BigMimicCrimson || target.boss || target.type is NPCID.ServantofCthulhu or NPCID.CultistBoss or NPCID.SkeletronHand)
                {
                    return false;
                }
                if (target.type is NPCID.HallowBoss or NPCID.QueenSlimeBoss  || target.type == ModContent.NPCType<RainbowFish>())
                {
                    return true;
                }
                if (target.ModNPC != null)
                {
                    return false;
                }
                else
                    return true;
            }
            // PLANTERA
            if (projectile.type == ProjectileID.SeedPlantera || projectile.type == ProjectileID.PoisonSeedPlantera || projectile.type == ProjectileID.ThornBall)
            {
                if (target.type is NPCID.HallowBoss or NPCID.QueenSlimeBoss or NPCID.EaterofWorldsBody or NPCID.EaterofWorldsHead or NPCID.EaterofWorldsTail or NPCID.BrainofCthulhu or NPCID.Retinazer or NPCID.Spazmatism or NPCID.TheDestroyer or NPCID.TheDestroyerBody or NPCID.SkeletronPrime)
                {
                    return true;
                }
                if (target.type is NPCID.JungleSlime or NPCID.SpikedJungleSlime or NPCID.PlanterasTentacle or NPCID.PlanterasHook or NPCID.Hornet or NPCID.GiantTortoise or NPCID.JungleBat or NPCID.Snatcher or NPCID.AngryTrapper or NPCID.Arapaima or NPCID.JungleCreeper or NPCID.Derpling or NPCID.JungleCreeperWall or NPCID.TurtleJungle or NPCID.Frog or NPCID.DoctorBones or NPCID.BigMimicJungle or NPCID.Grubby or NPCID.Sluggy or NPCID.Buggy or NPCID.Moth or NPCID.HornetFatty or NPCID.HornetLeafy or NPCID.HornetSpikey or NPCID.GiantMossHornet or NPCID.LittleMossHornet or NPCID.Piranha or NPCID.LittleHornetFatty or NPCID.HornetStingy or NPCID.GiantMossHornet or NPCID.LittleHornetLeafy or NPCID.LittleHornetStingy or NPCID.LittleHornetSpikey or NPCID.GolemFistLeft or NPCID.GolemFistRight or NPCID.GolemHead or NPCID.GolemHeadFree or NPCID.FlyingSnake or NPCID.Lihzahrd or NPCID.LihzahrdCrawler)
                {
                    return false;
                }
                else if (target.boss != true)
                {
                    return true;
                }
                if (target.type is NPCID.HallowBoss or NPCID.QueenSlimeBoss or NPCID.EaterofWorldsBody or NPCID.EaterofWorldsHead or NPCID.EaterofWorldsTail or NPCID.BrainofCthulhu or NPCID.Retinazer or NPCID.Spazmatism or NPCID.TheDestroyer or NPCID.TheDestroyerBody or NPCID.SkeletronPrime)
                {
                    return true;
                }
                if (target.ModNPC != null)
                {
                    return false;
                }
                else
                    return false;
            }
            /*
            return null;
        }
      
      /*  public override void SetDefaults(Projectile projectile)
        {
            if (projectile.type == ProjectileID.FairyQueenSunDance || projectile.type == ProjectileID.FairyQueenLance || projectile.type == ProjectileID.FairyQueenHymn || projectile.type == ProjectileID.HallowBossDeathAurora || projectile.type == ProjectileID.HallowBossLastingRainbow || projectile.type == ProjectileID.HallowBossRainbowStreak || projectile.type == ProjectileID.HallowBossSplitShotCore || projectile.type == ProjectileID.HallowBossLastingRainbow || projectile.type == ProjectileID.QueenSlimeSmash || projectile.type == ProjectileID.QueenSlimeGelAttack)
            {
                projectile.hostile = true;
                projectile.friendly = true;
            }
        } */
            return base.CanHitNPC(projectile, target);
        }
    }
}
