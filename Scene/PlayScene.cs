using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace templeRun
{
    class PlayScene : Scene
    {
       
        protected Background bg;

        protected int mapWidth;
        protected int mapHeight;

        public override void Start()
        {
            base.Start();

            Vector2 screenCenter = new Vector2(Game.window.Width / 2, Game.window.Height / 2);

            GfxManager.Init();
            
            GfxManager.AddSpritesheet("bg0",new Texture("Assets/BG.png"));
            
            UpdateManager.Init();
            DrawManager.Init();
            PhysicsManager.Init();

            GfxManager.LoadTiledMap("Assets/map.lv1trun.tmx", ref mapWidth, ref mapHeight);

            CameraManager.Init(screenCenter, screenCenter, mapWidth, mapHeight);
            CameraManager.AddCamera("bg", 0);
            
            FontManager.Init();
            
            bg = new Background("bg0", new Vector2(-640, -300), 0/*-20*/);
            bg.SetCamera(CameraManager.GetCamera("bg"));
        }

        public override void Draw()
        {
            DrawManager.Draw();
        }

        protected virtual void CreatePlayer(int id = 0)
        {
            //Player player = new Player("greenTank", new Vector2(200 + 100 * id, 20), id);
            //players.Add(player);
        }

        public override void Input()
        {
            if (Game.window.GetKey(KeyCode.D))
            {
                CameraManager.mainCamera.position.X += 500 * Game.DeltaTime;
            }
            else if (Game.window.GetKey(KeyCode.A))
            {
                CameraManager.mainCamera.position.X -= 500 * Game.DeltaTime;
            }
            else if (Game.window.GetKey(KeyCode.S))
            {
                CameraManager.mainCamera.position.Y += 500 * Game.DeltaTime;
                Console.WriteLine(CameraManager.mainCamera.position.Y);
            }
            else if (Game.window.GetKey(KeyCode.W))
            {
                CameraManager.mainCamera.position.Y -= 500 * Game.DeltaTime;
            }
            //for (int i = 0; i < players.Count; i++)
            //{
            //    players[i].UpdateFSM();
            //}
        }

        public virtual void ResetTimer()
        {
        }

        public virtual void StopTimer()
        {
        }

        public virtual void NextPlayer()
        {
            //currentPlayerIndex = (currentPlayerIndex + nextPlayerInc) % players.Count;
            //CurrentPlayer = players[currentPlayerIndex];
            //nextPlayerInc = 1;
            //CameraManager.MoveCameraTo(CurrentPlayer.Position);
            //CameraManager.SetTarget(CurrentPlayer);
            //CurrentPlayer.Play();
        }

        //public virtual void OnPlayerDies(Player player)
        //{
        //    players.Remove(player);

        //    if (players.Count == 1)
        //    {
        //        IsPlaying = false;
        //        return;
        //    }
        //    else if (player.PlayerId <= currentPlayerIndex)
        //    {
        //        nextPlayerInc = 0;
        //    }

        //    CurrentPlayer.Wait();
        //}

        public override void Update()
        {
            //if (timer.IsActive)
            //{
            //    PlayerTimer -= Game.DeltaTime;
            //    timer.Text = ((int)PlayerTimer).ToString();
            //}

            PhysicsManager.Update();
            UpdateManager.Update();
            PhysicsManager.CheckCollisions();
            //SpawnManager.Update();
            CameraManager.Update();
           
        }

        public override void OnExit()
        {
            UpdateManager.RemoveAll();
            DrawManager.RemoveAll();
            PhysicsManager.RemoveAll();
            //GfxManager.RemoveAll();
            //SpawnManager.RemoveAll();
        }
    }
}
