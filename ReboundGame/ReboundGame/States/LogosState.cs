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

using StarEngine.Reflect;
using System.Reflection;
using StarEngine.Archive;
using StarEngine.Lighting;
using StarEngine.PostProcess;
using StarEngine.Import;
using StarEngine.Material;
using StarEngine.State;
namespace ReboundGame.States
{
    public class LogosState : StarState
    {
        public GraphCam3D cam1 = null;
        public SceneGraph3D scene3d = null;
        public GraphNode3D ent1 = null;
        public GraphLight3D light1 = null;
        public PostProcessRender ppRen;
        public override void InitState()
        {
            Console.WriteLine("Setting up 3D test.");
            //  Import.RegDefaults();
            Console.WriteLine("Setting up post-processing.");
            ppRen = new StarEngine.PostProcess.PostProcessRender(512, 512);
            Console.WriteLine("Creating 3D Scene graph.");
            scene3d = new SceneGraph3D();

            ppRen.Scene = scene3d;

            Console.WriteLine("Importing mesh.");
            ent1 = Import.ImportNode("Data\\3D\\testshadow1.3ds");
            Console.WriteLine("Set up.");
            var mat1 = new Material3D();
            Console.WriteLine("Loading texture.");
            mat1.TCol = new Tex2D("Data\\3D\\brick_2.png");
            mat1.TNorm = new Tex2D("Data\\3D\\brick_2_NRM.png");
            Console.WriteLine("Loaded.");
            var ge = ent1 as GraphEntity3D;

            ge.SetMat(mat1);
            cam1 = new GraphCam3D();
            cam1.LocalPos = new OpenTK.Vector3(0, 25, 120);


            //cam1.LookAt(ent1);



            light1 = new StarEngine.Lighting.GraphLight3D();
            var l2 = new StarEngine.Lighting.GraphLight3D();
            var l3 = new StarEngine.Lighting.GraphLight3D();

            l3.LocalPos = new OpenTK.Vector3(20, 40, 5);
            l3.Diff = new OpenTK.Vector3(0, 1, 2);

            l2.LocalPos = new OpenTK.Vector3(5, 25, 20);
            l2.Diff = new OpenTK.Vector3(1, 2, 1);


            light1.LocalPos = new OpenTK.Vector3(0, 10, 60);

            ent1.Rot(new OpenTK.Vector3(0, 45, 0), Space.Local);


            scene3d.Add(ent1);

            scene3d.Add(l2);

            scene3d.Add(l3);
            scene3d.Add(light1);

            scene3d.Add(cam1);

            light1.Diff = new OpenTK.Vector3(1, 1, 1);
        }

        public override void DrawState()
        {
            r = r + 1;

            light1.Diff = new OpenTK.Vector3(2, 2, 1);
            light1.Pos(new OpenTK.Vector3(StarEngine.Util.Maths.Cos(r) * 50, 30, StarEngine.Util.Maths.Sin(r) * 50), Space.Local);

            //       cam1.LocalPos = new OpenTK.Vector3(0, 0, 30);

            //light1.LocalPos = cam1.LocalPos;
            //light1.LocalTurn = cam1.LocalTurn;
            cam1.LocalPos = new OpenTK.Vector3(0, 25, 90);
            scene3d.Render();



        }
        private float r = 0;

    }
}
