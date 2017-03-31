﻿# Slickflow
.NET Workflow Engine

Slickflow is implemented by activity and transition iterative algorithm. It supports sequence, split/merge, sub-process, multi-instance, withdraw, sendback and reverse pattern.

Slickflow used BPMN graphic elements to build its workflow natation.

Slickflow designer is a HTML5, JQuery, SVG based web designer.

Slickflow project support SQLSERVER, ORACLE, MySQL database, it implemented by dapper library.

There are demo programs that you can find in WebDemo, WinformDemo project.

The source project is under LGPL license, we also provide customers commercial license. if you have any further inquery, please feel free to contact us: 

Quick Start:
https://github.com/besley/Slickflow/wiki

Slickflow website:
http://www.slickflow.com

Demo:
http://www.slickflow.com/demo/index

Document:
http://www.slickflow.com/wiki/index

QQ(Author): 47743901

EMail: sales@ruochisoft.com



### Slickflow(1.5.8) 企业版更新说明:
1. 增加加签通过率类型字段CompareType，用于加签办理页面动态指定（非设计器指定，是运行时决策指定）变量传入；

示例如下：

        //动态变量数据格式(包含在WfAppRunner属性中)
        "DynamicVariables": {
            "SignForwardType": "SignForwardBefore",
            "SignForwardCompleteOrder": 2,
            "CompareType":  "Count"
        }

2. 实现会签加签通过率两种类型(个数和百分比)的全覆盖功能；

3. 修正Gateway节点之后Transition定义的ReceiverType类型未能获取的BUG；

4. 实现跨Gateway节点退回的功能(OrSplit单一分支撤回)。


### Slickflow(1.5.7) 企业版更新说明:
1. Slickflow.Designer 设计器项目全面重构编写，更新如下：

  1). 升级到ASP.NET MVC5;
  
  2). 升级到BOOTSTRAP3.3.7；
  
  3). 图形库框架升级到JSPLUMB2.2.8，图形体验更流畅；
  
  4). AG-Grid取代SlickGrid，AG-Grid在开源社区方面的建设更加完善；
  
2. 项目解决方案VS2017版本建立。

### Slickflow(1.5.6) 企业版更新说明：
1. 提供获取流程发起人的流程图连线定义（Transition property page）
2. 修正子流程节点变量名称改变后的条件判断处理；


### Slickflow(1.5.5) Demo版本功能说明：
**1. 引擎**

- 1) 引擎集成国产数据库人大金仓Kingbase；

- 2) 添加Slickflow.Module项目，实现组织机构的模块化构建；

- 3) 引擎实现提交至发送人员的部门主管，下属或者同级同事流转功能，相应增加部门员工数据表和存储过程；

  <Transition>
    ...
    <Receiver type="Superior" />
    ...
  </Transition>
  
**2. 设计器**

- 1) 流程设计器增加节点元素添加的操作面板；

- 2) 流程设计器修正连线控制Gateway的显示Bug；

**3. DEMO示例**

- 1) WebDemo/MvcDemo/Designer去除多项目引用，调试运行不依赖IIS Server。


### Slickflow(1.5.2) Demo版本功能说明：
**1. DEMO示例**

重新改版MvcDemo项目(电商生产订单流程)，采用Bootstrap框架，增加人员弹框功能演示；

**2. 设计器**

重新改版设计器项目，使用Bootstrap框架，优化界面及性能；

**3. 引擎**

- 1）引擎增加辅助查询步骤角色用户关系接口；

GetNextActivityRoleUserTree();  	//下一步选人弹框控件使用

GetRoleUserListByProcess();

GetUserListByRole();

GetRoleUserByRoleIDs();



### Slickflow(1.5.1) Demo版本功能说明：

**1. 流程引擎**

  - 1) 会签加签不同模式处理，串行并行及通过率设置；会签加签内部撤销退回处理；
  - 2) 引擎响应外部接口，并实现调用功能；

**2. 设计器**

  - 1) IE8及以上, Firefox 和谷歌浏览器兼容版本实现。
  - 2) 增加会签加签子流程等特性配置；
  - 3) 增加显示网格功能。

**3. Slickflow多数据库支持**

  - 改造Dapper，使得Slickflow支持Oracle，MySQL等数据库。

**4. 会签加签事件交互说明文档**

  - Slickflow会签加签事件程序调用说明文档.docx

**5. 增加Slickflow.Data项目，开放源代码**
  
**6. 修正1.5.0版本对Demo中的SQL语句报错问题**


EMail: william.ligong@yahoo.com

QQ(Author): 47743901

Slickflow 网站:

http://www.slickflow.com

DEMO:

http://www.slickflow.com/demo/index

文档:

http://www.slickflow.com/wiki/index

捐赠:

http://www.slickflow.com/donate/index
