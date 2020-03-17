﻿using System;
using static SiMay.Serialize.Standard.PacketSerializeHelper;

namespace SiMay.Net.SessionProvider.Core
{
    /// <summary>
    /// 消息处理帮助(格式:Int16的消息头 + payload)
    /// </summary>
    public static class MessageHelper
    {
        /// <summary>
        /// 序列化数据实体，并封装消息头
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static byte[] CopyMessageHeadTo<T>(T cmd, object entity)
            where T : struct
        {
            return CopyMessageHeadTo(cmd, SerializePacket(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="data"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static byte[] CopyMessageHeadTo<T>(T cmd, byte[] data, int offset, int size)
            where T : struct
        {
            byte[] buff = new byte[size + sizeof(short)];
            BitConverter.GetBytes(Convert.ToInt16(cmd)).CopyTo(buff, 0);
            Array.Copy(data, 0, buff, sizeof(Int16) + offset, size);

            return buff;
        }

        /// <summary>
        /// 构建消息至数据头部
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] CopyMessageHeadTo<T>(T cmd, byte[] data = null)
            where T : struct
        {
            if (data == null)
                data = new byte[] { };

            return CopyMessageHeadTo(cmd, data, 0, data.Length);
        }

        /// <summary>
        /// 构建消息头至数据头部
        /// </summary>
        /// <param name="cmd">消息头</param>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static byte[] CopyMessageHeadTo<T>(T cmd, string str)
            where T : struct
        {
            byte[] data = str.UnicodeStringToBytes();

            return CopyMessageHeadTo(cmd, data, 0, data.Length);
        }

        /// <summary>
        /// 获取消息头
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T GetMessageHead<T>(this byte[] data)
            where T : struct
        {
            return (T)Enum.ToObject(typeof(T), BitConverter.ToInt16(data, 0));
        }

        /// <summary>
        /// 获取消息载体
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] GetMessagePayload(this byte[] data)
        {
            byte[] payload = new byte[data.Length - sizeof(short)];
            Array.Copy(data, sizeof(short), payload, 0, payload.Length);
            return payload;
        }

        /// <summary>
        /// 反序列化数据实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T GetMessageEntity<T>(this byte[] data)
            where T : new()
        {
            var entity = DeserializePacket<T>(GetMessagePayload(data));
            return entity;
        }
    }
}