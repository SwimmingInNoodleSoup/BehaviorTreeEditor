﻿using System;
using System.Collections.Generic;

namespace R7BehaviorTree
{
    public sealed class CSharpProxyManager : Singleton<CSharpProxyManager>, IProxyManager
    {
        private Dictionary<string, ProxyData> m_ProxyDic = new Dictionary<string, ProxyData>();
        private Dictionary<string, Type> m_ProxyTypeDic = new Dictionary<string, Type>();

        public void Initalize()
        {
            BehaviorTreeManager.Instance.AddProxyManager(EProxyType.CSharp, this);
            CollectProxyInfos();
        }

        public ProxyData GetProxyData(string classType)
        {
            return null;
        }

        public BaseNodeProxy CreateProxy(BaseNode node)
        {
            ProxyData proxyData = node.ProxyData;
            CSharpNodeProxy nodeProxy = new CSharpNodeProxy();

            return null;
        }

        /// <summary>
        /// 注册Proxy
        /// </summary>
        /// <param name="classType">节点类</param>
        /// <param name="nodeType">节点类型</param>
        /// <param name="proxyType">代理类型</param>
        /// <param name="type">逻辑对应的Type</param>
        /// <param name="needUpdate">是否执行proxy的update(优化性能)</param>
        public void Register(string classType, ENodeType nodeType, Type type)
        {
            if (string.IsNullOrEmpty(classType))
            {
                string msg = "CSharpProxyManager.Register() \n classType is null.";
                BehaviorTreeManager.Instance.LogError(msg);
                throw new Exception(msg);
            }

            if (m_ProxyDic.ContainsKey(classType))
            {
                string msg = $"CSharpProxyManager.Register() \n m_ProxyDic already Contain key {classType}.";
                BehaviorTreeManager.Instance.LogError(msg);
                throw new Exception(msg);
            }

            ProxyData proxyData = new ProxyData();
            proxyData.ClassType = classType;
            proxyData.NodeType = nodeType;
            proxyData.ProxyType = EProxyType.CSharp;
            proxyData.Type = type;
            proxyData.NeedUpdate = false;

            m_ProxyDic.Add(classType, proxyData);
            m_ProxyTypeDic.Add(classType, type);
        }

        /// <summary>
        /// 收集所有C#端的Proxy信息
        /// </summary>
        private void CollectProxyInfos()
        {
            m_ProxyDic.Clear();
            m_ProxyTypeDic.Clear();

            Type[] types = typeof(BaseNodeProxy).Assembly.GetTypes();
            for (int i = 0; i < types.Length; i++)
            {
                Type type = types[i];
                object[] objs = type.GetCustomAttributes(typeof(BaseNodeAttribute), true);
                if (objs == null || objs.Length == 0)
                    continue;

                BaseNodeAttribute baseNodeAttribute = objs[0] as BaseNodeAttribute;

                if (baseNodeAttribute == null)
                    continue;

                string classType = baseNodeAttribute.ClassType;

                Register(classType, baseNodeAttribute.NodeType, type);
            }
        }

        public Type GetType(string classType)
        {
            Type type = null;
            if (!m_ProxyTypeDic.TryGetValue(classType, out type))
            {
                string msg = $"CSharpProxyManager.GetType() \n m_ProxyTypeDic not contains key {classType}.";
                BehaviorTreeManager.Instance.LogError(msg);
                throw new Exception(msg);
            }

            return type;
        }
    }
}