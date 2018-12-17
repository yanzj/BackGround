# 4G指标数据库设计

包括了大部分4G小区级指标的ORM实体定义

## 流量类指标

### ORM表

#### 华为流量指标FlowHuawei

对应华为网管中提取的流量等指标统计

#### 中兴流量指标FlowZte

对应中兴网管中提取的流量等指标统计

#### 华为双流比指标DoubleFlowHuawei

对应华为网管中提取的双流比指标统计

#### 中兴双流比指标DoubleFlowZte

对应中兴网管中提取的双流比指标统计

### 内容整理

## 连接类指标

### 华为RRC连接成功率RrcHuawei

### 中兴RRC连接成功率RrcZte

### 华为Rssi指标RssiHuawei

### 中兴Rssi指标RssiZte

## 质量类指标

### 华为全天CQI优良比CqiHuawei

### 中兴全天CQI优良比CqiZte

### 忙时CQI优良比HourCqi

### 精确覆盖率PreciseCoverage4G

## 容量类指标

### 华为PRB利用率PrbHuawei

### 中兴PRB利用率PrbZte

### 忙时PRB利用率HourPrb

### 忙时用户数HourUsers