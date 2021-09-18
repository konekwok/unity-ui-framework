using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
	public abstract class ConditionTimer : Timer
	{
		protected KeepContinue m_condition;
		public ConditionTimer(TimerData data)
			:base(data)
		{
            m_condition = data.condition;
		}
        public override void Reset(TimerData data)
        {
            m_condition = data.condition;
            base.Reset(data);
        }
    }
	public class ConditionLoopTimer : ConditionTimer
	{
		public ConditionLoopTimer(TimerData data)
			: base(data)
		{
		}
		public override TimerType Type => TimerType.enConditionLoop;
		public override bool Tick(float dt)
		{
			if (m_condition.Invoke())
			{
				m_body.Invoke();
			}
			return base.Tick(dt);
		}
		
	}
	public class ConditionOnceTimer : ConditionTimer
	{
		public ConditionOnceTimer(TimerData data)
			: base(data)
		{
		}
		public override TimerType Type => TimerType.enConditionOnce;
		public override bool Tick(float dt)
		{
			if (m_condition.Invoke())
			{
				m_body.Invoke();
				return false;
			}
			return true;
		}
	}
}
