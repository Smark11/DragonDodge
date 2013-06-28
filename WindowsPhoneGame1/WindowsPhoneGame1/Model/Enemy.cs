using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
using Microsoft.Devices.Sensors;

namespace WindowsPhoneGame1.Model
{
    public class Enemy : Microsoft.Xna.Framework.Game
    {
        public Enemy(Vector2 position)
        {
            try
            {
                ListOfEnemy = new List<Texture2D>();
                EnemyPosition = position;
                //LoadContent();
            }
            catch (Exception ex)
            {

            }
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            try
            {
                //ListOfEnemy.Add(Content.Load<Texture2D>("Content/Enemy1/Enemy01Assset0001"));
                ListOfEnemy.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0002"));
                ListOfEnemy.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0003"));
                ListOfEnemy.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0004"));
                ListOfEnemy.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0005"));
                ListOfEnemy.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0006"));
                ListOfEnemy.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0007"));
                ListOfEnemy.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0008"));
                ListOfEnemy.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0009"));
                ListOfEnemy.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0010"));
                ListOfEnemy.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0011"));
                ListOfEnemy.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0012"));
                ListOfEnemy.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0013"));
                ListOfEnemy.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0014"));
                ListOfEnemy.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0015"));
                ListOfEnemy.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0016"));
                ListOfEnemy.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0017"));

                CurrentEnemyTexture = ListOfEnemy[0];
            }
            catch (Exception ex)
            {

            }
        }

        public Texture2D CurrentEnemyTexture;
        public Vector2 EnemyPosition;
        public List<Texture2D> ListOfEnemy = new List<Texture2D>();
    }
}
