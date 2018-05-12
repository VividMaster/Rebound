using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarEngine.State
{
    public delegate void Act();
    public delegate bool Until();
    public delegate void FlowInit();
    public delegate bool FlowLogic();
    public delegate bool When();
    public delegate bool Unless();
    public class StarState
    {

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

        public virtual void In(Act Action,Until Until)
        {
            var ai = new ActInfo();
            ai.Action = Action;
            ai.Until = Until;
            ai.NoTime = true;
            Acts.Add(ai);
        }
        public virtual void In(int ms, Act Action, Until Until)
        {
            var ai = new ActInfo();
            ai.Action = Action;
            ai.When = Environment.TickCount + ms;
            ai.Until = Until;
            Acts.Add(ai);
        }
        public virtual void In(int ms, Act Action, bool once = true, int forms = 0)
        {

            var ai = new ActInfo();
            ai.Action = Action;
            ai.When = Environment.TickCount + ms;
            ai.For = forms;
            ai.Once = once;
            Acts.Add(ai);
        }
        public void Flow(FlowInit init,FlowLogic logic,FlowLogic endLogic=null)
        {
            var flow = new FlowInfo();
            flow.Init = init;
            flow.Logic = logic;
            flow.EndLogic = endLogic;
            Flows.Add(flow);
        }
        public void InternalUpdate()
        {
            var rw = new List<WhenInfo>();
            foreach(var w in Whens)
            {
                if (w.When())
                {
                    if (w.Unless != null)
                    {
                        if (w.Unless())
                        {
                            rw.Add(w);
                        }
                        else
                        {
                            w.Action();
                            rw.Add(w);
                        }
                    }
                    else
                    {
                        w.Action();
                        rw.Add(w);
                    }
                }
            }
            foreach(var w in rw)
            {
                Whens.Remove(w);
            }
            if (Flows.Count > 0)
            {
                var ff = Flows[0];
                if (ff.Begun == false)
                {
                    if (ff.Init != null)
                    {
                        ff.Init();
                    }
                    ff.Begun = true;
                }
                if (ff.Logic())
                {
                    if (ff.EndLogic != null)
                    {
                        ff.EndLogic();
                    }
                    Flows.Remove(ff);
                }

            }
            List<ActInfo> rem = new List<ActInfo>();
            int ms = Environment.TickCount;
            foreach(var a in Acts)
            {

                if (a.NoTime)
                {
                    a.Action();
                    if (a.Until())
                    {
                        rem.Add(a);
                        continue;
                    }
                }
                if (a.Until != null)
                {
                    if (ms > (a.When))
                    {
                        a.Action();
                        if (a.Until())
                        {
                            rem.Add(a);
                            continue;
                        }
                    }

                }
                if (a.Running)
                {
                    if (ms > (a.When + a.For))
                    {
                        Console.WriteLine("Done");
                        rem.Add(a);
                        continue;
                    }
                }
                else
                {
                    if (ms > a.When)
                    {
                        a.Action();
                        if (a.Once)
                        {
                            rem.Add(a);
                        }
                        else
                        {
                            a.Running = true;
                        }
                    }
                }

            }
            foreach(var a in rem)
            {
                Acts.Remove(a);
            }
        }
        public void When(When when, Act action, Unless unless = null)
        {
            WhenInfo wi = new WhenInfo();
            wi.When = when;
            wi.Action = action;
            wi.Unless = unless;
            Whens.Add(wi);
        }
        public List<FlowInfo> Flows = new List<FlowInfo>();
        private List<ActInfo> Acts = new List<ActInfo>();
        public List<WhenInfo> Whens = new List<WhenInfo>();

    }
    public class FlowInfo
    {
        public FlowInit Init;
        public FlowLogic Logic;
        public FlowLogic EndLogic = null;
        public bool Begun = false;
    }
    public class ActInfo
    {
        public Act Action;
        public int When;
        public int For;
        public int Begun;
    public bool Running = false;
        public bool Once = true;
        public Until Until = null;
        public bool NoTime = false;
    }
    public class WhenInfo
    {
        public When When = null;
        public Act Action = null;
        public Unless Unless = null;
    }
}
