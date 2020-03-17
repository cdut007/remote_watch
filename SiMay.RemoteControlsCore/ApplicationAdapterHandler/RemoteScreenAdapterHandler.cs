﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiMay.Basic;
using SiMay.Core;
using SiMay.Core.Enums;
using SiMay.Core.PacketModelBinder.Attributes;
using SiMay.Core.PacketModelBinding;
using SiMay.Core.Packets;
using SiMay.Core.Packets.Screen;
using SiMay.Core.ScreenSpy.Entitys;
using SiMay.Net.SessionProvider;
using SiMay.RemoteControlsCore.Enum;
using static SiMay.Serialize.Standard.PacketSerializeHelper;

namespace SiMay.RemoteControlsCore.HandlerAdapters
{
    public class RemoteScreenAdapterHandler : ApplicationAdapterHandler
    {

        /// <summary>
        /// 远程屏幕服务初始化完成
        /// </summary>
        public event Action<RemoteScreenAdapterHandler, int, int, int, MonitorItem[]> OnServcieInitEventHandler;

        /// <summary>
        /// 获取剪切板
        /// </summary>
        public event Action<RemoteScreenAdapterHandler, string> OnClipoardReceivedEventHandler;

        /// <summary>
        /// 屏幕帧处理
        /// </summary>
        public event Action<RemoteScreenAdapterHandler, Fragment[], ScreenReceivedType> OnScreenFragmentEventHandler;

        //public RemoteScreenAdapterHandler()
        //{
        //    CurrentSession.Socket.NoDelay = false;
        //}

        //已接收帧数
        private int _frameCount = 0;
        public void RemoteMouseKeyEvent(MOUSEKEY_ENUM @event, int point1, int point2)
        {
            SendTo(CurrentSession, MessageHead.S_SCREEN_MOUSEKEYEVENT,
                new ScreenKeyPack()
                {
                    Key = @event,
                    Point1 = point1,
                    Point2 = point2
                });
        }

        [PacketHandler(MessageHead.C_SCREEN_BITINFO)]
        private void SetBitmapHandler(SessionProviderContext session)
        {
            var bitinfo = GetMessageEntity<ScreenInitBitPack>(session);
            this.OnServcieInitEventHandler?.Invoke(this, bitinfo.Height, bitinfo.Width, bitinfo.PrimaryScreenIndex, bitinfo.Monitors);
        }

        [PacketHandler(MessageHead.C_SCREEN_DIFFBITMAP)]
        private void FullFragmentHandler(SessionProviderContext session)
        {
            var fragments = GetMessageEntity<ScreenFragmentPack>(session);
            this.OnScreenFragmentEventHandler?.Invoke(this, fragments.Fragments, ScreenReceivedType.Noninterlaced);
        }

        [PacketHandler(MessageHead.C_SCREEN_BITMP)]
        private void SigleFragmentHandler(SessionProviderContext session)
        {
            var fragments = GetMessageEntity<ScreenFragmentPack>(session);
            this.OnScreenFragmentEventHandler?.Invoke(this, fragments.Fragments, ScreenReceivedType.Difference);
        }
        [PacketHandler(MessageHead.C_SCREEN_SCANCOMPLETE)]
        private void ScanFinishHandler(SessionProviderContext session)
        {
            this.OnScreenFragmentEventHandler?.Invoke(this, new Fragment[0], ScreenReceivedType.DifferenceEnd);
        }

        public void StartGetScreen(int height, int width, int x, int y, ScreenDisplayMode mode)
        {
            var rect = SerializePacket(new ScreenHotRectanglePack()
            {
                X = x,
                Y = y,
                Height = height,
                Width = width,
                CtrlMode = mode.ConvertTo<int>()
            });

            _frameCount = 0;
            //第一帧不计入连续帧
            for (int i = 0; i < 3; i++)
                SendTo(CurrentSession, MessageHead.S_SCREEN_NEXT_SCREENBITMP, rect);
        }

        public void GetNextScreen(int height, int width, int x, int y, ScreenDisplayMode mode)
        {
            if (this.WhetherClose)
                return;

            _frameCount++;
            //Console.WriteLine(this.imgDesktop.Height + " | " + Width + "|" + this.Height + " | " + this.Width);

            if (_frameCount == 1)//使帧数更连续
            {
                var rect = SerializePacket(new ScreenHotRectanglePack()
                {
                    X = x,
                    Y = y,
                    Height = height,
                    Width = width,
                    CtrlMode = mode.ConvertTo<int>()
                });
                for (int i = 0; i < 3; i++)
                    SendTo(CurrentSession, MessageHead.S_SCREEN_NEXT_SCREENBITMP, rect);
            }
            else if (_frameCount == 3)
                _frameCount = 0;
        }

        public void MonitorChange(int screenIndex)
        {
            SendTo(CurrentSession, MessageHead.S_SCREEN_CHANGE_MONITOR,
                new MonitorChangePack()
                {
                    MonitorIndex = screenIndex
                });
        }

        public void GetInitializeBitInfo()
        {
            SendTo(CurrentSession, MessageHead.S_SCREEN_GET_INIT_BITINFO);
        }
        public void RemoteDeleteWallPaper()
        {
            SendTo(CurrentSession, MessageHead.S_SCREEN_DELETE_WALLPAPER);
        }
        public void RemoteMouseBlock(bool islock)
        {
            byte @lock = islock ? (byte)10 : (byte)11;
            SendTo(CurrentSession, MessageHead.S_SCREEN_MOUSEBLOCK, new byte[] { @lock });
        }

        public void RemoteScreenBlack()
        {
            SendTo(CurrentSession, MessageHead.S_SCREEN_BLACKSCREEN);
        }

        public void RemoteChangeScanMode(ScreenScanMode scanMode)
        {
            SendTo(CurrentSession, MessageHead.S_SCREEN_CHANGESCANMODE, new byte[] { (byte)scanMode });
        }

        public void RemoteResetBrandColor(BrandColorMode mode)
        {
            SendTo(CurrentSession, MessageHead.S_SCREEN_RESET, new byte[] { (byte)mode });
        }

        public void RemoteSetScreenQuantity(long qty)
        {
            SendTo(CurrentSession, MessageHead.S_SCREEN_SETQTY,
                new ScreenSetQtyPack()
                {
                    Quality = qty
                });
        }

        public void SetRemoteClipoardText(string text)
        {
            SendTo(CurrentSession, MessageHead.S_SCREEN_SET_CLIPBOARD_TEXT,
                                    new ScreenSetClipoardPack()
                                    {
                                        Text = text
                                    });
        }
        public void SendCtrlAltDel()
        {
            SendTo(CurrentSession, MessageHead.S_SCREEN_CTRL_ALT_DEL);
        }

        public void ResetPFrame()
        {
            SendTo(CurrentSession, MessageHead.S_SCREEN_RESET_PFRAME);
        }

        public void GetRemoteClipoardText()
        {
            SendTo(CurrentSession, MessageHead.S_SCREEN_GET_CLIPOARD_TEXT);
        }
        [PacketHandler(MessageHead.C_SCREEN_CLIPOARD_TEXT)]
        private void GetClipoardValueHandler(SessionProviderContext session)
        {
            var response = GetMessageEntity<ScreenClipoardValuePack>(session);
            this.OnClipoardReceivedEventHandler?.Invoke(this, response.Value);
        }
    }
}
