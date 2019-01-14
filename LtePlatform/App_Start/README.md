# 应用启动配置

本目录定义了应用启动所必须的配置。
启动包括MVC和WebAPI程序的启动。

## 账号认证启动配置

## 类映射启动配置

## 注入依赖启动配置

分为3个文件。

### NinjectControllerFactory.cs

### NinjectDependencyResolver.cs

### KernelBindingOpertions.cs

## 前端打包配置

``` CSharp

    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));
        }
    }
```