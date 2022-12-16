(function(Global)
{
 "use strict";
 var FilesExchanger,Host,JsModules,SendFileJsModule,DownloadFileJsModule,DeviceNameJsModule,FirstConnectionForReceiveJsModule,FirstConnectionForSendJsModule,Client,FilesExchanger$Host_Templates,WebSharper,Concurrency,Remoting,AjaxRemotingProvider,UI,Var$1,Templating,Runtime,Server,ProviderBuilder,Handler,TemplateInstance,Client$1,Templates;
 FilesExchanger=Global.FilesExchanger=Global.FilesExchanger||{};
 Host=FilesExchanger.Host=FilesExchanger.Host||{};
 JsModules=Host.JsModules=Host.JsModules||{};
 SendFileJsModule=JsModules.SendFileJsModule=JsModules.SendFileJsModule||{};
 DownloadFileJsModule=JsModules.DownloadFileJsModule=JsModules.DownloadFileJsModule||{};
 DeviceNameJsModule=JsModules.DeviceNameJsModule=JsModules.DeviceNameJsModule||{};
 FirstConnectionForReceiveJsModule=JsModules.FirstConnectionForReceiveJsModule=JsModules.FirstConnectionForReceiveJsModule||{};
 FirstConnectionForSendJsModule=JsModules.FirstConnectionForSendJsModule=JsModules.FirstConnectionForSendJsModule||{};
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
 SendFileJsModule.Run$15$20=function(sendFileResponse)
 {
  return function(e)
  {
   var _;
   Concurrency.StartImmediate((_=null,Concurrency.Delay(function()
   {
    var filePath;
    filePath=e.Vars.Hole("filelocation").$1.Get();
    return Concurrency.TryWith(Concurrency.Delay(function()
    {
     return Concurrency.Bind((new AjaxRemotingProvider.New()).Async("FilesExchanger.Host:FilesExchanger.Host.InternalContext.SendFilesHandler.Send:-1938358458",[filePath]),function(a)
     {
      sendFileResponse.Set("Status: "+a);
      return Concurrency.Zero();
     });
    }),function(a)
    {
     if(a instanceof Global.Error)
      {
       sendFileResponse.Set("Status: unknown error");
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
    var filePath;
    filePath=e.Vars.Hole("filelocation").$1.Get();
    return Concurrency.TryWith(Concurrency.Delay(function()
    {
     return Concurrency.Bind((new AjaxRemotingProvider.New()).Async("FilesExchanger.Host:FilesExchanger.Host.InternalContext.SendFilesHandler.Send:-1938358458",[filePath]),function(a)
     {
      sendFileResponse.Set("Status: "+a);
      return Concurrency.Zero();
     });
    }),function(a)
    {
     if(a instanceof Global.Error)
      {
       sendFileResponse.Set("Status: unknown error");
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
 DownloadFileJsModule.Run$15$22=function(downloadResult)
 {
  return function(e)
  {
   var _;
   Concurrency.StartImmediate((_=null,Concurrency.Delay(function()
   {
    var filePath;
    filePath=e.Vars.Hole("targetfolderlocation").$1.Get();
    return Concurrency.Bind((new AjaxRemotingProvider.New()).Async("FilesExchanger.Host:FilesExchanger.Host.Handlers.DownloadFilesHandler.DownloadFile:-1938358458",[filePath]),function(a)
    {
     downloadResult.Set("Status: "+Global.String(a));
     return Concurrency.Zero();
    });
   })),null);
  };
 };
 DownloadFileJsModule.Run=function()
 {
  var downloadResult,b,D,_this,t,p,i;
  downloadResult=Var$1.Create$1("");
  return(b=(D=downloadResult.get_View(),(_this=(t=new ProviderBuilder.New$1(),(t.h.push(Handler.EventQ2(t.k,"download",function()
  {
   return t.i;
  },function(e)
  {
   var _;
   Concurrency.StartImmediate((_=null,Concurrency.Delay(function()
   {
    var filePath;
    filePath=e.Vars.Hole("targetfolderlocation").$1.Get();
    return Concurrency.Bind((new AjaxRemotingProvider.New()).Async("FilesExchanger.Host:FilesExchanger.Host.Handlers.DownloadFilesHandler.DownloadFile:-1938358458",[filePath]),function(a)
    {
     downloadResult.Set("Status: "+Global.String(a));
     return Concurrency.Zero();
    });
   })),null);
  })),t)),(_this.h.push({
   $:2,
   $0:"downloadfileresponse",
   $1:D
  }),_this))),(p=Handler.CompleteHoles(b.k,b.h,[["targetfolderlocation",0,null]]),(i=new TemplateInstance.New(p[1],FilesExchanger$Host_Templates.downloadfileform(p[0])),b.i=i,i))).get_Doc();
 };
 DeviceNameJsModule.Run$15$32=function(deviceName)
 {
  return function()
  {
   var _;
   Concurrency.StartImmediate((_=null,Concurrency.Delay(function()
   {
    return Concurrency.Bind((new AjaxRemotingProvider.New()).Async("FilesExchanger.Host:FilesExchanger.Host.Handlers.GenerateDeviceNameHandler.GetName:2013676698",[]),function(a)
    {
     deviceName.Set("Name: "+Global.String(a));
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
    return Concurrency.Bind((new AjaxRemotingProvider.New()).Async("FilesExchanger.Host:FilesExchanger.Host.Handlers.GenerateDeviceNameHandler.GetName:2013676698",[]),function(a)
    {
     deviceName.Set("Name: "+Global.String(a));
     return Concurrency.Zero();
    });
   })),null);
  })),t)),(_this.h.push({
   $:2,
   $0:"devicenameresponse",
   $1:D
  }),_this))),(p=Handler.CompleteHoles(b.k,b.h,[]),(i=new TemplateInstance.New(p[1],FilesExchanger$Host_Templates.generatedevicenameform(p[0])),b.i=i,i))).get_Doc();
 };
 FirstConnectionForReceiveJsModule.Run$15$28=function(connectionStatus)
 {
  return function()
  {
   var _;
   Concurrency.StartImmediate((_=null,Concurrency.Delay(function()
   {
    return Concurrency.TryWith(Concurrency.Delay(function()
    {
     return Concurrency.Bind((new AjaxRemotingProvider.New()).Async("FilesExchanger.Host:FilesExchanger.Host.Handlers.FirstConnectionForReceiveHandler.Connect:2013676698",[]),function(a)
     {
      connectionStatus.Set("Status: "+Global.String(a));
      return Concurrency.Zero();
     });
    }),function(a)
    {
     if(a instanceof Global.Error)
      {
       connectionStatus.Set("Status: disconnected");
       return Concurrency.Zero();
      }
     else
      throw a;
    });
   })),null);
  };
 };
 FirstConnectionForReceiveJsModule.Run=function()
 {
  var connectionStatus,b,T,_this,t,p,i;
  connectionStatus=Var$1.Create$1("");
  return(b=(T=connectionStatus.get_View(),(_this=(t=new ProviderBuilder.New$1(),(t.h.push(Handler.EventQ2(t.k,"waitconnection",function()
  {
   return t.i;
  },function()
  {
   var _;
   Concurrency.StartImmediate((_=null,Concurrency.Delay(function()
   {
    return Concurrency.TryWith(Concurrency.Delay(function()
    {
     return Concurrency.Bind((new AjaxRemotingProvider.New()).Async("FilesExchanger.Host:FilesExchanger.Host.Handlers.FirstConnectionForReceiveHandler.Connect:2013676698",[]),function(a)
     {
      connectionStatus.Set("Status: "+Global.String(a));
      return Concurrency.Zero();
     });
    }),function(a)
    {
     if(a instanceof Global.Error)
      {
       connectionStatus.Set("Status: disconnected");
       return Concurrency.Zero();
      }
     else
      throw a;
    });
   })),null);
  })),t)),(_this.h.push({
   $:2,
   $0:"testconnectionresponse",
   $1:T
  }),_this))),(p=Handler.CompleteHoles(b.k,b.h,[]),(i=new TemplateInstance.New(p[1],FilesExchanger$Host_Templates.tryconnectforreceiveform(p[0])),b.i=i,i))).get_Doc();
 };
 FirstConnectionForSendJsModule.Run$15$24=function(testConnectionResponse)
 {
  return function(e)
  {
   var _;
   Concurrency.StartImmediate((_=null,Concurrency.Delay(function()
   {
    var externalDeviceName;
    externalDeviceName=e.Vars.Hole("address").$1.Get();
    return Concurrency.TryWith(Concurrency.Delay(function()
    {
     return Concurrency.Bind((new AjaxRemotingProvider.New()).Async("FilesExchanger.Host:FilesExchanger.Host.Handlers.FirstConnectionForSendHandler.Connect:-1938358458",[externalDeviceName]),function(a)
     {
      testConnectionResponse.Set("Status: "+Global.String(a));
      return Concurrency.Zero();
     });
    }),function(a)
    {
     if(a instanceof Global.Error)
      {
       testConnectionResponse.Set("Status: disconnected");
       return Concurrency.Zero();
      }
     else
      throw a;
    });
   })),null);
  };
 };
 FirstConnectionForSendJsModule.Run=function()
 {
  var testConnectionResponse,b,T,_this,t,p,i;
  testConnectionResponse=Var$1.Create$1("");
  return(b=(T=testConnectionResponse.get_View(),(_this=(t=new ProviderBuilder.New$1(),(t.h.push(Handler.EventQ2(t.k,"tryconnect",function()
  {
   return t.i;
  },function(e)
  {
   var _;
   Concurrency.StartImmediate((_=null,Concurrency.Delay(function()
   {
    var externalDeviceName;
    externalDeviceName=e.Vars.Hole("address").$1.Get();
    return Concurrency.TryWith(Concurrency.Delay(function()
    {
     return Concurrency.Bind((new AjaxRemotingProvider.New()).Async("FilesExchanger.Host:FilesExchanger.Host.Handlers.FirstConnectionForSendHandler.Connect:-1938358458",[externalDeviceName]),function(a)
     {
      testConnectionResponse.Set("Status: "+Global.String(a));
      return Concurrency.Zero();
     });
    }),function(a)
    {
     if(a instanceof Global.Error)
      {
       testConnectionResponse.Set("Status: disconnected");
       return Concurrency.Zero();
      }
     else
      throw a;
    });
   })),null);
  })),t)),(_this.h.push({
   $:2,
   $0:"testconnectionresponse",
   $1:T
  }),_this))),(p=Handler.CompleteHoles(b.k,b.h,[["address",0,null]]),(i=new TemplateInstance.New(p[1],FilesExchanger$Host_Templates.tryconnectforsendform(p[0])),b.i=i,i))).get_Doc();
 };
 Client.DeviceNameModule=function()
 {
  return DeviceNameJsModule.Run();
 };
 Client.TestConnectionForReceiveModule=function()
 {
  return FirstConnectionForReceiveJsModule.Run();
 };
 Client.TestConnectionForSendModule=function()
 {
  return FirstConnectionForSendJsModule.Run();
 };
 Client.DownloadFileModule=function()
 {
  return DownloadFileJsModule.Run();
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
 FilesExchanger$Host_Templates.downloadfileform=function(h)
 {
  Templates.LoadLocalTemplates("main");
  return h?Templates.NamedTemplate("main",{
   $:1,
   $0:"downloadfileform"
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
 FilesExchanger$Host_Templates.tryconnectforreceiveform=function(h)
 {
  Templates.LoadLocalTemplates("main");
  return h?Templates.NamedTemplate("main",{
   $:1,
   $0:"tryconnectforreceiveform"
  },h):void 0;
 };
 FilesExchanger$Host_Templates.tryconnectforsendform=function(h)
 {
  Templates.LoadLocalTemplates("main");
  return h?Templates.NamedTemplate("main",{
   $:1,
   $0:"tryconnectforsendform"
  },h):void 0;
 };
}(self));
