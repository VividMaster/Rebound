using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarEngine.Resonance
{
    public class UI
    {

        public UIForm Root;


        public void Update()
        {

        }

        public void Render()
        {
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

        private void UpdateRenderList()
        {

            RenderList.Clear();

            AddNodeForward(RenderList, Root);
            

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
