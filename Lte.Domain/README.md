# 基础数据设施-业务层

主要包括基本数据类型定义、外部数据访问API、Excel和CSV数据格式定义等。

## 公共参数定义和变换

### 枚举类型定义

由于ORM的出现，数据库支持枚举类型。
我们根据无线通信的业务规则定义了大量的枚举类型。
例如如下的定义：

```C#
    [EnumTypeDescription(typeof(DemandLevel), LevelB)]
    public enum DemandLevel : byte
    {
        LevelA,
        LevelB,
        LevelC
    }
```

>这里定义了一个无线用户需求等级的数据结构，分为A、B、C三个等级。
>EnumTypeDescription特性标签说明这个枚举类型与描述定义有对应关系，其缺省类型是B。

### EnumTypeDescription特性定义

该特性的定义如下：

```CSharp
    [AttributeUsage(AttributeTargets.Enum)]
    public class EnumTypeDescriptionAttribute : Attribute
    {
        public Type EnumType { get; }

        public Tuple<object, string>[] TupleList { get; }

        public object DefaultValue { get; }

        public EnumTypeDescriptionAttribute(Type enumType, object defaultValue)
        {
            EnumType = enumType;
            DefaultValue = defaultValue;
            TupleList = WirelessConstants.EnumDictionary[EnumType.Name];
        }
    }
```

这里定义了3个属性

```CSharp
public Type EnumType { get; }//对应的枚举类型

public object DefaultValue { get; }//默认的枚举取值

public Tuple<object, string>[] TupleList { get; }//生成枚举值和描述的对应关系，从WirelessConstants.EnumDictionary中查询
```

### CSV数据定义

定义了部分CSV数据格式，另外部分CSV格式，如DT数据放在其他项目中

## EXCEL数据定义

定义了EXCEL数据格式。

## LinqToCsv

CSV格式文件数据导入程序，来自网上略作修改。

## LinqToExcel

EXCEL格式文件数据导入程序，来自网上略作修改。

## Lz4Net

## 其他公共数据类型和变换
