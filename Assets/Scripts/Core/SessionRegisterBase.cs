using System.Collections.Generic;

public class SessionContent
{

}
public class SessionRegisterBase
{
    protected SessionRegisterBase()
    {
        m_dics = new Dictionary<int, SessionContent>();
    }
    protected Dictionary<int, SessionContent> m_dics;
    public void Register<T>(int sessionId) where T : SessionContent, new()
    {
        T session = new T();
        m_dics.Add(sessionId, session);
    }
    public T Get<T>(int sessionId) where T : SessionContent
    {
        if(m_dics.TryGetValue(sessionId, out SessionContent session))
        {
            return (T)session;
        }
        return null;
    }
}
