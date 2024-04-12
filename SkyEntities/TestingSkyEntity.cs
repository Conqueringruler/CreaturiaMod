using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Skies;
using Terraria.GameContent.Skies.CreditsRoll;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.Graphics;
using Terraria.Utilities;
using System.Reflection;

namespace Creaturia.SkyEntities
{
	// ok this might actually be impossible, idk

  /*  public class TestingSkyEntity : AmbientSky
	{
		
		public TestingSkyEntity(Player player, FastRandom random)
		{
			VirtualCamera virtualCamera = new VirtualCamera(player);
			//TestingSkyEntity testingskyentity = new AirshipSkyEntity;
			SkyEntity.Effects = ((random.Next(2) != 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
			
			var SkyEntity = typeof(AmbientSky).GetNestedType("SkyEntity", BindingFlags.NonPublic);
			int num = 100;
			if (base.Effects == SpriteEffects.FlipHorizontally)
			{
				Position.X = virtualCamera.Position.X + virtualCamera.Size.X + (float)num;
			}
			else
			{
				base.Position.X = virtualCamera.Position.X - (float)num;
			}

			SkyEntity.Position.Y = random.NextFloat() * ((float)Main.worldSurface * 16f - 1600f - 2400f) + 2400f;
			base.Depth = random.NextFloat() * 3f + 3f;
			base.SetPositionInWorldBasedOnScreenSpace(base.Position);
			base.Texture = Main.Assets.Request<Texture2D>("Images/Backgrounds/Ambience/FlyingShip", (AssetRequestMode)1);
			base.Frame = new SpriteFrame(1, 4);
			base.LifeTime = random.Next(40, 71) * 60;
			base.OpacityNormalizedTimeToFadeIn = 0.05f;
			base.OpacityNormalizedTimeToFadeOut = 0.95f;
			base.BrightnessLerper = 0.2f;
			base.FinalOpacityMultiplier = 1f;
			base.FramingSpeed = 4;
		}

		public override void UpdateVelocity(int frameCount)
		{
			float num = 6f + Math.Abs(Main.WindForVisuals) * 1.6f;
			base.Velocity = new Vector2(num * (float)((base.Effects != SpriteEffects.FlipHorizontally) ? 1 : (-1)), 0f);
		}

		public override void Update(int frameCount)
		{
			base.Update(frameCount);
			if (Main.IsItRaining || !Main.dayTime || Main.eclipse)
			{
				base.StartFadingOut(frameCount);
			}
		}
	} */
}
