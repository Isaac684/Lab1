﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace Tarea1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D fondoPantalla;
        Texture2D pared;

        Color fondo = new Color(51, 134, 255);
        SpriteFont _font;
        Texture2D pez;//aqui esta la clave
        Vector2 posicionpez;
        int pezDireccion = 0; //1=izquierda 0=derecha

        Texture2D gusano;
        Vector2 posiciongusano;
        Texture2D[] gusanosz;

        int puntaje = 0;        

        Texture2D tiburon;
        Texture2D[,] tiburonframes;
        int cambiarframe = 0;
        Vector2 posiciontiburon;
        int tiburonDireccion = 0; //1=izquierda 0=derecha

        Song song;
        float vel;
        bool iniciojuego = false;

        //controlar colisiones
        bool pezgusano = false;
        bool peztiburon = false;

        Rectangle[] rects;//arreglo maneja las coordenadas de las intercepciones de sprite
        List<Rectangle> rectas;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            this._graphics.PreferredBackBufferWidth = 1366;//ancho pantalla
            this._graphics.PreferredBackBufferHeight = 700;//alto
            
        }

        void crearrectangulos()
        {
            rectas = new List<Rectangle>();
            rects = new Rectangle[4];
            //rects[0] = new Rectangle();
            rects[0] = new Rectangle((int)posicionpez.X+10,(int)posicionpez.Y+33,pez.Width-20,pez.Height-33);            
            rects[1] = new Rectangle((int)posiciontiburon.X+25, (int)posiciontiburon.Y+40, tiburon.Width-45, tiburon.Height-65);
            rects[2] = new Rectangle((int)posiciongusano.X, (int)posiciongusano.Y, gusano.Width, gusano.Height);
            //rectas.Add(new Rectangle((int)posicionpez.X + 10, (iposiciongusano.Y+300nt)posicionpez.Y, pez.Width - 20, pez.Height));
            //rectas.Add(new Rectangle((int)posiciontiburon.X, (int)posiciontiburon.Y + 53, tiburon.Width, tiburon.Height - 60));
        }
 
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //arreglo que contendra todos los frames del tiburon
            tiburonframes = new Texture2D[20, 2];

            posicionpez = new Vector2((_graphics.PreferredBackBufferWidth / 2) - 30, (_graphics.PreferredBackBufferHeight / 2) - 30);
            vel = 5f;

            //se define una posicion inicial random al gusano
            Random rnd = new Random();
            posiciongusano = new Vector2(rnd.Next(25, 1290), rnd.Next(105, 650));//105,650 Y  // x 25,1290

            //se define una posicion inicial random al tiburon en y
            Random rand = new Random();
            posiciontiburon = new Vector2(-1500, rand.Next(25, 570));//-1500
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            pez = Content.Load<Texture2D>("pez/pezderecha1");
            gusano = Content.Load<Texture2D>("gusano/gusano1");
            
            //cargando los sprite del tiburon 1 es izquierda 0 derecha
            for (int i = 0; i < 20; i++)
            {
                tiburonframes[i, 1] = Content.Load<Texture2D>("tiburon/tiburonizquierda" + (i + 1));
            }

            for (int i = 0; i < 20; i++)
            {
                tiburonframes[i, 0] = Content.Load<Texture2D>("tiburon/tiburonderecha" + (i + 1));
            }

            //asignandole el primer frame al sprite tiburon
            tiburon = tiburonframes[0, 0];
            
            fondoPantalla = Content.Load<Texture2D>("fondo");
            song = Content.Load<Song>("flowergarden");
            pared = Content.Load<Texture2D>("rect");

            
            MediaPlayer.Volume = 0.0F;
            MediaPlayer.Play(song, TimeSpan.Parse("00:02:30"));
            //MediaPlayer.IsRepeating = true;
            _font = Content.Load<SpriteFont>("fuente1");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            if (gameTime.TotalGameTime.Milliseconds % 120 == 0)
            {
                gusano = Content.Load<Texture2D>("gusano/gusano2");
            }
            if (gameTime.TotalGameTime.Milliseconds % 270 == 0)
            {
                gusano = Content.Load<Texture2D>("gusano/gusano3");
            }
            if (gameTime.TotalGameTime.Milliseconds % 390 == 0)
            {
                gusano = Content.Load<Texture2D>("gusano/gusano4");
            }
            if (gameTime.TotalGameTime.Milliseconds % 490 == 0)
            {
                gusano = Content.Load<Texture2D>("gusano/gusano1");
            }

            if (pezDireccion == 0)
            {
                if (gameTime.TotalGameTime.Milliseconds % 70 == 0)
                {
                    pez = Content.Load<Texture2D>("pez/pezderecha2");
                }
                if (gameTime.TotalGameTime.Milliseconds % 200 == 0)
                {
                    pez = Content.Load<Texture2D>("pez/pezderecha3");
                }
                if (gameTime.TotalGameTime.Milliseconds % 320 == 0)
                {
                    pez = Content.Load<Texture2D>("pez/pezderecha4");
                }
                /*if (gameTime.TotalGameTime.Milliseconds % 450 == 0)
                {
                    pez = Content.Load<Texture2D>("pez1");
                }*/
            }
            else if (pezDireccion == 1)
            {
                if (gameTime.TotalGameTime.Milliseconds % 70 == 0)
                {
                    pez = Content.Load<Texture2D>("pez/pezizquierda2");
                }
                if (gameTime.TotalGameTime.Milliseconds % 200 == 0)
                {
                    pez = Content.Load<Texture2D>("pez/pezizquierda3");
                }
                if (gameTime.TotalGameTime.Milliseconds % 320 == 0)
                {
                    pez = Content.Load<Texture2D>("pez/pezizquierda4");
                }
            }


            
            //desplazamiento del tiburon 
            if (tiburonDireccion == 0 && posiciontiburon.X <= 1406)
            {
                posiciontiburon.X += 8;
            }
            else if (tiburonDireccion == 1 && posiciontiburon.X >= -300)
            {

                posiciontiburon.X -= 8;
            }
            else if (tiburonDireccion == 0 && posiciontiburon.X >= 1406)
            {
                tiburonDireccion = 1;
                Random random = new Random();
                posiciontiburon.Y = random.Next(25, 570);
                //posiciontiburon.X = 1406;
            }

            else if (tiburonDireccion == 1 && posiciontiburon.X <= -300)
            {

                tiburonDireccion = 0;
                Random random = new Random();
                posiciontiburon.Y = random.Next(25, 570);
            }

            //animacion del tiburon cada cierto tiempo cambia al siguiente frame
            if (gameTime.TotalGameTime.Milliseconds % 25 == 0)
            {
                cambiarframe++;
                tiburon = tiburonframes[cambiarframe, tiburonDireccion];
            }

            //variable bandera para la posicion del frame del sprite tiburon
            if (cambiarframe == 19)
            {
                cambiarframe = 0;
            }


            var teclaestado = Keyboard.GetState();
            
            if (pezgusano && quecolisiono()==1)
            {
                pezgusano = false;
                puntaje++;
                Random rnd = new Random();

                Vector2 tempgusano = new Vector2(rnd.Next(25, 1290), rnd.Next(105, 650));
                Rectangle temp = new Rectangle((int)tempgusano.X, (int)tempgusano.Y, gusano.Width, gusano.Height);
                
                while (temp.Intersects(rects[2]))
                {
                    tempgusano = new Vector2(rnd.Next(25, 1290), rnd.Next(105, 650));
                    temp = new Rectangle((int)tempgusano.X, (int)tempgusano.Y, gusano.Width, gusano.Height);
                }
                posiciongusano = tempgusano;

            }

            quecolisiono();

            if (peztiburon == true)
            {
                //posicionpez = new Vector2(0, 0);
                //peztiburon = false;
                //Exit();

            }

            if (teclaestado.IsKeyDown(Keys.Up) && posicionpez.Y >= 40 && quecolisiono()!=2)
            {               
                posicionpez.Y -= vel;
                peztiburon = false;
            }
            //cuando el tiburon toca el pez
            else if (peztiburon == true)
            {
                //posicionpez = new Vector2(0,0);
                puntaje = 0;
                peztiburon = false;
            }

            if (teclaestado.IsKeyDown(Keys.Down) && posicionpez.Y <= _graphics.GraphicsDevice.Viewport.Height - 135 && quecolisiono() != 2)
            {
                posicionpez.Y += vel;
            }
            if (teclaestado.IsKeyDown(Keys.Left) && posicionpez.X >= 3 && quecolisiono() != 2)
            {

                pezDireccion = 1;

                posicionpez.X -= vel;
            }
            if (teclaestado.IsKeyDown(Keys.Right) && posicionpez.X <= _graphics.GraphicsDevice.Viewport.Width - 130 && quecolisiono() != 2)
            {

                pezDireccion = 0;
                posicionpez.X += vel;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(fondo);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(fondoPantalla, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
            _spriteBatch.DrawString(_font, "Josue Isaac Herrera Campos", new Vector2(1, 1), Color.White);
            _spriteBatch.DrawString(_font, "Score " + puntaje, new Vector2(_graphics.PreferredBackBufferWidth / 2, 1), Color.White);
            //_spriteBatch.DrawString(_font, "HC21018", new Vector2(_graphics.PreferredBackBufferWidth - 76, 1), Color.White);

            
            crearrectangulos();
            for (int i = 0; i < rects.Length; i++)
            {
                _spriteBatch.Draw(pared, rects[i], Color.White);
            }
            _spriteBatch.Draw(gusano, posiciongusano, Color.LightGreen);
            _spriteBatch.Draw(pez, posicionpez, Color.White);
            _spriteBatch.Draw(tiburon, posiciontiburon, Color.White);

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        int quecolisiono() //1 pezgusano, 2 peztiburon
        {
            if (rects!=null)
            {
                if (rects[0].Intersects(rects[1]))
                {
                    peztiburon = true;
                    return 2;
                }
                else if (rects[0].Intersects(rects[2]))
                {
                    pezgusano = true;
                    return 1;
                }

            }
            return 0;
        }
    }
}