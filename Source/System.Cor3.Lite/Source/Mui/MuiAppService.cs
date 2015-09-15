/* oio * 8/3/2015 * Time: 6:39 AM */

using System;
using System.ComponentModel.Design;
namespace Mui
{
  abstract public class MuiAppService<TClient> : MuiAppService
    where TClient:IMui
  {
    protected internal TClient Client { get { return (TClient)Parent; } }
    
    static public void RegisterAll(TClient client)
    {
      if (client.Services==null) return;
      for (var i=0; i < client.Services.Count; i++)
      {
        client.Services[i].Parent = client;
        client.Services[i].Register();
      }
    }
    static public void UnregisterAll(TClient client)
    {
      if (client.Services==null) return;
      for (var i=0; i < client.Services.Count; i++)
      {
        client.Services[i].Unregister();
        client.Services[i].Parent = null;
      }
    }
  }
  abstract public class MuiAppService
  {
    protected internal IMui Parent { get; set; }
    abstract public void Register();
    virtual public void Unregister(){ Parent = null; }
    
    static public void RegisterAll(IMui client)
    {
      if (client.Services==null) return;
      for (var i=0; i < client.Services.Count; i++)
      {
        client.Services[i].Parent = client;
        client.Services[i].Register();
      }
    }
    static public void UnregisterAll(IMui client)
    {
      if (client.Services==null) return;
      for (var i=0; i < client.Services.Count; i++)
      {
        client.Services[i].Unregister();
        client.Services[i].Parent = null;
      }
    }
  }
}








