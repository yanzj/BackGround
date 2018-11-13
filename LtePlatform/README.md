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

## AngularJS框架

## CSS模块

## 帮助说明模块