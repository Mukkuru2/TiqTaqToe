using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blok3Game.Engine.Helpers
{
	public class DrawingHelper
	{
		protected static Texture2D pixel;
		private static GraphicsDevice graphics;
		
		private static Texture2D[] circleTextures = new Texture2D[100];

		public static void Initialize(GraphicsDevice graphics)
		{
			DrawingHelper.graphics = graphics;
			
			pixel = new Texture2D(graphics, 1, 1);
			pixel.SetData(new[] { Color.White });
			
			for (int i = 1; i <= 100; i++)
			{
				circleTextures[i - 1] = createCircleTexture(i);
			}
		}

		public static void DrawRectangle(Rectangle r, SpriteBatch spriteBatch, Color col, int borderWidth = 1)
		{
			int bw = borderWidth; // Border width

			spriteBatch.Draw(pixel, new Rectangle(r.Left, r.Top, bw, r.Height), col); // Left
			spriteBatch.Draw(pixel, new Rectangle(r.Right, r.Top, bw, r.Height), col); // Right
			spriteBatch.Draw(pixel, new Rectangle(r.Left, r.Top, r.Width, bw), col); // Top
			spriteBatch.Draw(pixel, new Rectangle(r.Left, r.Bottom, r.Width, bw), col); // Bottom
		}
		
		public static void DrawRectangleFill(Rectangle r, SpriteBatch spriteBatch, Color col)
		{
			spriteBatch.Draw(pixel, r, col);
		}

		public static void DrawPoint(Vector2 p, SpriteBatch spriteBatch, Color col)
		{
			spriteBatch.Draw(pixel, p, col);
		}

		public static void DrawCircle(int x, int y, int r, SpriteBatch spriteBatch, Color col)
		{
			if (r > 100)
			{
				r = 100;
			}
			
			Texture2D circle = circleTextures[r - 1];
			spriteBatch.Draw(circle, new Vector2(x - r, y - r), col);
		}
		
		public static void DrawCirclePerformance(int x, int y, int r, SpriteBatch spriteBatch, Color col)
		{
			for(int i = 0; i < r; i++)
			{
				for (int j = 0; j < r; j++)
				{
					if (i * i + j * j < r * r)
					{
						spriteBatch.Draw(pixel, new Vector2(x + i, y + j), col);
						spriteBatch.Draw(pixel, new Vector2(x - i, y + j), col);
						spriteBatch.Draw(pixel, new Vector2(x + i, y - j), col);
						spriteBatch.Draw(pixel, new Vector2(x - i, y - j), col);
					}
				}
			}
		}
		
		private static Texture2D createCircleTexture(int radius)
		{
			int diam = radius * 2;
			Texture2D texture = new Texture2D(graphics, diam, diam);
			Color[] colorData = new Color[diam * diam];
			
			float radiussq = radius * radius;

			for (int x = 0; x < diam; x++)
			{
				for (int y = 0; y < diam; y++)
				{
					int index = x * diam + y;
					Vector2 pos = new Vector2(x - radius, y - radius);
					if (pos.LengthSquared() <= radiussq)
					{
						colorData[index] = Color.White;
					}
					else
					{
						colorData[index] = Color.Transparent;
					}
				}
			}
			
			texture.SetData(colorData);
			return texture;
		}
	}
}
