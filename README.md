﻿﻿
# 系统介绍
 - SiMay远程控制管理系统是一个Windows远程控制系统，底层基于IOCP的异步通信模型，能对海量客户端实时监控，目前功能已实现:远程桌面，基于可视区域逐行扫描算法，仅传输桌面变化区域，可有效节省传输流量；经典的文件管理方式，实现了快速上传下载文件和文件夹；实时传输远程语音与发送语音，及实时捕获远程摄像头；Windows风格界面的经典注册表管理模块；命令行终端；系统进程管理，实时进程查看，用户桌面视图墙轮播等功能，被控服务端支持绿色启动及以系统服务方式安装，解决了WindowsSession桌面切换限制，实现了捕获UAC,WinLogon桌面；系统实现了中间会话服务器，可支持不同平台多主控端同时监控同一被控端，项目完全采用C#.NET开发，代码仅供参考，项目不定时更新，欢迎关注点星星，fork。欢迎入群技术交流:905958449 :laughing:  :blush: 

# 申明
 - 作为创作者，我对由此软件引起的任何行为和/或损害不承担任何责任。 您对自己的行为承担全部责任，并承认此软件仅用于教育和研究目的。 不得用于您不拥有或有权使用的任何系统。 使用此软件，您自动同意上述内容，感谢支持。 

# 背景
 - 仅是个人项目，总结在工作中所遇到的有趣技术，新技术，新语法等，系统架构等，经过几次重构，系统相对比较成熟了，决定开源反馈开源社区，希望更多人能和我一起进步，欢迎吐槽改进。

![主控界面](https://images.gitee.com/uploads/images/2019/0717/225727_cc5c40c8_1654743.jpeg "主控制界面")
![创建服务端](https://images.gitee.com/uploads/images/2020/0216/154537_c7d2473c_1654743.png "创建服务")
![远程桌面](https://images.gitee.com/uploads/images/2019/0717/225853_2d8f4f8d_1654743.jpeg "远程桌面")
![文件管理](https://images.gitee.com/uploads/images/2019/0717/225829_9fed04ca_1654743.jpeg "文件管理")
![语音传输](https://images.gitee.com/uploads/images/2019/0717/225918_159b8bec_1654743.jpeg "语音传输")
![注册表管理](https://images.gitee.com/uploads/images/2019/0906/221633_6f9559ff_1654743.jpeg "注册表管理")
![中间服务器](https://images.gitee.com/uploads/images/2020/0216/154108_e5e50552_1654743.png "多对一实时控制")

# 系统项目结构

### SiMay.Core【公共核心功能】
 - SiMay.Basic --基础通用库
 - SiMay.Core --系统核心统一公共库【统一通讯指令丶共用组件丶通信数据实体等..】
 - SiMay.Serialize --轻量级高性能二进制序列化库【作用:系统通信数据实体化】

### SiMay.RemoteMonitor【主控制端】
 - SiMay.RemoteControlsCore --主控端核心库
 - SiMay.RemoteMonitor --Windows主控端(基于核心库)
 - SiMay.RemoteMonitorForWeb【计划，未完成】 --Web主控端后端(基于核心库，支持.NET Core)，基于WebSocket与前端通信
 - SiMay.RemoteMonitorForWebSite【计划，未完成】 --Web监控前端

### SiMay.RemoteService【远程被控服务端】
 - SiMay.RemoteService.Loader --内存加载Loader，实现远程内存载入被控端核心库
 - SiMay.ServiceCore --被控端核心库/被控端主程序

### SiMay.SessionProvider【会话提供层】
 - SiMay.Net.SessionProvider --会话提供库【作用：提供服务器监听模式或者中间会话代理协议】
 - SiMay.Net.SessionProvider.Core --代理协议统一公用库【作用：统一中间库和服务器的通信指令及序列化等】
 - SiMay.Net.SessionProviderService --中间会话代理服务器【作用：提供保持服务端会话保持丶数据转发功能，基于此实现多平台端监控】

### SiMay.Sockets【通信层】
 - SiMay.Socket.Standard --轻量级通信引擎
 - SiMaySocketTestApp --通信引擎测试程序

### SiMay.Web.MonitorService【Web监控服务端，已弃用】
 - SiMay.Net.HttpRemoteMonitorService --WebSocket监控服务端


### 编译
 - 1.Bin为编译目录，重新生成后，主控程序将编译到此目录，Bin->dat目录为被控服务端目录，被控服务端编译后在此。(没有目录新建一下)

### 运行
 - 1.局域网

主控端:打开位与Bin目录下的主控端程序SiMayRemoteMonitor.exe，确认系统设置服务器地址为0.0.0.0(监听本机所有网卡)，端口默认5200，使用会话模式为=本地服务器，然后保存配置重启程序,
重启后日志输出监听成功，即主控端设置正确。

被控服务端创建:打开主控端-->创建客户-->地址输入本机物理地址(或127.0.0.1)，端口设置为服务端监听端口(默认5200)-->点击连接测试检查配置是否正确-->创建服务端文件，服务端文件即为配置完成的被控端程序(如提示找不到文件，请检查被控服务程序是否存在[编译步骤是否正确])，双击运行被控服务程序即可在主控端看见服务在线信息，如主控端无在线信息，请检查上述步骤是否配置正确。

 - 2.广域网

条件:需要主控端处于公网环境(或者设置路由内网映射、使用内网映射工具[如花生壳，内网通])，并且开放主控端监听端口(注意检查端口是否开放、防火墙通行规则)。
创建客户端-->被控服务端连接至主控端的公网地址，端口即可

 - 3.中间服务器部署

条件:需要中间服务器处于公网环境(建议部署在公网服务器，或者设置路由内网映射)，并且开放中间服务器监听端口(默认522端口、注意检查端口是否开放、防火墙通行规则)。

主控端设置: 系统设置-->会话服务器地址 输入 中间服务器的公网地址，端口。-->设置会话模式为:中间会话模式-->确认AccessKey与中间服务器Accesskey一致。(中间会话服务器系统设置位于标题栏系统菜单右键)-->创建客户端并选择会话模式为中间会话模式，ip，端输入中间服务器的公网地址即可

 - 4.web端监控【完善中，不可用】

配置IIS，部署SiMay.WebRemoteMonitor网站，编译启动SiMay.Net.HttpRemoteMonitorService，配置地址指向中间服务器ip，端口即可(无系统设置，需手动配置配置文件)，如连接成功，中间服务器出现主控制连接在线日志即可
使用浏览器，访问SiMay.WebRemoteMonitor网站，输入SiMay.Net.HttpRemoteMonitorService配置的账号密码即可，当有中间服务器有被控端会话时，将自动连接至http服务，连接成功后网页可看到被控服务端计算机桌面视图，长按视图可打开更多功能。

### 技术架构
 - 基于组件式的系统架构
 - 基于实体的实体消息协议
 - 基于IOCP的异步Socket高性能通信模型
 - 基于可视区域逐行扫描算法的远程桌面
 - 基于Windows WaInXX系列实现的语音通讯
 - 基于Dx组件捕获摄像头
 - 基于HOOK技术的键盘记录
 - 基于WebSocket技术实现Web端监控
 - 中间会话服务转发，支持多个主控端同时实时监控

### 开发环境
 - 建议 Visual Studio 2019 企业版

### 参与贡献
 - Fork 本仓库
 - 新建 Feat_xxx 分支
 - 提交代码
 - 新建 Pull Request

### 未来构想
 - 完善更多web监控功能

### SiMay远程监控管理系统更新说明

### 6.0更新
1. 跨.NET Core支持，重构中间会话服务器 --2020.2.15
2. 二进制序列化器采用反射缓存，提高系统性能 -- 2020.1.25
3. 屏幕视图轮播 --2020.1.15
4. Web端主控端 --未实现
5. SOCK5代理，并兼容中间服务转发 -- 未实现

### 5.0更新
1. 优化了通讯库,支持FULL丶PACK数据处理方式，实现了更友好的配置接口
2. 新增中间会话转发服务,增加了SessionProvider层，控制端支持监听模式丶中间会话模式，在此基础上实现了Web监控服务，支持Web方式监控
3. 增强了远程桌面模块,支持全屏监控的远程鼠标控制及多屏幕切换
4. 重构代码结构，实现了组件化系统框架，屏蔽了系统底层实现细节，增强了可扩展性	--2019.5.19
5. 远程桌面增加了可视区域扫描算法，仅扫描可视区域变化部分，优化了远程桌面模块，速度更加快了 --2019.4.2
6. 增强系统管理模块，实现了进程实时监控	--8.28
7. 语音监听，视频监控支持录制功能	--待实现
8. 被控服务实现了以服务方式安装，使用服务方式可实现Session隔离穿透捕获桌面(锁屏，UAC)，	--11.9
9. 文件管理功文件夹传输重构优化	2019.7.13
10. 系统传输数据消息实体化 -- 2019-6-4
11. 二进制序列化器采用反射缓存，提高系统性能 --已实现
12. 远程桌面增加画面质量调整，优化低速率网络下的控制体验，使画面更加流畅 -- 7.27
13. 支持远程更新服务端 -- 7.27
14. 增加列表排序功能 -- 7.27
15. 注册表组件更新，支持二进制丶多种类型数据编辑 -- 9.6
16. 重构主控端，主控端逻辑核心库与展示层彻底分离(如:基于核心库横向扩展Web主控端，实现多平台逻辑复用) - 11.2

### 4.0更新
1. 重写了通讯层，解决网络环境极差时频繁断开连接的情况，实现了对象池，以更好的并发能力应对大规模的客户端数据交互
2. 设计了更稳定的通讯层接口，通讯层彻底与逻辑层分离
3. 优化了部分功能的通讯协议
4. 优化了远程桌面模块
5. 修复远程桌面在高分屏笔记本时显示不完全的问题
6. 修复视频监控显示不完全的问题
7. 增强了语音监听模块
8. 优化了窗体上的设计，用户体验更好了
9. 修复了系统管理