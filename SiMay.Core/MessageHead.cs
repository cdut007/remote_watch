﻿using System;
using System.Reflection;

namespace SiMay.Core
{

    /// <summary>
    /// S_ :表示控制端接收的命令头
    /// C_ :表示服务端接收的命令头
    /// </summary>
    public enum MessageHead
    {
        S_GLOBAL_ONCLOSE = 0,                              //关闭连接通道
        S_GLOBAL_OK,                                       //确认标志
        C_GLOBAL_CONNECT = 1000,                           //连接确认包，带连接密码


        //主窗体------------------------------------------------------------
        S_MAIN_REMARK = 1000,                              //备注
        S_MAIN_ACTIVATE_APPLICATIONSERVICE,                //创建功能服务
        S_MAIN_SESSION,                                    //会话管理
        S_MAIN_HTTPDOWNLOAD,                               //下载
        S_MAIN_OPEN_WEBURL,                                //打开URL
        S_MAIN_MESSAGEBOX,                                 //MessageBox
        S_MAIN_CREATE_DESKTOPVIEW,                         //屏幕查看
        S_MAIN_DESKTOPVIEW_GETFRAME,                       //获取屏幕
        S_MAIN_DESKTOPVIEW_CLOSE,                          //关闭屏幕墙
        S_MAIN_DESKTOPRECORD_OPEN,                         //设置屏幕记录
        S_MAIN_DESKTOPRECORD_CLOSE,                        //关闭屏幕记录
        S_MAIN_DESKTOPRECORD_GETFRAME,                     //获取下一帧屏幕记录数据
        S_MAIN_GROUP,                                      //分组
        S_MAIN_UPDATE,                                     //远程更新
        S_MAIN_PLUGIN_FILES,                               //插件文件
        S_MAIN_RELOADER,                                   //重新载入服务端程序(重启进程)
        S_MAIN_INSTANLL_SERVICE,                           //服务安装
        S_MAIN_UNINSTANLL_SERVICE,                         //卸载服务

        //接收指令
        C_MAIN_LOGIN = 2000,                               //上线信息
        C_MAIN_ACTIVE_APP,                                 //打开窗口
        C_MAIN_DESKTOPVIEW_CREATE,                         //创建屏幕查看控件
        C_MAIN_DESKTOPVIEW_FRAME,                          //屏幕墙数据
        C_MAIN_DESKTOPRECORD_FRAME,                        //屏幕记录数据
        C_MAIN_DESKTOPRECORD_OPEN,                         //打开桌面录制任务
        C_MAIN_GET_PLUGIN_FILES,                           //下载插件
        C_MAIN_TEMP_LOGIN,                                 //临时上线信息(插件未加载状态)

        //远程桌面------------------------------------------------------------------------
        S_SCREEN_NEXT_SCREENBITMP = 1000,                  //请求获取图像数据
        S_SCREEN_MOUSEKEYEVENT,                            //鼠键操作
        S_SCREEN_MOUSEBLOCK,                               //鼠标锁定
        S_SCREEN_BLACKSCREEN,                              //黑屏
        S_SCREEN_SET_CLIPBOARD_TEXT,                       //设置剪贴板内容
        S_SCREEN_RESET,                                    //改变屏幕位色
        S_SCREEN_CHANGESCANMODE,                           //改变扫描方式
        S_SCREEN_SETQTY,                                   //设置图像质量
        S_SCREEN_GET_CLIPOARD_TEXT,                        //获取剪切板Text
        S_SCREEN_GET_INIT_BITINFO,                         //获取初始化屏幕信息
        S_SCREEN_CTRL_ALT_DEL,                             //模拟Ctrl+Alt+Del
        S_SCREEN_CHANGE_MONITOR,                           //切换监视器
        S_SCREEN_DELETE_WALLPAPER,                         //移除壁纸
        S_SCREEN_RESET_PFRAME,                             //重置画面刷新

        C_SCREEN_BITINFO = 2000,                           //桌面大小信息
        C_SCREEN_SCANCOMPLETE,                             //屏幕扫描完成
        C_SCREEN_BITMP,                                    //图像数据
        C_SCREEN_DIFFBITMAP,                               //差异完整屏幕数据
        C_SCREEN_CLIPOARD_TEXT,                            //剪切板Text

        //文件管理----------------------------------------------------------------
        S_FILE_GET_DRIVES = 1000,                          //发送驱动器列表
        S_FILE_GET_FILES,                                  //打开目录
        S_FILE_EXECUTE,                                    //打开文件
        S_FILE_DELETE,                                     //删除
        S_FILE_CREATE_DIR,                                 //创建文件夹
        S_FILE_RENAME,                                     //文件重命名
        S_FILE_RENAME_FINISH,                              //重命名完成
        S_FILE_FILE_COPY,                                  //复制文件
        S_FILE_FILE_PASTER,                                //粘贴文件
        S_FILE_DOWNLOAD,                                   //下载文件
        S_FILE_OPEN_TEXT,                                  //打开Text
        S_FILE_UPLOAD,                                     //上传文件
        S_FILE_NEXT_DATA,                                  //下块数据
        S_FILE_FRIST_DATA,                                 //首个数据包
        S_FILE_DATA,                                       //文件数据
        S_FILE_STOP,                                       //停止任务
        S_FILE_GETDIR_FILES,                               //获取文件夹中的文件
        S_FILE_TREE_DIR,                                   //为树形控件获取指定目录所有文件夹
        S_FILE_REDIRION,                                   //跳转路径

        C_FILE_FILE_LIST = 2000,                           //文件列表
        C_FILE_CREATEF_DIR_FNISH,                          //文件夹创建完成
        C_FILE_OPEN_STATUS,                                //文件打开状态
        C_FILE_FRIST_DATA,                                 //首个数据包
        C_FILE_NEXT_DATA,                                  //下块数据
        C_FILE_DATA,                                       //文件数据
        C_FILE_DELETE_FINISH,                              //删除完成
        C_FILE_PASTER_FINISH,                              //粘贴完成
        C_FILE_RENAME_FINISH,                              //重命名完成
        C_FILE_ERROR_INFO,                                 //错误信息
        C_FILE_TEXT,                                       //Text数据
        C_FILE_DIR_FILES,                                  //文件夹内的文件
        C_FILE_COPY_FINISH,                                //复制文件完成
        C_FILE_TREE_DIRS,                                  //树形控件文件夹信息

        //SHELL-------------------------------------------------------------------
        S_SHELL_INPUT = 1000,                              //执行CMD命令

        C_SHELL_RESULT = 2000,                             //SHELL结果

        //系统管理----------------------------------------------------------------
        S_SYSTEM_GET_PROCESS_LIST = 1000,                  //进程列表
        S_SYSTEM_GET_SYSTEMINFO,                           //系统信息
        S_SYSTEM_KILL,                                     //结束进程
        S_SYSTEM_MAXIMIZE,                                 //最大化窗口
        S_SYSTEM_MINIMIZE,                                 //最小化窗体
        S_SYSTEM_GET_OCCUPY,                               //获取系统占用率信息
        S_SYSTEM_ENUMSESSIONS,                             //获取所有会话信息
        S_SYSTEM_CREATE_USER_PROCESS,                      //创建用户进程

        //接收指令
        C_SYSTEM_PROCESS_LIST = 2000,                      //进程列表
        C_SYSTEM_SYSTEMINFO,                               //系统信息
        C_SYSTEM_OCCUPY_INFO,                              //系统占用率信息
        C_SYSTEM_SESSIONS,                                 //会话信息

        //键盘记录---------------------------------------------------------------
        S_KEYBOARD_ONOPEN = 1000,                          //窗口打开
        S_KEYBOARD_OFFLINE,                                //开始离线记录
        S_KEYBOARD_GET_OFFLINEFILE,                        //获取离线文件并停止离线记录

        C_KEYBOARD_DATA = 2000,                            //键盘记录数据
        C_KEYBOARD_OFFLINEFILE,                            //离线文件

        //语音监听---------------------------------------------------------------
        S_AUDIO_START,                                     //开始语音
        S_AUDIO_DATA = 1000,                               //语音数据
        S_AUDIO_DEIVCE_ONOFF,                              //开关

        C_AUDIO_DATA = 2000,                               //语音数据
        C_AUDIO_DEVICE_OPENSTATE,                          //打开失败的设备

        //视频监控----------------------------------------------------------------
        S_VIEDO_GET_DATA = 1000,                           //获取图像
        S_VIEDO_RESET,                                     //改变画质

        C_VIEDO_DATA = 2000,                               //图像
        C_VIEDO_DEVICE_NOTEXIST,                           //未检测到视频设备

        //注册表管理----------------------------------------------------------------
        S_REG_OPENDIRECTLY = 1000,                         //打开根目录
        S_REG_OPENSUBKEY,                                  //打开子项
        S_REG_GETVALUES,                                   //获取键值数据
        S_REG_CREATEVALUE,                                 //创建键值
        S_REG_CREATESUBKEY,                                //创建项
        S_REG_DELETEVALUE,                                 //删除键值
        S_REG_DELETESUBKEY,                                //删除子项
        S_REG_CHANGEVALUE,                                 //修改键值数据

        C_REG_ROOT_DIRSUBKEYNAMES = 2000,                  //根目录数据
        C_REG_SUBKEYNAMES,                                 //子项数据
        C_REG_VALUENAMES,                                  //键值数据
        C_REG_DELETESUBKEY_FINSH,                          //删除子项完成
        C_REG_CREATESUBKEY_FINSH,                          //创建子项完成
        C_REG_DELETEVALUE_FINSH,                           //删除键值完成

        S_NREG_LOAD_REGKEYS = 1000,                        //加载键
        S_NREG_CREATE_KEY,                                 //创建KEY
        S_NREG_CREATE_VALUE,                               //创建值
        S_NREG_RENAME_KEY,                                 //重命名键
        S_NREG_RENAME_VALUE,                               //重命名值
        S_NREG_DELETE_KEY,                                 //删除键
        S_NREG_DELETE_VALUE,                               //删除值
        S_NREG_CHANGE_VALUE,                               //更改值
        
        C_NREG_LOAD_REGKEYS = 2000,                        //加载
        C_NREG_CREATE_KEY_RESPONSE,                        //创建KEY响应
        C_NREG_CREATE_VALUE_RESPONSE,                      //创建Value响应
        C_NREG_RENAME_KEY_RESPONSE,                        //重命名响应
        C_NREG_RENAME_VALUE_RESPONSE,                      //重命名值
        C_NREG_DELETE_KEY_RESPONSE,                        //删除键响应
        C_NREG_DELETE_VALUE_RESPONSE,                      //删除值响应
        C_NREG_CHANGE_VALUE_RESPONSE,                      //更改值响应

        S_TCP_GET_LIST = 1000,                             //获取TCP连接列表
        S_TCP_CLOSE_CHANNEL,

        C_TCP_LIST = 2000,                                 //关闭TCP连接
        C_TCP_CLOSE_ERROR_LIST,                            //关闭错误项

        S_STARTUP_GET_LIST = 1000,                         //获取所有启动项
        S_STARTUP_ADD_ITEM,                                //添加启动项
        S_STARTUP_REMOVE_ITEM,                             //移除启动项目

        C_STARTUP_LIST = 2000,                             //启动项列表
        C_STARTUP_OPER_RESPONSE                            //操作结果
    }
}