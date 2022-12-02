(function(Global)
{
 "use strict";
 var FilesExchanger,Host,Client,FilesExchanger$Host_Templates,WebSharper,Concurrency,Remoting,AjaxRemotingProvider,UI,Var$1,Templating,Runtime,Server,ProviderBuilder,Handler,TemplateInstance,Client$1,Templates;
 FilesExchanger=Global.FilesExchanger=Global.FilesExchanger||{};
 Host=FilesExchanger.Host=FilesExchanger.Host||{};
 Client=Host.Client=Host.Client||{};
 FilesExchanger$Host_Templates=Global.FilesExchanger$Host_Templates=Global.FilesExchanger$Host_Templates||{};
 WebSharper=Global.WebSharper;
 Concurrency=WebSharper&&WebSharper.Concurrency;
 Remoting=WebSharper&&WebSharper.Remoting;
 AjaxRemotingProvider=Remoting&&Remoting.AjaxRemotingProvider;
 UI=WebSharper&&WebSharper.UI;
 Var$1=UI&&UI.Var$1;
 Templating=UI&&UI.Templating;
 Runtime=Templating&&Templating.Runtime;
 Server=Runtime&&Runtime.Server;
 ProviderBuilder=Server&&Server.ProviderBuilder;
 Handler=Server&&Server.Handler;
 TemplateInstance=Server&&Server.TemplateInstance;
 Client$1=UI&&UI.Client;
 Templates=Client$1&&Client$1.Templates;
 Client.ConnectionModule$45$24=function(rvReversed)
 {
  return function()
  {
   var _;
   Concurrency.StartImmediate((_=null,Concurrency.Delay(function()
   {
    return Concurrency.Bind((new AjaxRemotingProvider.New()).Async("FilesExchanger.Host:FilesExchanger.Host.Server.SendByWebSocket:-595583518",[]),function(a)
    {
     rvReversed.Set(a);
     return Concurrency.Zero();
    });
   })),null);
  };
 };
 Client.ConnectionModule=function()
 {
  var rvReversed,b,C,_this,t,p,i;
  rvReversed=Var$1.Create$1("");
  return(b=(C=rvReversed.get_View(),(_this=(t=new ProviderBuilder.New$1(),(t.h.push(Handler.EventQ2(t.k,"tryconnect",function()
  {
   return t.i;
  },function()
  {
   var _;
   Concurrency.StartImmediate((_=null,Concurrency.Delay(function()
   {
    return Concurrency.Bind((new AjaxRemotingProvider.New()).Async("FilesExchanger.Host:FilesExchanger.Host.Server.SendByWebSocket:-595583518",[]),function(a)
    {
     rvReversed.Set(a);
     return Concurrency.Zero();
    });
   })),null);
  })),t)),(_this.h.push({
   $:2,
   $0:"connectionreserved",
   $1:C
  }),_this))),(p=Handler.CompleteHoles(b.k,b.h,[["texttoreverse",0,null]]),(i=new TemplateInstance.New(p[1],FilesExchanger$Host_Templates.connectionform(p[0])),b.i=i,i))).get_Doc();
 };
 Client.GetFileModule$32$22=function(rvReversed)
 {
  return function()
  {
   var _;
   Concurrency.StartImmediate((_=null,Concurrency.Delay(function()
   {
    return Concurrency.Bind((new AjaxRemotingProvider.New()).Async("FilesExchanger.Host:FilesExchanger.Host.Server.DoSomething:-409756626",["hello"]),function(a)
    {
     rvReversed.Set(a);
     return Concurrency.Zero();
    });
   })),null);
  };
 };
 Client.GetFileModule=function()
 {
  var rvReversed,b,t,p,i;
  rvReversed=Var$1.Create$1("");
  return(b=(t=new ProviderBuilder.New$1(),(t.h.push(Handler.EventQ2(t.k,"download",function()
  {
   return t.i;
  },function()
  {
   var _;
   Concurrency.StartImmediate((_=null,Concurrency.Delay(function()
   {
    return Concurrency.Bind((new AjaxRemotingProvider.New()).Async("FilesExchanger.Host:FilesExchanger.Host.Server.DoSomething:-409756626",["hello"]),function(a)
    {
     rvReversed.Set(a);
     return Concurrency.Zero();
    });
   })),null);
  })),t)),(p=Handler.CompleteHoles(b.k,b.h,[]),(i=new TemplateInstance.New(p[1],FilesExchanger$Host_Templates.getfileform(p[0])),b.i=i,i))).get_Doc();
 };
 Client.SendFileModule$18$20=function(rvReversed)
 {
  return function()
  {
   var _;
   Concurrency.StartImmediate((_=null,Concurrency.Delay(function()
   {
    rvReversed.Set("Try to send message");
    return Concurrency.Zero();
   })),null);
  };
 };
 Client.SendFileModule=function()
 {
  var rvReversed,b,t,p,i;
  rvReversed=Var$1.Create$1("");
  return(b=(t=new ProviderBuilder.New$1(),(t.h.push(Handler.EventQ2(t.k,"onsend",function()
  {
   return t.i;
  },function()
  {
   var _;
   Concurrency.StartImmediate((_=null,Concurrency.Delay(function()
   {
    rvReversed.Set("Try to send message");
    return Concurrency.Zero();
   })),null);
  })),t)),(p=Handler.CompleteHoles(b.k,b.h,[["texttoreverse",0,null]]),(i=new TemplateInstance.New(p[1],FilesExchanger$Host_Templates.sendfileform(p[0])),b.i=i,i))).get_Doc();
 };
 FilesExchanger$Host_Templates.connectionform=function(h)
 {
  Templates.LoadLocalTemplates("main");
  return h?Templates.NamedTemplate("main",{
   $:1,
   $0:"connectionform"
  },h):void 0;
 };
 FilesExchanger$Host_Templates.getfileform=function(h)
 {
  Templates.LoadLocalTemplates("main");
  return h?Templates.NamedTemplate("main",{
   $:1,
   $0:"getfileform"
  },h):void 0;
 };
 FilesExchanger$Host_Templates.sendfileform=function(h)
 {
  Templates.LoadLocalTemplates("main");
  return h?Templates.NamedTemplate("main",{
   $:1,
   $0:"sendfileform"
  },h):void 0;
 };
}(self));
