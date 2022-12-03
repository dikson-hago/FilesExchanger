(function(Global)
{
 "use strict";
 var FilesExchanger,Host,JsModules,Store,IpAddressStore,SC$1,SendFileJsModule,GetFileJsModule,TestConnectionJsModule,DeviceNameJsModule,Client,FilesExchanger$Host_Templates,WebSharper,Concurrency,Remoting,AjaxRemotingProvider,UI,Var$1,Templating,Runtime,Server,ProviderBuilder,Handler,TemplateInstance,Client$1,Templates;
 FilesExchanger=Global.FilesExchanger=Global.FilesExchanger||{};
 Host=FilesExchanger.Host=FilesExchanger.Host||{};
 JsModules=Host.JsModules=Host.JsModules||{};
 Store=JsModules.Store=JsModules.Store||{};
 IpAddressStore=Store.IpAddressStore=Store.IpAddressStore||{};
 SC$1=Global.StartupCode$FilesExchanger_Host$IpAddressStore=Global.StartupCode$FilesExchanger_Host$IpAddressStore||{};
 SendFileJsModule=JsModules.SendFileJsModule=JsModules.SendFileJsModule||{};
 GetFileJsModule=JsModules.GetFileJsModule=JsModules.GetFileJsModule||{};
 TestConnectionJsModule=JsModules.TestConnectionJsModule=JsModules.TestConnectionJsModule||{};
 DeviceNameJsModule=JsModules.DeviceNameJsModule=JsModules.DeviceNameJsModule||{};
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
 IpAddressStore.externalIpAddress=function()
 {
  SC$1.$cctor();
  return SC$1.externalIpAddress;
 };
 IpAddressStore.set_externalIpAddress=function($1)
 {
  SC$1.$cctor();
  SC$1.externalIpAddress=$1;
 };
 SC$1.$cctor=function()
 {
  SC$1.$cctor=Global.ignore;
  SC$1.externalIpAddress="";
 };
 SendFileJsModule.Run$16$20=function(sendFileResponse)
 {
  return function(e)
  {
   var _;
   Concurrency.StartImmediate((_=null,Concurrency.Delay(function()
   {
    return Concurrency.TryWith(Concurrency.Delay(function()
    {
     var filePath;
     filePath=e.Vars.Hole("filelocation").$1.Get();
     return Concurrency.Bind((new AjaxRemotingProvider.New()).Async("FilesExchanger.Host:FilesExchanger.Host.InternalContext.SendFilesHandler.Send:-1323212191",[IpAddressStore.externalIpAddress(),filePath]),function()
     {
      sendFileResponse.Set("ok");
      return Concurrency.Zero();
     });
    }),function(a)
    {
     if(a instanceof Global.Error)
      {
       sendFileResponse.Set("error");
       return Concurrency.Zero();
      }
     else
      throw a;
    });
   })),null);
  };
 };
 SendFileJsModule.Run=function()
 {
  var sendFileResponse,b,S,_this,t,p,i;
  sendFileResponse=Var$1.Create$1("");
  return(b=(S=sendFileResponse.get_View(),(_this=(t=new ProviderBuilder.New$1(),(t.h.push(Handler.EventQ2(t.k,"onsend",function()
  {
   return t.i;
  },function(e)
  {
   var _;
   Concurrency.StartImmediate((_=null,Concurrency.Delay(function()
   {
    return Concurrency.TryWith(Concurrency.Delay(function()
    {
     var filePath;
     filePath=e.Vars.Hole("filelocation").$1.Get();
     return Concurrency.Bind((new AjaxRemotingProvider.New()).Async("FilesExchanger.Host:FilesExchanger.Host.InternalContext.SendFilesHandler.Send:-1323212191",[IpAddressStore.externalIpAddress(),filePath]),function()
     {
      sendFileResponse.Set("ok");
      return Concurrency.Zero();
     });
    }),function(a)
    {
     if(a instanceof Global.Error)
      {
       sendFileResponse.Set("error");
       return Concurrency.Zero();
      }
     else
      throw a;
    });
   })),null);
  })),t)),(_this.h.push({
   $:2,
   $0:"sendfileresponse",
   $1:S
  }),_this))),(p=Handler.CompleteHoles(b.k,b.h,[["filelocation",0,null]]),(i=new TemplateInstance.New(p[1],FilesExchanger$Host_Templates.sendfileform(p[0])),b.i=i,i))).get_Doc();
 };
 GetFileJsModule.Run$14$22=function(rvReversed)
 {
  return function()
  {
   var _;
   Concurrency.StartImmediate((_=null,Concurrency.Delay(function()
   {
    rvReversed.Set("res");
    return Concurrency.Zero();
   })),null);
  };
 };
 GetFileJsModule.Run=function()
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
    rvReversed.Set("res");
    return Concurrency.Zero();
   })),null);
  })),t)),(p=Handler.CompleteHoles(b.k,b.h,[]),(i=new TemplateInstance.New(p[1],FilesExchanger$Host_Templates.getfileform(p[0])),b.i=i,i))).get_Doc();
 };
 TestConnectionJsModule.Run$16$24=function(rvReversed)
 {
  return function(e)
  {
   var _;
   Concurrency.StartImmediate((_=null,Concurrency.Delay(function()
   {
    var address;
    address=e.Vars.Hole("address").$1.Get();
    return Concurrency.TryWith(Concurrency.Delay(function()
    {
     return Concurrency.Bind((new AjaxRemotingProvider.New()).Async("FilesExchanger.Host:FilesExchanger.Host.Handlers.GenerateDeviceNameHandler.GetName:-710314813",[]),function(a)
     {
      return Concurrency.Bind((new AjaxRemotingProvider.New()).Async("FilesExchanger.Host:FilesExchanger.Host.Handlers.TestConnectHandler.TestConnect:118170792",[address,a]),function(a$1)
      {
       IpAddressStore.set_externalIpAddress(a$1);
       rvReversed.Set("connected");
       return Concurrency.Zero();
      });
     });
    }),function(a)
    {
     if(a instanceof Global.Error)
      {
       rvReversed.Set("disconnected");
       return Concurrency.Zero();
      }
     else
      throw a;
    });
   })),null);
  };
 };
 TestConnectionJsModule.Run=function()
 {
  var rvReversed,b,C,_this,t,p,i;
  rvReversed=Var$1.Create$1("");
  return(b=(C=rvReversed.get_View(),(_this=(t=new ProviderBuilder.New$1(),(t.h.push(Handler.EventQ2(t.k,"tryconnect",function()
  {
   return t.i;
  },function(e)
  {
   var _;
   Concurrency.StartImmediate((_=null,Concurrency.Delay(function()
   {
    var address;
    address=e.Vars.Hole("address").$1.Get();
    return Concurrency.TryWith(Concurrency.Delay(function()
    {
     return Concurrency.Bind((new AjaxRemotingProvider.New()).Async("FilesExchanger.Host:FilesExchanger.Host.Handlers.GenerateDeviceNameHandler.GetName:-710314813",[]),function(a)
     {
      return Concurrency.Bind((new AjaxRemotingProvider.New()).Async("FilesExchanger.Host:FilesExchanger.Host.Handlers.TestConnectHandler.TestConnect:118170792",[address,a]),function(a$1)
      {
       IpAddressStore.set_externalIpAddress(a$1);
       rvReversed.Set("connected");
       return Concurrency.Zero();
      });
     });
    }),function(a)
    {
     if(a instanceof Global.Error)
      {
       rvReversed.Set("disconnected");
       return Concurrency.Zero();
      }
     else
      throw a;
    });
   })),null);
  })),t)),(_this.h.push({
   $:2,
   $0:"connectionreserved",
   $1:C
  }),_this))),(p=Handler.CompleteHoles(b.k,b.h,[["address",0,null]]),(i=new TemplateInstance.New(p[1],FilesExchanger$Host_Templates.tryconnectform(p[0])),b.i=i,i))).get_Doc();
 };
 DeviceNameJsModule.Run$15$32=function(deviceName)
 {
  return function()
  {
   var _;
   Concurrency.StartImmediate((_=null,Concurrency.Delay(function()
   {
    return Concurrency.Bind((new AjaxRemotingProvider.New()).Async("FilesExchanger.Host:FilesExchanger.Host.Handlers.GenerateDeviceNameHandler.GetName:-710314813",[]),function(a)
    {
     deviceName.Set(a);
     return Concurrency.Zero();
    });
   })),null);
  };
 };
 DeviceNameJsModule.Run=function()
 {
  var deviceName,b,D,_this,t,p,i;
  deviceName=Var$1.Create$1("");
  return(b=(D=deviceName.get_View(),(_this=(t=new ProviderBuilder.New$1(),(t.h.push(Handler.EventQ2(t.k,"generatedevicename",function()
  {
   return t.i;
  },function()
  {
   var _;
   Concurrency.StartImmediate((_=null,Concurrency.Delay(function()
   {
    return Concurrency.Bind((new AjaxRemotingProvider.New()).Async("FilesExchanger.Host:FilesExchanger.Host.Handlers.GenerateDeviceNameHandler.GetName:-710314813",[]),function(a)
    {
     deviceName.Set(a);
     return Concurrency.Zero();
    });
   })),null);
  })),t)),(_this.h.push({
   $:2,
   $0:"devicename",
   $1:D
  }),_this))),(p=Handler.CompleteHoles(b.k,b.h,[]),(i=new TemplateInstance.New(p[1],FilesExchanger$Host_Templates.generatedevicenameform(p[0])),b.i=i,i))).get_Doc();
 };
 Client.DeviceNameModule=function()
 {
  return DeviceNameJsModule.Run();
 };
 Client.TestConnectionModule=function()
 {
  return TestConnectionJsModule.Run();
 };
 Client.GetFileModule=function()
 {
  return GetFileJsModule.Run();
 };
 Client.SendFileModule=function()
 {
  return SendFileJsModule.Run();
 };
 FilesExchanger$Host_Templates.sendfileform=function(h)
 {
  Templates.LoadLocalTemplates("main");
  return h?Templates.NamedTemplate("main",{
   $:1,
   $0:"sendfileform"
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
 FilesExchanger$Host_Templates.tryconnectform=function(h)
 {
  Templates.LoadLocalTemplates("main");
  return h?Templates.NamedTemplate("main",{
   $:1,
   $0:"tryconnectform"
  },h):void 0;
 };
 FilesExchanger$Host_Templates.generatedevicenameform=function(h)
 {
  Templates.LoadLocalTemplates("main");
  return h?Templates.NamedTemplate("main",{
   $:1,
   $0:"generatedevicenameform"
  },h):void 0;
 };
}(self));
