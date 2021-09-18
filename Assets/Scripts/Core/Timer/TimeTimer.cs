using System.Collections;
using System.Collections.Generic;

namespace Core
{
	public abstract class TimeTimer : Timer
	{
		protected float m_interval;
		protected float m_startTime;
		public TimeTimer(TimerData data)
			: base(data)
		{
			m_interval = data.interval;
			m_startTime = 0;
		}
		public override void Reset(TimerData data)
		{
			m_interval = data.interval;
			m_startTime = 0;
			base.Reset(data);
		}
	}
	public class LoopTimer : TimeTimer
	{

		public LoopTimer(TimerData data)
			: base(data)
		{

		}
		public override TimerType Type => TimerType.enLoop;
		public override bool Tick(float dt)
		{
			m_startTime += dt;
			if (m_startTime >= m_interval)
			{
				m_body.Invoke();
				m_startTime = 0;
			}
			return base.Tick(dt);
		}
	}
	public class OnceTimer : TimeTimer
	{
		public OnceTimer(TimerData data)
			: base(data)
		{

		}
		public override TimerType Type => TimerType.enOnce;
		public override bool Tick(float dt)
		{
			m_startTime += dt;
			if (m_startTime >= m_interval)
			{
				m_body.Invoke();
				m_startTime = 0;
				return false;
			}
			return true;
		}
	}
}
