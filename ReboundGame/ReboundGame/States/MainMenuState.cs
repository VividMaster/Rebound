using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarEngine;
using StarEngine.App;
using StarEngine.Draw;
using StarEngine.FX;
using StarEngine.FXS;
using StarEngine.Input;
using StarEngine.Scene;
using StarEngine.Tex;
using StarEngine.Util;
using StarEngine.VFX;
using StarEngine.Sound;
using StarEngine.Reflect;
using System.Reflection;
using StarEngine.Archive;
using StarEngine.Lighting;
using StarEngine.PostProcess;
using StarEngine.Import;
using StarEngine.Material;
using StarEngine.State;
using StarEngine.Texture;
using StarEngine.Logic;
using StarEngine.Resonance;
using StarEngine.App;
using StarEngine.Resonance.Forms;
namespace ReboundGame.States
{
    public class MainMenuState : StarState
    {


        public UI UI = null;
        public ImageForm BG;
        private VTex2D MenuBG;
        public VSound Music;

        public GraphCam3D cam1 = null;
        public SceneGraph3D scene3d = null;
        public GraphNode3D ent1 = null;
        public GraphLight3D light1 = null;
        public PostProcessRender ppRen;

        public override void InitState()
        {



            MenuBG = new VTex2D("Data\\2D\\Backgrounds\\MainMenu\\menubg.jpg", LoadMethod.Single, false);

            Music = StarSoundSys.Play2DFile("Data\\Music\\Menu\\MainMenu\\MenuTheme1.mp3");

            UI = new UI();

            UI.Root = new ImageForm().Set(0, 0, StarEngine.App.StarApp.W, StarEngine.App.StarApp.H,"ImageForm").SetImage(MenuBG);

            UI.Root = new ButtonForm().Set(50, 100, 200, 40, "Test");

            var test = UI.Root;

            test.Click = (b) =>
            {

                Console.WriteLine("Yep!");

            };

            ppRen = new StarEngine.PostProcess.PostProcessRender(512, 512);
            Console.WriteLine("Creating 3D Scene graph.");
            scene3d = new SceneGraph3D();

            ppRen.Scene = scene3d;

            Console.WriteLine("Importing mesh.");
            ent1 = Import.ImportNode("Data\\3D\\Logo\\Menu\\Wall1.3ds");
            Console.WriteLine("Set up.");
            //var mat1 = new Material3D();
            //Console.WriteLine("Loading texture.");
            //mat1.TCol = new Tex2D("Data\\3D\\brick_2.png");
            //mat1.TNorm = new Tex2D("Data\\3D\\brick_2_NRM.png");
            Console.WriteLine("Loaded.");

            var ge = ent1 as GraphEntity3D;

            //ge.SetMat(mat1);
            cam1 = new GraphCam3D();
            cam1.LocalPos = new OpenTK.Vector3(0, 25, 150);


            //cam1.LookAt(ent1);



            light1 = new StarEngine.Lighting.GraphLight3D();
            var l2 = new StarEngine.Lighting.GraphLight3D();
            var l3 = new StarEngine.Lighting.GraphLight3D();

            l3.LocalPos = new OpenTK.Vector3(20, 40, 5);
            l3.Diff = new OpenTK.Vector3(0, 1, 2);

            l2.LocalPos = new OpenTK.Vector3(5, 25, 20);
            l2.Diff = new OpenTK.Vector3(1, 2, 1);


            light1.LocalPos = new OpenTK.Vector3(0, 40, 150);

            ent1.Rot(new OpenTK.Vector3(0, 45, 0), Space.Local);



            scene3d.Add(ent1);

  //          scene3d.Add(l2);

//            scene3d.Add(l3);
            scene3d.Add(light1);

            scene3d.Add(cam1);

            light1.Diff = new OpenTK.Vector3(1, 1, 1);


        }

        public override void UpdateState()
        {

            UI.Update();

        }

        public override void DrawState()
        {

            scene3d.Render();

            UI.Render();


        }


    }
}
