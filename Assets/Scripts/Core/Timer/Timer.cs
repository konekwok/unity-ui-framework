using System.Collections;
using System.Collections.Generic;

namespace Core
{
    public enum TimerType
    {
        enOnce,
        enLoop,
        enConditionLoop,
        enConditionOnce,
    }
    public abstract class Timer
    {
        public delegate bool KeepContinue();
        public delegate void ExecuteBody();

        protected KeepContinue m_continue;
        protected ExecuteBody m_body;
        protected ExecuteBody m_end;
        // test
        public object objthis;
        public string objName;
        public bool isNew;

        bool m_isStart;
        public bool IsFinished { get { return !m_isStart; } }
        public Timer(TimerData data)
        {
            isNew = true;
            objthis = data.objthis;
            objName = objthis.ToString();

            m_isStart = true;
            m_continue = data.keep;
            m_body = data.body;
            m_end = data.end;
        }
        public abstract TimerType Type { get; }
        public virtual bool Tick(float dt)
        {
            return m_continue.Invoke();
        }
        public virtual void Reset(TimerData data)
        {
            isNew = false;
            objthis = data.objthis;
            objName = objthis.ToString();

            m_isStart = true;
            m_body = data.body;
            m_end = data.end;
            m_continue = data.keep;
        }
        public void Stop()
        {
            m_isStart = false;
            objthis = null;
        }
        public void OnFinish()
        {
            m_isStart = false;
            m_end?.Invoke();
        }
    }
}