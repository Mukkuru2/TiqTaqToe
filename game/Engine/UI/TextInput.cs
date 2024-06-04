using Blok3Game.Engine.GameObjects;
using Blok3Game.Engine.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Blok3Game.Engine.UI
{
    public class TextInput : UIElement
    {
        private SpriteGameObject background;
        private TextGameObject text;

        public override UIElementMouseState UIElementState 
        { 
            get => base.UIElementState;
            set 
            {
                uiElementMouseState = value;
                background.Sprite.SheetIndex = (int)value;
            }
        }

        public TextInput(Vector2 position, float scale) : base()
        {
            Position = position;

            AddBackground(scale);
            AddText();

            UIElementState = UIElementMouseState.Normal;
        }

        private void AddBackground(float scale)
        {
            background = new SpriteGameObject("Images/UI/TextInput@1x2", 0, "background");
            background.Scale = scale;
            AddBackground(background);
        }

        private void AddText()
        {
            text = new TextGameObject("Fonts/SpriteFont", 1, "text");
            text.Text = "";
            text.Position = new Vector2(20, background.Height / 2 - text.Size.Y / 2 - 5);
            Add(text);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (background.BoundingBox.Contains(inputHelper.MousePosition))
            {
                if (inputHelper.MouseLeftButtonPressed)
                {
                    isClicked = true;                    
                }                             
                if (isClicked && inputHelper.MouseLeftButtonReleased)
                {
                    isClicked = false;
                    OnClicked();
                }
            }

            if (UIElementState == UIElementMouseState.Disabled)
            {
                return;
            }

            if (inputHelper.KeyPressed(Keys.Back))
            {
                if (text.Text.Length > 0)
                {
                    text.Text = text.Text.Substring(0, text.Text.Length - 1);
                }
            }
            else if (inputHelper.KeyPressed(Keys.Space))
            {
                text.Text += " ";
            }            
            else if (inputHelper.AnyKeyPressed)
            {
                if (inputHelper.GetKeyPressed(out Keys keys))
                {
                    int key = (int)keys;
                    //Alphanumeric keys only. See ASCII table (https://www.commfront.com/pages/ascii-chart) for more info.
                    if (key >= 65 && key <= 90) 
                    {
                        text.Text += keys;
                    }
                    else if (key >= 48 && key <= 57)
                    { 
                        text.Text += key - 48;
                    }
                }                
            }
        }

        public string Text => text.Text;
    }
}
