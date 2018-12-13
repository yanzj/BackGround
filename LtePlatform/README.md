# LTE网络优化平台的主项目

## 应用程序启动模块

### 启动程序

#### Startup.cs

``` CSharp
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
```

#### Startup.Auth.cs

``` CSharp
    public partial class Startup
    {
        // 使应用程序可使用 OAuthAuthorization。然后，你便可以保护 Web API
        static Startup()
        {
            PublicClientId = "web";

            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                AuthorizeEndpointPath = new PathString("/Account/Authorize"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };
        }

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // 有关配置身份验证的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            ......
        }
    }
```

## Restful API 接口定义

本项目的主体部分，定义了各种数据的查询和操作接口。

## AngularJS框架

随着前后端分离的深入开展，本项目逐渐退化为后端项目，保留部分前端代码作为数据导入界面。

## CSS模块

配合前端框架的CSS代码。

## 帮助说明模块

对接口的说明文档模块。