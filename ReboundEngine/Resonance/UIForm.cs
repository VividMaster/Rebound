using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarEngine.Texture;
using StarEngine.Draw;
using OpenTK;
namespace StarEngine.Resonance
{
    public delegate void Draw();
    public delegate void Update();
    public delegate void MouseEnter();
    public delegate void MouseLeave();
    public delegate void MouseMove();
    public delegate void FormLogic();

    public class UIForm
    {

        public Vector4 Col = new Vector4(1, 1, 1, 0.7f);

        public Draw Draw=null;
        public Update Update=null;
        public MouseEnter MouseEnter = null;
        public MouseLeave MouseLeave = null;
        public MouseMove MouseMove = null;
        public FormLogic FormLogic = null;

        public VTex2D CoreTex = null;

        public int X = 0, Y = 0;
        public int W = 0, H = 0;
        public string Text = "";

        public UIForm Root = null;
        public List<UIForm> Forms = new List<UIForm>();

        public int GX
        {
            get
            {
                int x = 0;
                if (Root != null)
                {
                    x = x + Root.GX;
                }
                x = x + X;
                return x;
            }
        }

        public int GY
        {
            get
            {
                int y = 0;
                if (Root != null)
                {
                    y = y + Root.GY;
                }
                y = y + Y;
                return y;
            }
        }

        public void DrawForm(VTex2D tex)
        {

            VPen.BlendMod = VBlend.Alpha;
            
            VPen.Rect(GX, GY, W, H, CoreTex, Col);

        }

        public UIForm Set(int x,int y,int w,int h,string text = "")
        {
            X = x;
            Y = y;
            W = w;
            H = h;
            Text = text;
            return this;
        }

        public UIForm SetImage(VTex2D tex)
        {
            CoreTex = tex;
            return this;
        }
    }

    public class ImageForm : UIForm
    {

        public ImageForm()
        {

            CoreTex = new VTex2D("Data\\UI\\Skin\\windowbg.png", LoadMethod.Single, true);

            void DrawFunc()
            {
                DrawForm(CoreTex);
            }

            Draw = DrawFunc;

        }

    }

}
