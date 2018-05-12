using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarEngine.Logic;
namespace StarEngine.State
{

    public class StarState
    {

        public Logic.Logics Cause = new Logics();

        public string Name
        {
            get;
            set;
        }

        public bool Running
        {
            get;
            set;
        }

        public StarState(string name = "")
        {
            Name = name;
            Running = false;
        }

        public virtual void InitState()
        {

        }

        public virtual void StartState()
        {

        }

        public virtual void UpdateState()
        {

        }

        public virtual void DrawState()
        {

        }

        public virtual void StopState()
        {

        }

        public virtual void ResumeState()
        {

        }

        public void InternalUpdate()
        {
          //  Cause.InternalUpdate();
        }


    }

}
