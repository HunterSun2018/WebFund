# How to develop a Web Application 

## 1. Install dotnet core SDK 

```shell
sudo apt install dotnet-sdk-3.1
```

## 2. Create a MVC Web App

```shell
dotnet new mvc -o WebApp
```

## 3. Add NuGet Package

```shell
dotnet tool install --global dotnet-ef
dotnet tool install --global dotnet-aspnet-codegenerator
dotnet add package Microsoft.EntityFrameworkCore.SQLite
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

## 4. Add data model class

create a file Models/Fund.cs

```c#
using System;
using System.ComponentModel.DataAnnotations;

namespace WebFund
{
    public class Fund
    {
        public int Id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public double value { get; set; }

        [DataType(DataType.Date)]
        public DateTime UpdateDate { get; set; }
        public double ChangeInDay { get; set; }
        public double ChangeInWeek { get; set; }
        public double ChangeInMonth { get; set; }

    }
}
```



## 5. Create a database context 

in file Data/FundContext.cs 

```c#
using Microsoft.EntityFrameworkCore;
using WebFund.Models;

namespace WebFund.Data
{
    public class FundContext : DbContext
    {
        public FundContext(DbContextOptions<FundContext> options)
            : base(options)
        {

        }

        public DbSet<Fund> Funds { get; set; }
    }
}
```



## 6. Register database context 

in file startup.cs

```c#
using MvcMovie.Data;
using Microsoft.EntityFrameworkCore;

public void ConfigureServices(IServiceCollection services)
{
    services.AddControllersWithViews();
    
    services.AddDbContext<MvcMovieContext>(options =>
            options.UseSqlite(Configuration.GetConnectionString("FundContext")));
}
```

## 7. Add database connection string in file appsettings.json 

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "MvcMovieContext": "Data Source=WebFund.db"
  }
}
```



## 8. Create a MVC page using scaffolding

```shell
dotnet aspnet-codegenerator controller -name MoviesController -m Movie -dc MvcMovieContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
```

下表详细说明了 ASP.NET Core 代码生成器参数：

| 参数                       | 说明                                                 |
| -------------------------- | ---------------------------------------------------- |
| -m                         | 模型的名称。                                         |
| -dc                        | 数据上下文。                                         |
| -udl                       | 使用默认布局。                                       |
| --relativeFolderPath       | 用于创建文件的相对输出文件夹路径。                   |
| --useDefaultLayout         | 应为视图使用默认布局。                               |
| --referenceScriptLibraries | 向“编辑”和“创建”页面添加 `_ValidationScriptsPartial` |

## 9. Initial Migration DB

```shell
dotnet ef migrations add InitialCreate
dotnet ef database update
#Done. To undo this action, use 'ef migrations remove'
```



## 10. add package for crawling the web page into DB

```shell
dotnet add package HtmlAgilityPack --version 1.11.21
dotnet add package System.Text.Encoding.CodePages
```

## 11. Register Provider for 'gb2132' on initialization

```c#
using System.Text;

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
```

