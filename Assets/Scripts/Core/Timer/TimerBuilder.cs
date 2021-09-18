using System.Diagnostics;
using System.Collections.Generic;

namespace Core
{
    public struct TimerData
    {
        public TimerType type;
        public float interval;
        public Timer.KeepContinue keep;
        public Timer.KeepContinue condition;
        public Timer.ExecuteBody body;
        public Timer.ExecuteBody end;
        public object objthis;
    }
    public class TimerBuilder
    {
        List<Timer> m_timers = new List<Timer>();
        List<Timer> m_queues = new List<Timer>();
        List<Timer> m_timerPool = new List<Timer>();
        List<Timer> m_removes = new List<Timer>();

        private bool CheckPoolHasTimer(Timer timer)
        {
            for (int i = 0; i < m_timerPool.Count; i++)
            {
                if (m_timerPool[i].GetHashCode() == timer.GetHashCode())
                {
                    return true;
                }
            }
            return false;
        }

        public void OnFixedUpdate()
        {
            var dt = UnityEngine.Time.deltaTime;
            if(m_queues.Count > 0)
            {
                m_timers.AddRange(m_queues);
                m_queues.Clear();
            }
            for (int i=0; i<m_timers.Count; i++)
            {
                var timer = m_timers[i];
                if(!timer.IsFinished)
                {
                    if (!timer.Tick(dt))
                    {
                        timer.OnFinish();
                        m_removes.Add(timer);
                    }
                }
                else
                {
                    m_removes.Add(timer);
                }
            }
            for (int i=0; i<m_removes.Count; i++)
            {
                var timer = m_removes[i];
                m_timers.Remove(timer);
                //if(m_timerPool.Any(v=>v.GetHashCode() == timer.GetHashCode()))
                if (CheckPoolHasTimer(timer))
                {
                    
                }
                else
                {
                    m_timerPool.Add(timer);
                }
                timer.Stop();
            }
            m_removes.Clear();
        }
        public void OnQuit()
        {
            for(int i=0;i<m_timers.Count; i++)
            {
                var timer = m_timers[i];
                timer.Stop();
            }
            for(int i=0; i<m_queues.Count; i++)
            {
                m_queues[i].Stop();
            }
        }
        public void OnDestroy()
        {
            
        }
        public void ForceStopTimer(Timer timer, object obj)
        {
            if (null != timer.objthis && timer.objthis.GetHashCode() != obj.GetHashCode())
            {
                return;
            }
            timer.Stop();
        }
        /*
         bool ExecuteKeeping();
         void ExecuteBody();
         void ExecuteEnd();
         bool ExecuteCondition();

         *LoopTimer, new TimerData{type = Inf.TimerType.enLoop, interval = 1f, keep = this.ExecuteKeeping, body = this.ExecuteBody, [end = this.ExecuteEnd] }

         *OnceTimer, new TimerData{type = Inf.TimerType.enOnce, interval = 1f, body = this.ExecuteBody, [end = this.ExecuteEnd] }

         *ConditionLoopTimer, new TimerData{type = Inf.TimerType.enConditionLoop, condition = this.ExecuteCondition, keep = this.ExecuteKeeping, body = this.ExecuteBody, [end = this.ExecuteEnd] }

         *ConditionOnceTimer, new TimerData{type = Inf.TimerType.enConditionOnce, condition = this.ExecuteCondition, body = this.ExecuteBody, [end = this.ExecuteEnd] }
         */
        public Timer CreateTimer(TimerData data)
        {
            Timer timer = null;
            for(int i=0; i<m_timerPool.Count; i++)
            {
                if(m_timerPool[i].Type == data.type)
                {
                    timer = m_timerPool[i];
                    m_timerPool.Remove(timer);
                    break;
                }
            }
            if (null != timer)
            {
                timer.Reset(data);
                m_queues.Add(timer);
                return timer;
            }
            switch(data.type)
            {
                case TimerType.enLoop:
                    timer = new LoopTimer(data);
                    break;
                case TimerType.enOnce:
                    timer = new OnceTimer(data);
                    break;
                case TimerType.enConditionLoop:
                    timer = new ConditionLoopTimer(data);
                    break;
                case TimerType.enConditionOnce:
                    timer = new ConditionOnceTimer(data);
                    break;
                default:
                    break;
            }
            if(null != timer)
            {
                m_queues.Add(timer);
                return timer;
            }
            return null;
        }
    }
}
