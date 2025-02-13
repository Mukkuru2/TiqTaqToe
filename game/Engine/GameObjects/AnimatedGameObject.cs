﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Blok3Game.Engine.GameObjects
{
	public class AnimatedGameObject : SpriteGameObject
	{
		protected Dictionary<string,Animation> animations;

		public AnimatedGameObject(int layer = 0, string id = "")
			: base("", layer, id)
		{
			animations = new Dictionary<string, Animation>();
		}

		public void LoadAnimation(string assetName, string id, bool looping, float frameTime = 0.1f)
		{
			Animation anim = new Animation(assetName, looping, frameTime);
			animations[id] = anim;        
		}

		/*public void PlayAnimation(string id)
		{
			if (sprite == animations[id])
			{
				return;
			}
			if (sprite != null)
			{
				animations[id].Mirror = sprite.Mirror;
			}
			animations[id].Play();
			sprite = animations[id];
			origin = new Vector2(sprite.Width / 2, sprite.Height);        
		}*/

		public void PlayAnimation(string id, bool forceRestart = false, int startSheetIndex = 0)
		{
			// if the animation is already playing, do nothing
			if (!forceRestart && sprite == animations[id])
				return;

			animations[id].Play(startSheetIndex);
			sprite = animations[id];
		}

		public override void Update(GameTime gameTime)
		{
			if (sprite == null)
			{
				return;
			}
			Current.Update(gameTime);
			base.Update(gameTime);
		}

		public Animation Current
		{
			get { return sprite as Animation; }
		}
	}
}