﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarEngine.Font;
using StarEngine.Input;
using StarEngine.Logic;

namespace StarEngine.Resonance
{
    public class UI
    {

        public static UIForm TopForm = null;
        public Logics Logics = new Logics();
        public Logics Graphics = new Logics();

        public static Font.VFont Font = null;

        public UIForm Root;

        public UI()
        {
            InitUI();
            for(int i = 0; i < 32; i++)
            {
                Pressed[i] = null;
            }
        }

        public void InitUI()
        {
            Font = new VFont("data/font/times.ttf.vf");
        }

        public bool FirstMouse = true;
        public static int MX,MY,MXD,MYD;

        public void Update()
        {

            Logics.SmartUpdate();

            if (FirstMouse)
            {
                MX = VInput.MX;
                MY = VInput.MY;
                FirstMouse = false;
            }

            MXD = VInput.MX - MX;
            MYD = VInput.MY - MY;
            MX = VInput.MX;
            MY = VInput.MY;

            UpdateUpdateList();

            int f = 0;
            var top = GetTopForm(MX, MY);

            if (top != null)
            {

                if (TopForm != top)
                {
                    if (TopForm != null)
                    {

                        if (TopForm != Pressed[0])
                        {
                            TopForm.MouseLeave?.Invoke();
                        }
                    }


                    top.MouseEnter?.Invoke();

                }


            }
            if (top != null)
            {
                if (top == TopForm)
                {

                    top.MouseMove?.Invoke(MX - top.GX, MY - top.GY, MXD, MYD);

                }
            }
            if (top == null)
            {
                if (TopForm != null)
                {
                    if (TopForm != Pressed[0])
                    {
                        TopForm.MouseLeave?.Invoke();
                    }
                }
            }
            TopForm = top;


            if (VInput.MB[0])
            {

                if (TopForm != null)
                {
                    if (Pressed[0] == null)
                    {
                        TopForm.MouseDown?.Invoke(0);
                        Pressed[0] = TopForm;
                    }
                    else if (Pressed[0] == TopForm)
                    {
                        TopForm.MousePressed?.Invoke(0);
                    }
                    else if (Pressed[0] != TopForm)
                    {
                        Pressed[0].MousePressed?.Invoke(0);

                    }
                }
                else
                {
                    if (Pressed[0] != null)
                    {
                        Pressed[0].MousePressed?.Invoke(0);
                    }
                }

            }
            else
            {
                //Console.WriteLine("Wop");
                if (Pressed[0] != null)
                {
                    if (Pressed[0].InBounds(MX, MY) == false)
                    {
                        Pressed[0].MouseLeave?.Invoke();
                    }
        
                    Pressed[0].MouseUp?.Invoke(0);
                    Pressed[0] = null;
                }
            }


        }
        public UIForm[] Pressed = new UIForm[32];
        private UIForm GetTopForm(int mx, int my)
        {
            foreach (var form in UpdateList)
            {

                if (form.InBounds(mx, my))
                {
                    return form;
                    
                }

            }
            return null;
        }

        public void Render()
        {
            Graphics.SmartUpdate();

            UpdateRenderList();

            foreach (var form in RenderList)
            {

                if (form.Draw != null)
                {
                    form.Draw();
                }

            }
        }

        public List<UIForm> UpdateList = new List<UIForm>();
        public List<UIForm> RenderList = new List<UIForm>();

        private void UpdateUpdateList()
        {
            UpdateList.Clear();

            AddNodeBackward(UpdateList, Root);



        }

        private void UpdateRenderList()
        {

            RenderList.Clear();

            AddNodeForward(RenderList, Root);
            

        }

        private void AddNodeBackward(List<UIForm> forms,UIForm form)
        {
            int fc = form.Forms.Count;
            if (fc > 0)
            {
                while (true)
                {
                    fc--;
                    var af = form.Forms[fc];
                    AddNodeBackward(forms, af);
                    if (fc == 0) break;
                }
            }
            forms.Add(form);

        }

        private void AddNodeForward(List<UIForm> forms,UIForm form)
        {

            RenderList.Add(form);
            foreach(var nf in form.Forms)
            {
                AddNodeForward(forms, nf);
            }

        }
            

    }


}
